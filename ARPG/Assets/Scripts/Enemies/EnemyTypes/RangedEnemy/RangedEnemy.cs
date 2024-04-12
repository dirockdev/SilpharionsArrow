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
    private new void Awake()
    {
        base.Awake();
        rangedStats = (RangedStats)stats;
    }
    private new void OnEnable()
    {
        base.OnEnable();
        enemyProjectile = rangedStats.projectilePrefab;
        projectileSpeed = rangedStats.projectileSpeed;
    }
    protected override void DoDamage()
    {
        GameObject projectileInstance=ObjectPoolManager.SpawnObject(enemyProjectile, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.identity);
        EnemyProjectile projectileScript = projectileInstance.GetComponent<EnemyProjectile>();

        if (projectileScript != null)
        {
            // Configurar el proyectil (por ejemplo, dirección y velocidad)
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0;
            direction.Normalize();
            projectileScript.SetDirection(direction, projectileSpeed);
            projectileScript.SetDamage(damage);
        }
    }
}