using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePowerUp : IPowerUp
{
    private float slowFactor;
    public StatePowerUp(float slowFactor)
    {
        this.slowFactor = slowFactor;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.state += slowFactor;
        }
    }

    public void Decrease(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.state -= slowFactor;
        }
    }
}
