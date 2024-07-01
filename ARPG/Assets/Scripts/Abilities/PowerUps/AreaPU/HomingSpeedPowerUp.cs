using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingSpeedPowerUp: IPowerUp
{
    private int additionalSpeed;
    public HomingSpeedPowerUp(int additionalSpeed)
    {
        this.additionalSpeed = additionalSpeed;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is AreaAbilityContainer)
        {

            AreaAbilityContainer areaAbilityContainer = (AreaAbilityContainer)abilityContainer;
            areaAbilityContainer.homingSpeed += additionalSpeed;
        }
    }

    public void Decrease(AbilityContainer abilityContainer)
    {
        if (abilityContainer is AreaAbilityContainer)
        {

            AreaAbilityContainer areaAbilityContainer = (AreaAbilityContainer)abilityContainer;
            areaAbilityContainer.homingSpeed -= additionalSpeed;
        }
    }
}
