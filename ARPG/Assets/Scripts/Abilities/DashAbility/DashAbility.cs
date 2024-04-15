
using System;
using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability, IAbilityBehaviour
{
    public int healAmount;
    public int cooldownAbilities;
    public int moveSpeed;
    public int timeSpeedBurst;
    public bool canArrowRain;
    public static event Action<int,int> Dashing;
    public static event Action<int> onHealDashing;
    public void ExecuteAbility(Transform initialPos, Vector3 mousePos, AbilityContainer abilityContainer)
    {
        DashAbilityContainer dashAbilityContainer = (DashAbilityContainer)abilityContainer;

        Dashing?.Invoke(dashAbilityContainer.movSpeed,dashAbilityContainer.timeSpeedBurst);
        
        if(dashAbilityContainer.healAmount>0) onHealDashing?.Invoke(dashAbilityContainer.healAmount); 


        if (dashAbilityContainer.canArrowRain)
        {
            dashAbilityContainer.abilityHandler.ActivateAbility(dashAbilityContainer.abilityHandler.Abilities[4]);

        }

        if (abilityContainer.canReduceCooldown)
        {
            foreach (var item in dashAbilityContainer.abilityHandler.Abilities)
            {
                item.currentCooldown -= 1f;
            } 
        }
    }
    

}
