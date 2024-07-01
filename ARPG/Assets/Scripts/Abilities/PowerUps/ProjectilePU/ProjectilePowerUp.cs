

public class ProjectilePowerUp : IPowerUp
{
    private int additionalProjectiles;
    public ProjectilePowerUp(int additionalProjectiles)
    {
        this.additionalProjectiles = additionalProjectiles;

    }

    public void Apply(AbilityContainer abilityContainer)
    {
        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.numProjectiles += additionalProjectiles;
        }
    }

    public void Decrease(AbilityContainer abilityContainer)
    {

        if (abilityContainer is ProjectileAbilityContainer)
        {

            ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
            projectileAbilityContainer.numProjectiles -= additionalProjectiles;
        }

    }
}
