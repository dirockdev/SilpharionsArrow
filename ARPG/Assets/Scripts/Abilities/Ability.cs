using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public abstract class Ability : ScriptableObject
{
    [JsonIgnore]
    public Sprite icon;
    public float cooldown;


    public void UseAbility(Transform initialPos, IAbilityBehaviour behaviour, Vector3 mousePosition, AbilityContainer abilityContainer)
    {
        // Ejecutar la acción específica de la habilidad
        behaviour.ExecuteAbility(initialPos,mousePosition,abilityContainer);
    }
    
}
