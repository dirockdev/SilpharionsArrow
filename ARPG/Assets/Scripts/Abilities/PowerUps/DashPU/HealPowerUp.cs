using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp : IPowerUp
{
    private int healthAmount;
    public HealPowerUp(int healthAmount)
    {
        this.healthAmount = healthAmount;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is DashAbilityContainer)
        {

            DashAbilityContainer dashAbility = (DashAbilityContainer)abilityContainer;
            dashAbility.healAmount += healthAmount;
        }
    }

    public void Decrease(AbilityContainer abilityContainer)
    {
        if (abilityContainer is DashAbilityContainer)
        {

            DashAbilityContainer dashAbility = (DashAbilityContainer)abilityContainer;
            dashAbility.healAmount -= healthAmount;
        }
    }
}
