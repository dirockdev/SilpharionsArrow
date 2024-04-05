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
    public float state;
    public int stateDmg;
    public float widthProj;
    public float timeAlive;
    public void ExecuteAbility(Transform initialPos, Vector3 mousePos,AbilityContainer abilityContainer)
    {
        ProjectileAbilityContainer projectileAbilityContainer = (ProjectileAbilityContainer)abilityContainer;
        // Calcular el ángulo entre los proyectiles
        float angleBetweenProjectiles = projectileAbilityContainer.angleProj; // Por ejemplo, podrías ajustar este valor según el cono deseado


        // Normalizar la dirección una vez fuera del bucle
        Vector3 direction = (mousePos - initialPos.position).normalized;

        // Calcular el vector horizontal una vez fuera del bucle
        Vector3 horizontalDirection = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;

        // Calcular la rotación inicial una vez fuera del bucle
        Quaternion initialRotation = Quaternion.AngleAxis(-(projectileAbilityContainer.numProjectiles - 1) * angleBetweenProjectiles / 2f, Vector3.up);

        for (int i = 0; i < projectileAbilityContainer.numProjectiles; i++)
        {
            // Rotar la dirección del proyectil basada en el ángulo
            Quaternion rotation = Quaternion.AngleAxis(i * angleBetweenProjectiles, Vector3.up) * initialRotation;


            // Instanciar el proyectil en la dirección calculada
            GameObject projectile = ObjectPoolManager.SpawnObject(prefabProjectile, new Vector3(initialPos.position.x, 2, initialPos.position.z), Quaternion.identity);
            
            

            IProjectile projectileComponent = projectile.GetComponent<IProjectile>();
            projectileComponent.SetWidthProj(projectileAbilityContainer.widthProj);
            projectileComponent.SetDamage(projectileAbilityContainer.currentDamage);
            projectileComponent.SetVelocity(projectileAbilityContainer.speed);
            projectileComponent.SetCanPenetrate(canPenetrate);
            projectileComponent.SetCrit(projectileAbilityContainer.probCrit);
            projectileComponent.SetState(projectileAbilityContainer.state);
            projectileComponent.SetDirection(rotation*horizontalDirection);
            projectileComponent.SetStateDmg(projectileAbilityContainer.currentDamage / projectileAbilityContainer.stateDmg);
            projectileComponent.SetTimeAlive(projectileAbilityContainer.timeAlive);
            
        }
    }

}
