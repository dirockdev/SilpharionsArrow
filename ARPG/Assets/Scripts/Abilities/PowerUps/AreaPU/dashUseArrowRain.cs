using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashUseArrowRain : IPowerUp
{
    private bool canUseArea;
    public dashUseArrowRain(bool canUseArea)
    {
        this.canUseArea = canUseArea;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is DashAbilityContainer)
        {

            DashAbilityContainer projectileAbilityContainer = (DashAbilityContainer)abilityContainer;
            projectileAbilityContainer.canArrowRain = canUseArea;
        }
    }
}
