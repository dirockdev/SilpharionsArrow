using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class DashAbility : Ability, IAbilityBehaviour
{
    public int healAmount;
    public int cooldownAbilities;
    public int moveSpeed;
    public int timeSpeedBurst;
    
    public void ExecuteAbility(Transform initialPos, Vector3 mousePos, AbilityContainer abilityContainer)
    {
        DashAbilityContainer dashAbilityContainer = (DashAbilityContainer)abilityContainer;
        dashAbilityContainer.anim.SetTrigger("Teleport");

        dashAbilityContainer.animationHandler.TeleportBurst(dashAbilityContainer.movSpeed, dashAbilityContainer.timeSpeedBurst);

        CharacterStats CharacterStats =InstancePlayer.instance.GetComponent<CharacterStats>();
        if(dashAbilityContainer.healAmount>0)CharacterStats.GetHeal(dashAbilityContainer.healAmount);

        if (abilityContainer.canReduceCooldown)
        {
            foreach (var item in dashAbilityContainer.abilityHandler.Abilities)
            {
                item.currentCooldown -= 1f;
            } 
        }
    }
    

}
