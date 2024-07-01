using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerUp : IPowerUp
{
    private int additionalDamage;
    public DamagePowerUp(int additionalDamage)
    {
        this.additionalDamage = additionalDamage;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.currentDamage += additionalDamage;
        }
    }

    public void Decrease(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.currentDamage -= additionalDamage;
        }
    }
}
