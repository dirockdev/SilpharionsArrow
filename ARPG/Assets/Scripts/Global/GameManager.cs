
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject skillPanel,skillTrees,skillHolder;
    public void ToggleSkillPanel(InputAction.CallbackContext context)
    {
        // Toggle the visibility of the skill panel
        skillTrees.SetActive(false);
        skillPanel.SetActive(!skillPanel.activeSelf);
        skillHolder.SetActive(skillPanel.activeSelf);
        
        UISkillController.UpdatePoints();
    }
}
