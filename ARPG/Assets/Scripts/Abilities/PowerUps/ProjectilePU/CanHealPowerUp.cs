using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHealPowerUp : IPowerUp
{
    private bool canHeal;
    public CanHealPowerUp(bool canHeal)
    {
        this.canHeal = canHeal;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.canHealOnCrits = canHeal;
        }
    }
}

