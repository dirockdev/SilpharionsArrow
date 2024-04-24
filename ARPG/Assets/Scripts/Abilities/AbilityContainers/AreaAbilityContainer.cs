using Newtonsoft.Json;
using System;
using UnityEngine;
[Serializable]
public class AreaAbilityContainer : AbilityContainer
{
    
    public int tickDamage;
    
    public int area;
    
    public float timeAlive;
    
    public int homingSpeed;
    
    public bool isHoming;

    public AreaAbilityContainer(Ability ability) : base(ability)
    {
        AreaAbility areaAbility = (AreaAbility)ability;
        area = areaAbility.area;
        tickDamage = areaAbility.tickDamage;
        timeAlive = areaAbility.timeAlive;
        isHoming = areaAbility.isHoming;
        homingSpeed = areaAbility.homingSpeed;
    }

    public void SetValues(AreaAbilityContainer areaAbilityContainer)
    {
        tickDamage = areaAbilityContainer.tickDamage;
        area = areaAbilityContainer.area;
        timeAlive = areaAbilityContainer.timeAlive;
        homingSpeed = areaAbilityContainer.homingSpeed;
        isHoming = areaAbilityContainer.isHoming;
        cooldownTime = areaAbilityContainer.cooldownTime;
        coolDownLevel = areaAbilityContainer.coolDownLevel;
        isUnlocked = areaAbilityContainer.isUnlocked;
    }
}
