using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglePowerUp : IPowerUp
{
    private int angleReduce;
    public AnglePowerUp(int angleReduce)
    {
        this.angleReduce = angleReduce;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.angleProj += angleReduce;
        }
    }
}
