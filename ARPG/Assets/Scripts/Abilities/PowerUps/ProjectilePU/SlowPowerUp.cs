using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPowerUp : IPowerUp
{
    private float slowFactor;
    public SlowPowerUp(float slowFactor)
    {
        this.slowFactor = slowFactor;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.slowFactor += slowFactor;
        }
    }
}
