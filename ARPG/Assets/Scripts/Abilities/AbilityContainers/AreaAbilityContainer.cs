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
        this.tickDamage = areaAbilityContainer.tickDamage;
        this.area = areaAbilityContainer.area;
        this.timeAlive = areaAbilityContainer.timeAlive;
        this.homingSpeed = areaAbilityContainer.homingSpeed;
        this.isHoming = areaAbilityContainer.isHoming;
        cooldownTime = areaAbilityContainer.cooldownTime;
        coolDownLevel = areaAbilityContainer.coolDownLevel;
    }
}
