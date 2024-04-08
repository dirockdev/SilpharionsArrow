using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownReducePowerUp : IPowerUp
{
    private bool canReduceCooldown;
    public CooldownReducePowerUp(bool canReduceCooldown)
    {
        this.canReduceCooldown = canReduceCooldown;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        abilityContainer.canReduceCooldown = canReduceCooldown;
        
    }
}
