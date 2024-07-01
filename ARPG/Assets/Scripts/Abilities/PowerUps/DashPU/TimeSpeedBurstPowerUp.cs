using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeedBurstPowerUp : IPowerUp
{
    private int movSpeedTime;
    public TimeSpeedBurstPowerUp(int movSpeed)
    {
        this.movSpeedTime = movSpeed;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is DashAbilityContainer)
        {

            DashAbilityContainer dashAbility = (DashAbilityContainer)abilityContainer;
            dashAbility.timeSpeedBurst += movSpeedTime;
        }
    }

    public void Decrease(AbilityContainer abilityContainer)
    {
        if (abilityContainer is DashAbilityContainer)
        {

            DashAbilityContainer dashAbility = (DashAbilityContainer)abilityContainer;
            dashAbility.timeSpeedBurst += movSpeedTime;
        }
    }
}
