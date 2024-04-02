using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownPowerUp : IPowerUp
{
    
    public void Apply(AbilityContainer abilityContainer)
    {
        abilityContainer.coolDownLevel++;
    }
}
