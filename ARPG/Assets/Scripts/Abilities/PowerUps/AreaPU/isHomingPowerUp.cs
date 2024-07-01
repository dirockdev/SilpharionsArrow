using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isHomingPowerUp : IPowerUp
{
    private bool isHoming;
    public isHomingPowerUp(bool isHoming)
    {
        this.isHoming = isHoming;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is AreaAbilityContainer)
        {

            AreaAbilityContainer areaAbilityContainer = (AreaAbilityContainer)abilityContainer;
            areaAbilityContainer.isHoming = isHoming;
        }
    }

    public void Decrease(AbilityContainer abilityContainer)
    {
        if (abilityContainer is AreaAbilityContainer)
        {

            AreaAbilityContainer areaAbilityContainer = (AreaAbilityContainer)abilityContainer;
            areaAbilityContainer.isHoming = !isHoming;
        }
    }
}
