using System;

[Serializable]
public class ProjectileAbilityContainer : AbilityContainer
{
    public int probCrit;
    public int currentDamage;
    public int numProjectiles;
    public float angleProj;
    public int speed;
    public float slowFactor;

    public ProjectileAbilityContainer(Ability ability) : base(ability)
    {
        ProjectileAbility projectileAbility = (ProjectileAbility)ability;
        probCrit = projectileAbility.critProb;
        currentDamage = projectileAbility.damage;
        numProjectiles = projectileAbility.numberOfProjectiles;
        speed = projectileAbility.speed;
        angleProj = projectileAbility.angleProj;
        slowFactor = projectileAbility.slow;

    }

}
