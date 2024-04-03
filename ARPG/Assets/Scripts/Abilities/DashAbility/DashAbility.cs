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
        InstancePlayer.instance.GetComponentInChildren<Animator>().SetTrigger("Teleport");
        DashAbilityContainer dashAbilityContainer = (DashAbilityContainer)abilityContainer;

        InstancePlayer.instance.GetComponentInChildren<AnimationHandler>().TeleportBurst(dashAbilityContainer.movSpeed, dashAbilityContainer.timeSpeedBurst);

        CharacterStats CharacterStats =InstancePlayer.instance.GetComponent<CharacterStats>();
        CharacterStats.GetHeal(dashAbilityContainer.healAmount);


    }
    

}
