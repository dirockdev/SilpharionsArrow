using Newtonsoft.Json;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [JsonIgnore]
    public Sprite icon;
    public float cooldown;
    public int mana;

    public void UseAbility(Transform initialPos, IAbilityBehaviour behaviour, Vector3 mousePosition, AbilityContainer abilityContainer)
    {
        // Ejecutar la acción específica de la habilidad
        behaviour.ExecuteAbility(initialPos,mousePosition,abilityContainer);
    }
    
}
