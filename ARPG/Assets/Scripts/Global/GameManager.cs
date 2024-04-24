
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject skillPanel,skillTrees,skillHolder;
    public GameObject menuPanel;
    public static event Action<bool> OnToggleMenu;

    [SerializeField] InputReader inputReader;
    private void Awake()
    {
        inputReader.OnInputOpenMenu += ToggleMenuPanel;

        inputReader.OnInputOpenSkills += ToggleSkillPanel;
    }
    private void OnDisable()
    {
        inputReader.OnInputOpenMenu -= ToggleMenuPanel;

        inputReader.OnInputOpenSkills -= ToggleSkillPanel;
        
    }
    public void ToggleSkillPanel()
    {
        // Toggle the visibility of the skill panel
        skillTrees.SetActive(false);
        skillPanel.SetActive(!skillPanel.activeSelf);
        skillHolder.SetActive(skillPanel.activeSelf);

        OnToggleMenu?.Invoke(!skillPanel.activeSelf && !menuPanel.activeSelf);

        UISkillController.UpdatePoints();
    }

    public void ToggleMenuPanel()
    {
        if (skillPanel.activeSelf) {
            skillPanel.SetActive(false);
            skillTrees.SetActive(false); 
            OnToggleMenu?.Invoke(!skillPanel.activeSelf && !menuPanel.activeSelf);
        }
        else
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
            OnToggleMenu?.Invoke(!skillPanel.activeSelf);

        }

    }
    public void CloseSkillPanel()
    {
        if(!skillTrees.activeSelf) {skillPanel.SetActive(false);}
        OnToggleMenu?.Invoke(!skillPanel.activeSelf);
    }  

    public void CloseSkillPanelInMenu()
    {
        if(skillTrees.activeSelf) {skillPanel.SetActive(false);}
        OnToggleMenu?.Invoke(!skillPanel.activeSelf);
    }
}
