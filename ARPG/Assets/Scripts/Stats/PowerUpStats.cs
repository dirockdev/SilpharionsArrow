using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/PowerUpStats")]
public class PowerUpStats : ScriptableObject
{

    [Header("Projectile")]

    public int projectiles;
    public int crit;
    public int angle;
    public int damage;
    public int speed;
    public float slow;
    [Header("Dash")]
    public int movSpeed;
    public int timeSpeed;
    public int healthAmount;
}
