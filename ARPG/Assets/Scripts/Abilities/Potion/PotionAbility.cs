using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PotionAbility : Ability, IAbilityBehaviour
{
    public int healthAmount;
   

    public static event Action<int> onPotionUse;
    public void ExecuteAbility(Transform initialPos, Vector3 mousePos, AbilityContainer abilityContainer)
    {
        onPotionUse?.Invoke(healthAmount);
    }
}
