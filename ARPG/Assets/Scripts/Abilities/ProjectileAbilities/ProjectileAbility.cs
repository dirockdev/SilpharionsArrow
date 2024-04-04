using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class ProjectileAbility :Ability, IAbilityBehaviour
{
    
    public int numberOfProjectiles;
    public bool canPenetrate;
    public GameObject prefabProjectile;
    public int damage;
    public int speed;
    public int critProb;
    public float angleProj;
    public float slow;
    public void ExecuteAbility(Transform initialPos, Vector3 mousePos,AbilityContainer abilityContainer)
    {
        ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
        // Calcular el �ngulo entre los proyectiles
        float angleBetweenProjectiles = projectileAbilityContainer.angleProj; // Por ejemplo, podr�as ajustar este valor seg�n el cono deseado


        // Normalizar la direcci�n una vez fuera del bucle
        Vector3 direction = (mousePos - initialPos.position).normalized;

        // Calcular el vector horizontal una vez fuera del bucle
        Vector3 horizontalDirection = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;

        // Calcular la rotaci�n inicial una vez fuera del bucle
        Quaternion initialRotation = Quaternion.AngleAxis(-(projectileAbilityContainer.numProjectiles - 1) * angleBetweenProjectiles / 2f, Vector3.up);

        for (int i = 0; i < projectileAbilityContainer.numProjectiles; i++)
        {
            // Rotar la direcci�n del proyectil basada en el �ngulo
            Quaternion rotation = Quaternion.AngleAxis(i * angleBetweenProjectiles, Vector3.up) * initialRotation;


            // Instanciar el proyectil en la direcci�n calculada
            GameObject projectile = ObjectPoolManager.SpawnObject(prefabProjectile, new Vector3(initialPos.position.x, 2, initialPos.position.z), Quaternion.identity);
            // Configurar el da�o, velocidad, etc., seg�n sea necesario

            IProjectile projectileComponent = projectile.GetComponent<IProjectile>();
            projectileComponent.SetDamage(projectileAbilityContainer.currentDamage);
            projectileComponent.SetVelocity(projectileAbilityContainer.speed);
            projectileComponent.SetCanPenetrate(canPenetrate);
            projectileComponent.SetCrit(projectileAbilityContainer.probCrit);
            projectileComponent.SetSlow(projectileAbilityContainer.slowFactor);
            projectile.GetComponent<ProjectController1>().SetDirection(rotation*horizontalDirection);

            
        }
    }

}