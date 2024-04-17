using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class SkillTree : MonoBehaviour
{
    public string treeName;
    public static int skillPoints = 30;
    public List<Skill> skillList;

    private bool initialized = false;
    CheckTreePoints treePoints;
    public bool Initialized { get => initialized; set => initialized = value; }

    IDataService dataService = new JsonDataService();
    private void Start()
    {
        UISkillController.UpdatePoints();
        treePoints = GetComponent<CheckTreePoints>();
    }
    private void OnEnable()
    {
        if (Initialized) UpdateAllSkillsUI();
    }
    public void UpdateAllSkillsUI()
    {
        foreach (var skill in skillList)
        {
            skill.UpdateUI();
        }
        UISkillController.UpdatePoints();
        if (treePoints != null) treePoints.CheckAllPoints();

    }

    // Método para aumentar el nivel de una habilidad
    public void LevelUpSkill(int skillId)
    {
        if (CantLevelUp(skillId)) return;


        skillPoints--;
        skillList[skillId].Level++;
        UpdateAllSkillsUI();

        if (CantLevelUp(skillId)) skillList[skillId].SkillButton.enabled = false;
    }

    public bool CantLevelUp(int skillId)
    {
        return skillPoints < 1 || skillList[skillId].Level >= skillList[skillId].MaxLevel;
    }
    public void SerializeJson()
    {
        int[] levels = new int[skillList.Count];
        for (int i = 0; i < skillList.Count; i++)
        {
            levels[i] = skillList[i].Level;
        }

        if (dataService.SaveData($"/player-skillTree-{treeName}.json", levels, true))
        {

            Debug.Log("Level saved successfully!");
        }

        else
        {
            Debug.Log("Could not save");
        }
    }
    public void DeserializeJson()
    {
        int[] savedLevels = dataService.LoadData<int[]>($"/player-skillTree-{treeName}.json", true);
        if (savedLevels != null && savedLevels.Length == skillList.Count)
        {
            for (int i = 0; i < skillList.Count; i++)
            {
                skillList[i].Level = savedLevels[i];
            }
            UpdateAllSkillsUI();
        }
        else
        {
            Debug.LogWarning("Failed to load skill tree data.");
        }

    }
}
