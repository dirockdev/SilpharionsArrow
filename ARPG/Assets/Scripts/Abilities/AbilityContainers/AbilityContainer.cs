using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public abstract class AbilityContainer
{
    [JsonIgnore]
    public Ability ability;
    
    public bool isPressed;
    public bool isUnlocked;
    
    public float currentCooldown;
    
    public float cooldownTime;
    [JsonIgnore]
    public float CooldownNormalized { get { return 1f - currentCooldown / cooldownTime; } }

    
    public int coolDownLevel;
    
    public bool canReduceCooldown;
    public AbilityContainer(Ability ability)
    {
        this.ability = ability;
        ChangeCooldownTime();
    }

    public AbilityContainer(Ability ability, float currentCooldown)
    {
        this.ability = ability;
        this.currentCooldown = currentCooldown;
        this.canReduceCooldown = false;
    }

    internal void Cooldown()
    {
        currentCooldown = cooldownTime;
    }

    internal void ReduceCooldown(float deltaTime)
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= deltaTime;
        }

    }
    public void ChangeCooldownTime()
    {
        cooldownTime= ability.cooldown / (1 + coolDownLevel * 0.2f);
    }
  
    
}
