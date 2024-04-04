using System;

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
    }

}
