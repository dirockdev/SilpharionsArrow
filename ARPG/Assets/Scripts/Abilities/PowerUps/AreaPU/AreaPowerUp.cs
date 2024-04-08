public class AreaPowerUp:IPowerUp
{
    private int areaSize;

    public AreaPowerUp(int areaSize)
    {
        this.areaSize = areaSize;
    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is AreaAbilityContainer)
        {

            AreaAbilityContainer areaAbilityContainer = (AreaAbilityContainer)abilityContainer;
            areaAbilityContainer.area += areaSize;
        }
    }
}