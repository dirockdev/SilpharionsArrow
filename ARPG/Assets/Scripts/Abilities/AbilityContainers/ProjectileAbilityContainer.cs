using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class ProjectileAbilityContainer : AbilityContainer
{
    
    public int probCrit;
    
    public int currentDamage;
    
    public int numProjectiles;
    
    public float angleProj;
    
    public int speed;
    
    public float state;
    
    public int stateDmg;
    
    public float widthProj;
    
    public float timeAlive;
    
    public bool canHealOnCrits;
    
    public float stunProb;
    
    public float animSpeed;
    public ProjectileAbilityContainer(Ability ability) : base(ability)
    {
        ProjectileAbility projectileAbility = (ProjectileAbility)ability;
        probCrit = projectileAbility.critProb;
        currentDamage = projectileAbility.damage;
        numProjectiles = projectileAbility.numberOfProjectiles;
        speed = projectileAbility.speed;
        angleProj = projectileAbility.angleProj;
        state = projectileAbility.state;
        stateDmg = projectileAbility.stateDmg;
        widthProj = projectileAbility.widthProj;
        timeAlive = projectileAbility.timeAlive;
        canHealOnCrits=projectileAbility.canHealOnCrits;
        canReduceCooldown = projectileAbility.canReduceCooldown;
        stunProb = projectileAbility.stunProb;
        animSpeed = 1;
    }

    public void SetLoadValues(ProjectileAbilityContainer loaded)
    {
        probCrit = loaded.probCrit;
        currentDamage = loaded.currentDamage;
        numProjectiles = loaded.numProjectiles;
        angleProj = loaded.angleProj;
        speed = loaded.speed;
        state = loaded.state;
        stateDmg = loaded.stateDmg;
        widthProj = loaded.widthProj;
        timeAlive = loaded.timeAlive;
        canHealOnCrits = loaded.canHealOnCrits;
        stunProb = loaded.stunProb;
        animSpeed = loaded.animSpeed;
        coolDownLevel = loaded.coolDownLevel;
        cooldownTime = loaded.cooldownTime;
        isUnlocked = loaded.isUnlocked;
    }
    

}
