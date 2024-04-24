using System.Collections;
using System.Collections.Generic;
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


    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra en el área de activación es el jugador
        if (other.CompareTag("Player"))
        {
            // Activa el spawner para instanciar enemigos
            SpawnEnemies();
            gameObject.SetActive(false); // Marca el spawner como activado para que no se active nuevamente
        }
    }

    void SpawnEnemies()
    {
        for (int i = minEnemies; i < maxEnemies; i++)
        {
            GameObject enemyToSpawn= enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];
            ObjectPoolManager.SpawnObject(enemyToSpawn, transform.position, Quaternion.identity);
        }
            
            
    }
}
