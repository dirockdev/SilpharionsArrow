using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public AnimationCurve scaleCurve;
    public float experience = 0;
    public int rotationSpeed = 10;
    public int speed, health, level;

    public float requiredExp;


}
