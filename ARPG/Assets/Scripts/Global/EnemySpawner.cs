using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemiesToSpawn; 

    [SerializeField]
    private int minEnemies; 
    [SerializeField]
    private int maxEnemies;

    private BoxCollider boxCollider;
    private void Start()
    {
        boxCollider=GetComponent<BoxCollider>();
        Portal.onTakePortal += EnablePortal;
    }
    private void OnDisable()
    {
        Portal.onTakePortal -= EnablePortal;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra en el área de activación es el jugador
        if (other.CompareTag("Player"))
        {
            // Activa el spawner para instanciar enemigos
            SpawnEnemies();
            boxCollider.enabled = false;

        }
    }

    private void EnablePortal()
    {
        if(boxCollider.enabled == false)
        boxCollider.enabled = true;
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < Random.Range(minEnemies,maxEnemies); i++)
        {
            GameObject enemyToSpawn= enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];
            ObjectPoolManager.SpawnObject(enemyToSpawn, transform.position, Quaternion.identity);
        }
            
            
    }
}
