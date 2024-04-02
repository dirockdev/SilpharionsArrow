using UnityEngine;



[CreateAssetMenu(menuName = "Stats/Enemy")]
public class EnemiesStats : ScriptableObject
{
    public int speed, health, damage,attackspeed, radiusDetection, exp, acceleration, angularVelocity;
}
