using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunProbPowerUp : IPowerUp
{
    private float probStun;
    public StunProbPowerUp(float probStun)
    {
        this.probStun = probStun;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.stunProb += probStun;
        }
    }
}
