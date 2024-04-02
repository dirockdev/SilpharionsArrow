using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[Serializable]
public abstract class AbilityContainer
{
    public Ability ability;
    public bool isPressed;
    public float currentCooldown;
    public float CooldownNormalized { get { return 1f - currentCooldown / GetModifiedCooldown(); } }

    public int coolDownLevel;

    public AbilityContainer(Ability ability)
    {
        this.ability = ability;
    }

    public AbilityContainer(Ability ability, float currentCooldown)
    {
        this.ability = ability;
        this.currentCooldown = currentCooldown;

    }

    internal void Cooldown()
    {
        currentCooldown = GetModifiedCooldown();
    }

    internal void ReduceCooldown(float deltaTime)
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= deltaTime;
        }

    }
    private float GetModifiedCooldown()
    {
        return ability.cooldown / (1 + coolDownLevel * 0.2f); // Reduce el cooldown en un 20% por nivel de power-up
    }
    
}
