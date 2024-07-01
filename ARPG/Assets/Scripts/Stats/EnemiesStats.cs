using UnityEngine;



[CreateAssetMenu(menuName = "Stats/Enemy")]
public class EnemiesStats : ScriptableObject
{
    public AnimationCurve scaleCurve;
    public int speed=5, health=300, damage=100,attackspeed=1, radiusDetection=3, exp=200, acceleration=10, angularVelocity=1000;
    public bool canPoison, canStun;
}
