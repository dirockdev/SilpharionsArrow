using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] List<AbilitySlotButton> slotButtons;
    [SerializeField]
    private AbilityHandler abilityHandler;
    [SerializeField]private GameObject abilitySkillTrees;
    public static event Action onInicializeSkills;

    [SerializeField]InputReader inputReader;
    private void Awake()
    {
        if (abilitySkillTrees.activeSelf) { abilitySkillTrees.SetActive(false); };
        if (!abilitySkillTrees.activeSelf) { 
            abilitySkillTrees.SetActive(true); 
            abilitySkillTrees.SetActive(false);
        }
            onInicializeSkills?.Invoke();

        inputReader.OnInputAbility0 += ActivateAbilitySecondary;
        inputReader.OnInputAbility1 += ActivateAbility1;
        inputReader.OnInputAbility2 += ActivateAbility2;
        inputReader.OnInputAbility3 += ActivateAbility3;
        inputReader.OnInputAbility4 += ActivateAbility4;
        inputReader.OnUsePotion += UsePotion; 
        AbilityHandler.onCooldownUpdate += UpdateCooldown;
        AbilityHandler.onAbilityChange += UpdateAbility;
    }

    private void OnDisable()
    {
        inputReader.OnInputAbility0 -= ActivateAbilitySecondary;
        inputReader.OnInputAbility1 -= ActivateAbility1;
        inputReader.OnInputAbility2 -= ActivateAbility2;
        inputReader.OnInputAbility3 -= ActivateAbility3;
        inputReader.OnInputAbility4 -= ActivateAbility4;
        inputReader.OnUsePotion -= UsePotion;
        AbilityHandler.onCooldownUpdate -= UpdateCooldown;
        AbilityHandler.onAbilityChange -= UpdateAbility;

    }

    public void ActivateAbility1(InputAction.CallbackContext context)
    {
        
        HoldAbilities(context,1);
    }

    public void ActivateAbility2(InputAction.CallbackContext context)
    {
        HoldAbilities(context,2);
      
    }
    public void ActivateAbility3(InputAction.CallbackContext context)
    {
        HoldAbilities(context,3);
        
    }
    public void ActivateAbility4(InputAction.CallbackContext context)
    {

        HoldAbilities(context,4);
        
    }
    public void ActivateAbilitySecondary(InputAction.CallbackContext context)
    {
        HoldAbilities(context,0);
        
    }
    public void UsePotion(InputAction.CallbackContext context)
    {
        HoldAbilities(context, 5);
    }
    private void HoldAbilities(InputAction.CallbackContext context,int abilityID)
    {
        
        if (context.started)
        {
            abilityHandler.ActivateAbility(abilityID, true);
        }
        if (context.canceled)
        {
            abilityHandler.ActivateAbility(abilityID, false);
           
        }
    }

    public void UpdateAbility(AbilityContainer ability, int abilitySlotID)
    {
        slotButtons[abilitySlotID].UpdateAbility(ability);
    }
    public void UpdateCooldown(float coolDownNormalized, int abilitySlotId)
    {
        slotButtons[abilitySlotId].UpdateCooldown(coolDownNormalized);
    }
}
