using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;

public class RangedEnemy : EnemyBase
{
    GameObject enemyProjectile;
    RangedStats rangedStats;
    private int projectileSpeed;
    private void Awake()
    {
        rangedStats = (RangedStats)stats;
    }
    private void OnEnable()
    {
        enemyProjectile = rangedStats.projectilePrefab;
        projectileSpeed = rangedStats.projectileSpeed;
    }
    protected new void DoDamage()
    {
        GameObject projectileInstance=ObjectPoolManager.SpawnObject(enemyProjectile, transform.position, Quaternion.identity);
        EnemyProjectile projectileScript = projectileInstance.GetComponent<EnemyProjectile>();
        if (projectileScript != null)
        {
            // Configurar el proyectil (por ejemplo, dirección y velocidad)
            Vector3 direction = (target.position - transform.position).normalized;
            projectileScript.SetDirection(direction, projectileSpeed);
        }
    }
}