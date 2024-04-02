using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritPowerUp : IPowerUp
{
    private int additionalCrit;
    public CritPowerUp(int additionalCrit)
    {
        this.additionalCrit = additionalCrit;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.probCrit += additionalCrit;
        }
    }

}
