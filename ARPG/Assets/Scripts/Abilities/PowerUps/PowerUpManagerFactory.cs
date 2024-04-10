using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpManagerFactory : MonoBehaviour
{
    private AbilityHandler abilityHandler;
    [SerializeField]
    PowerUpStats powerUpStats;

    public static PowerUpManagerFactory instance;
    private Dictionary<string, IPowerUp> factories = new Dictionary<string, IPowerUp>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
        private void Start()
    {
        abilityHandler = GetComponent<AbilityHandler>();
        InitializeFactories();
    }

    private void InitializeFactories()
    {
        //Projectiles
        factories.Add("Cooldown", new CooldownPowerUp());
        factories.Add("Projectile", new ProjectilePowerUp(powerUpStats.projectiles));
        factories.Add("Crit", new CritPowerUp(powerUpStats.crit));
        factories.Add("Angle", new AnglePowerUp(powerUpStats.angle));
        factories.Add("Slow", new StatePowerUp(powerUpStats.slow));
        factories.Add("Damage", new DamagePowerUp(powerUpStats.damage));
        factories.Add("Speed", new SpeedPowerUp(powerUpStats.speed));
        factories.Add("PoisonState", new StatePowerUp(powerUpStats.poisonChance));
        factories.Add("PoisonStateDmg", new StateDmgPowerUp(powerUpStats.stateDmgDiv));
        factories.Add("Width", new WidthPowerUp(powerUpStats.widthProj));
        factories.Add("StunProb", new StunProbPowerUp(powerUpStats.stunProb));
        //ShotGun

        factories.Add("ProjectilesShotGun", new ProjectilePowerUp(powerUpStats.projectilesShotGun));
        factories.Add("DamageShotGun", new DamagePowerUp(powerUpStats.damageShotGun));
        factories.Add("StatePart", new StatePowerUp(powerUpStats.speedPartShotGun));
        factories.Add("CritProbShotGun", new CritPowerUp(powerUpStats.critShotGun));
        factories.Add("AngleProjShotGun", new AnglePowerUp(powerUpStats.angleShotGun));
        factories.Add("WidthShotGun", new WidthPowerUp(powerUpStats.widthProjShotGun));
        factories.Add("SpeedShotGun", new SpeedPowerUp(powerUpStats.speedShotGun));
        factories.Add("CanHealShotGun", new CanHealPowerUp(true));
        factories.Add("CanReduceCooldown", new CooldownReducePowerUp(true));

        //Area
        factories.Add("TickDamage", new TickDamagePowerUp(powerUpStats.damageTick));
        factories.Add("isHoming", new isHomingPowerUp(true));
        factories.Add("HomingSpeed", new HomingSpeedPowerUp(powerUpStats.homingSpeed));
        factories.Add("Area", new AreaPowerUp(powerUpStats.areaSize));
        factories.Add("TimeDuration", new AreaTimeDuration(powerUpStats.timeDuration));
        factories.Add("CanUseArea", new dashUseArrowRain(true));

        //Dash

        factories.Add("MovSpeed", new MovSpeedPowerUp(powerUpStats.movSpeed));
        factories.Add("MovSpeedTime", new TimeSpeedBurstPowerUp(powerUpStats.timeSpeed));
        factories.Add("HealthHeal", new HealPowerUp(powerUpStats.healthAmount));

    }

    public void UpgradePowerUp(string powerUpType, int idAbility)
    {
        if (!factories.ContainsKey(powerUpType))
        {
            Debug.LogError("Invalid powerUpType.");
            return;
        }

        UpgradeAbility(idAbility, factories[powerUpType]);
    }

    private void UpgradeAbility(int idAbility, IPowerUp powerUp)
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
