
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject skillPanel,skillTrees,skillHolder;
    public GameObject menuPanel;
    public static event Action<bool> OnToggleMenu;
    public void ToggleSkillPanel()
    {
        // Toggle the visibility of the skill panel
        skillTrees.SetActive(false);
        skillPanel.SetActive(!skillPanel.activeSelf);
        skillHolder.SetActive(skillPanel.activeSelf);

        OnToggleMenu?.Invoke(!skillPanel.activeSelf && !menuPanel.activeSelf);

        UISkillController.UpdatePoints();
    }

    public void ToggleMenuPanel(InputAction.CallbackContext context)
    {
        // Toggle the visibility of the skill panel
        menuPanel.SetActive(!menuPanel.activeSelf);
        OnToggleMenu?.Invoke(!skillPanel.activeSelf && !menuPanel.activeSelf);

    }
    public void CloseSkillPanel()
    {
        if(!skillTrees.activeSelf) {skillPanel.SetActive(false);}
        OnToggleMenu?.Invoke(!skillPanel.activeSelf);
    }
}
