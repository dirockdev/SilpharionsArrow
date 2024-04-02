using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : IPowerUp
{
    private int additionalSpeed;
    public SpeedPowerUp(int additionalSpeed)
    {
        this.additionalSpeed = additionalSpeed;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.speed += additionalSpeed;
        }
    }
}
