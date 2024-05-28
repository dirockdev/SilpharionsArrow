using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
public class Portal : MonoBehaviour, IInteractObject
{
    Outline outline;

    [SerializeField] Transform terrainPos;

    [SerializeField] MapStats statsMap;
    [SerializeField] Light directionalLight;

    [SerializeField] private int minLevel;

    private void Awake()
    {
        outline = GetComponentInChildren<Outline>();

    }
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public int[] Health()
    {
        return new[] { 1, 1 };
    }

    public string ObjectName()
    {
        return gameObject.name;
    }

    public Outline outLine()
    {
        return outline;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (PlayerExp.level >= minLevel)
            {
                other.GetComponent<NavMeshAgent>().Warp(terrainPos.localPosition);
                ChangeLight();
                AudioManager.instance.PlaySFXWorld("9", transform.position);
                CharacterStats.spawnPoint=terrainPos.localPosition; 
            }
            else
            {
                AudioManager.instance.PlaySFXWorld("10", transform.position, 0.5f, 0.6f);
            }

        }


    }

    private void ChangeLight()
    {
        directionalLight.color = statsMap.colorLight;
        directionalLight.intensity = statsMap.intensityLight;
        directionalLight.colorTemperature = statsMap.kelvin;
    }
}
