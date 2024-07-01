using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovSpeedPowerUp : IPowerUp
{
    private int movSpeed;
    public MovSpeedPowerUp(int movSpeed)
    {
        this.movSpeed = movSpeed;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is DashAbilityContainer)
        {

            DashAbilityContainer dashAbility = (DashAbilityContainer)abilityContainer;
            dashAbility.movSpeed += movSpeed;
        }
    }

    public void Decrease(AbilityContainer abilityContainer)
    {
        if (abilityContainer is DashAbilityContainer)
        {

            DashAbilityContainer dashAbility = (DashAbilityContainer)abilityContainer;
            dashAbility.movSpeed -= movSpeed;
        }
    }
}
