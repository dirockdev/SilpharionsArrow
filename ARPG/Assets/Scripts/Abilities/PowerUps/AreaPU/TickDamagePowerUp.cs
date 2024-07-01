using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickDamagePowerUp : IPowerUp
{
    private int additionalDamage;
    public TickDamagePowerUp(int additionalDamage)
    {
        this.additionalDamage = additionalDamage;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is AreaAbilityContainer)
        {

            AreaAbilityContainer areaAbilityContainer = (AreaAbilityContainer)abilityContainer;
            areaAbilityContainer.tickDamage += additionalDamage;
        }
    }

    public void Decrease(AbilityContainer abilityContainer)
    {
        if (abilityContainer is AreaAbilityContainer)
        {

            AreaAbilityContainer areaAbilityContainer = (AreaAbilityContainer)abilityContainer;
            areaAbilityContainer.tickDamage -= additionalDamage;
        }
    }
}
