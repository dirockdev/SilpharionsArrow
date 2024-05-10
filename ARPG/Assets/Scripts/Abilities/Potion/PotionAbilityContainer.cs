using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PotionAbilityContainer : AbilityContainer
{
    int healthAmount;
    public PotionAbilityContainer(Ability ability) : base(ability)
    {
        PotionAbility potionAbility= (PotionAbility)ability;
        healthAmount = potionAbility.healthAmount;
        AudioManager.instance.PlaySFXWorld("9");
    }


}
