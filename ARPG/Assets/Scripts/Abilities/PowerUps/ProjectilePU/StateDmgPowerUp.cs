using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDmgPowerUp : IPowerUp
{
    private int stateDmgDiv;
    public StateDmgPowerUp(int stateDmgDiv)
    {
        this.stateDmgDiv = stateDmgDiv;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.stateDmg -= stateDmgDiv;
        }
    }
}
