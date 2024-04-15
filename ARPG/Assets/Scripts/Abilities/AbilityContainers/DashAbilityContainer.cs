using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class DashAbilityContainer : AbilityContainer
{
    public int movSpeed;
    public int healAmount;
    public int cooldownAbilities;
    public int timeSpeedBurst;
    public AnimationHandler animationHandler;
    public AbilityHandler abilityHandler;
    public bool canArrowRain;

    public float animSpeed;
    public DashAbilityContainer(Ability ability) : base(ability)
    {
        DashAbility dashAbility = (DashAbility)this.ability;
        movSpeed = dashAbility.moveSpeed;
        healAmount = dashAbility.healAmount;
        cooldownAbilities = dashAbility.cooldownAbilities;
        timeSpeedBurst = dashAbility.timeSpeedBurst;
        animationHandler=InstancePlayer.instance.GetComponentInChildren<AnimationHandler>();
        abilityHandler=InstancePlayer.instance.GetComponent<AbilityHandler>();
        canArrowRain=dashAbility.canArrowRain;
        animSpeed = 1f;
    }

    
}
