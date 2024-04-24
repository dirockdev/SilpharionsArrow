
using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class DashAbilityContainer : AbilityContainer
{
    
    public int movSpeed;
    
    public int healAmount;
    
    public int cooldownAbilities;
    
    public int timeSpeedBurst;
    [JsonIgnore]
    public AnimationHandler animationHandler;
    [JsonIgnore]
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

    public void SetLoadValues(DashAbilityContainer loadedDash)
    {
        movSpeed = loadedDash.movSpeed;
        healAmount = loadedDash.healAmount;
        cooldownAbilities = loadedDash.cooldownAbilities;
        timeSpeedBurst = loadedDash.timeSpeedBurst;
        canArrowRain = loadedDash.canArrowRain;
        animSpeed = loadedDash.animSpeed;
        cooldownTime = loadedDash.cooldownTime;
        coolDownLevel = loadedDash.coolDownLevel;
        isUnlocked = loadedDash.isUnlocked;
    }


}
