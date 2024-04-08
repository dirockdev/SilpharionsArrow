using System;

[Serializable]
public class AreaAbilityContainer : AbilityContainer
{
    public int tickDamage;
    public int area;
    public float timeAlive;
    
    public AreaAbilityContainer(Ability ability) : base(ability)
    {
        AreaAbility areaAbility = (AreaAbility)ability;
        area = areaAbility.area;
        tickDamage = areaAbility.tickDamage;
        timeAlive = areaAbility.timeAlive;
    }

}
