using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionAbilityContainer : AbilityContainer
{
    int healthAmount;
    public PotionAbilityContainer(Ability ability) : base(ability)
    {
        PotionAbility potionAbility= (PotionAbility)ability;
        healthAmount = potionAbility.healthAmount;

    }


}
