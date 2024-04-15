using DG.Tweening;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    private void Start()
    {
        DOTween.SetTweensCapacity(350,75);
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
