using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidthPowerUp : IPowerUp
{
    private float widthProj;
    public WidthPowerUp(float widthProj)
    {
        this.widthProj = widthProj;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.widthProj += widthProj;
        }
    }
}
