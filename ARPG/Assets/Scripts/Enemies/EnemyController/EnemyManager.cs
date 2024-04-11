using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    private void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            ObjectPoolManager.SpawnObject(enemy, new Vector3(-12, 0, 0), Quaternion.identity);
        }
    }
    public void SpawnEnemy()
    {
        ObjectPoolManager.SpawnObject(enemy,Vector3.zero,Quaternion.identity);
    }

}
