public  class AreaTimeDuration:IPowerUp
{
    private float timeDuration;

    public AreaTimeDuration(float timeDuration)
    {
        this.timeDuration = timeDuration;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is AreaAbilityContainer)
        {

            AreaAbilityContainer areaAbilityContainer = (AreaAbilityContainer)abilityContainer;
            areaAbilityContainer.timeAlive += timeDuration;
        }
    }
}