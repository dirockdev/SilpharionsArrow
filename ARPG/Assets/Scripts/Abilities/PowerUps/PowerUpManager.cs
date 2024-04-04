using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private AbilityHandler abilityHandler;
    [SerializeField]
    PowerUpStats powerUpStats;
    private void Start()
    {
        abilityHandler = GetComponent<AbilityHandler>();
    }
    public void UpgradeCooldown(int idAbility)
    {
        List<AbilityContainer> abilities = abilityHandler.Abilities;
        CooldownPowerUp cooldownPowerUp = new CooldownPowerUp();
        cooldownPowerUp.Apply(abilities[idAbility]);
    }

    #region ProjectilePowerUps
    public void UpgradeProjectiles(int idAbility)
    {
        UpgradeAbility(idAbility, new ProjectilePowerUp(powerUpStats.projectiles));
    }

    public void UpgradeCritProb(int idAbility)
    {
        UpgradeAbility(idAbility, new CritPowerUp(powerUpStats.crit));

    }
    public void UpgradeAngleProj(int idAbility)
    {
        UpgradeAbility(idAbility, new AnglePowerUp(powerUpStats.angle));

    }
    public void UpgradeSlow(int idAbility)
    {
        UpgradeAbility(idAbility, new StatePowerUp(powerUpStats.slow));

    }
    public void UpgradeDamage(int idAbility)
    {
        UpgradeAbility(idAbility, new DamagePowerUp(powerUpStats.damage));

    }
    public void UpgradeSpeed(int idAbility)
    {
        UpgradeAbility(idAbility, new SpeedPowerUp(powerUpStats.speed));

    }

    public void UpgradePoisonState(int idAbility)
    {
        UpgradeAbility(idAbility, new StatePowerUp(powerUpStats.poisonChance));

    } 
    public void UpgradePoisonStateDmg(int idAbility)
    {
        UpgradeAbility(idAbility, new StateDmgPowerUp(powerUpStats.stateDmgDiv));

    }
    public void UpgradeWidth(int idAbility)
    {
        UpgradeAbility(idAbility, new WidthPowerUp(powerUpStats.widthProj));

    }
    #endregion
    #region DashPowerUps

    public void UpgradeMovSpeed(int idAbility)
    {
        UpgradeAbility(idAbility, new MovSpeedPowerUp(powerUpStats.movSpeed));

    }
    public void UpgradeMovSpeedTime(int idAbility)
    {
        UpgradeAbility(idAbility, new TimeSpeedBurstPowerUp(powerUpStats.timeSpeed));

    }
    public void UpgradeHealthHeal(int idAbility)
    {
        UpgradeAbility(idAbility, new HealPowerUp(powerUpStats.healthAmount));

    }
    #endregion

    // Función genérica para realizar una mejora en una habilidad con un power-up específico
    private void UpgradeAbility<T>(int idAbility, T powerUp) where T : IPowerUp
    {
        List<AbilityContainer> abilities = abilityHandler.Abilities;

        if (idAbility < 0 || idAbility >= abilities.Count)
        {
            Debug.LogError("Invalid ability ID.");
            return;
        }

        AbilityContainer abilityContainer = abilities[idAbility];
        powerUp.Apply(abilityContainer);

    }

}
