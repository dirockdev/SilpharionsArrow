using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownPowerUp : IPowerUp
{
    public void Apply(AbilityContainer abilityContainer)
    {
        abilityContainer.coolDownLevel++;
        abilityContainer.ChangeCooldownTime();
        if (abilityContainer is ProjectileAbilityContainer)
        {
            ProjectileAbilityContainer projectileContainer= (ProjectileAbilityContainer)abilityContainer;
            projectileContainer.animSpeed+= projectileContainer.animSpeed*0.2f;
            
        }
    }
}
