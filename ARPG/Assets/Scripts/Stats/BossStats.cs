using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Stats/Boss")]
public class BossStats : EnemiesStats
{
    public float areaAttackRadius;
    public int projectileSpeed;
    public float laserAttackRange;
    public GameObject laserPrefab;
}
