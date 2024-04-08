using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AreaAbility : Ability, IAbilityBehaviour
{
    public int tickDamage;
    public int area;
    public float timeAlive;
    public GameObject prefabArea;
    public void ExecuteAbility(Transform initialPos, Vector3 mousePos, AbilityContainer abilityContainer)
    {
        AreaAbilityContainer areaAbilityContainer=(AreaAbilityContainer)abilityContainer;

        GameObject areaGameObject = ObjectPoolManager.SpawnObject(prefabArea, new Vector3(MouseInput.rayToWorldPoint.x, 0, MouseInput.rayToWorldPoint.z), Quaternion.identity);

        IArea areaComponent = areaGameObject.GetComponent<IArea>();

        areaComponent.SetArea(areaAbilityContainer.area);    
        areaComponent.SetDamage(areaAbilityContainer.tickDamage);    
        areaComponent.SetTimeAlive(areaAbilityContainer.timeAlive);    

    }
}

