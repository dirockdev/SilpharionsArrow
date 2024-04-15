
using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability, IAbilityBehaviour
{
    public int healAmount;
    public int cooldownAbilities;
    public int moveSpeed;
    public int timeSpeedBurst;
    public bool canArrowRain;

    public void ExecuteAbility(Transform initialPos, Vector3 mousePos, AbilityContainer abilityContainer)
    {
        DashAbilityContainer dashAbilityContainer = (DashAbilityContainer)abilityContainer;
        dashAbilityContainer.anim.SetTrigger("Teleport");
        
        dashAbilityContainer.animationHandler.TeleportBurst(dashAbilityContainer.movSpeed, dashAbilityContainer.timeSpeedBurst);

        CharacterStats characterStats =InstancePlayer.instance.GetComponent<CharacterStats>();
        if(dashAbilityContainer.healAmount>0)characterStats.GetHeal(dashAbilityContainer.healAmount);

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
