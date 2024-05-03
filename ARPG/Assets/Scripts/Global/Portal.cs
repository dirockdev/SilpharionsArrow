using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
public class Portal : MonoBehaviour, IInteractObject
{
    Outline outline;

    [SerializeField]Transform terrainPos;
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
            other.GetComponent<NavMeshAgent>().Warp(terrainPos.position);
        }
    }

}
