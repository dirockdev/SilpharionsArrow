using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class SkillTree : MonoBehaviour
{
    public string treeName;
    public static int skillPoints = 0;
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

    private void LoadRequiredPointsImage()
    {
        foreach (var skill in skillList)
        {
            // Iterar sobre todos los niveles de la habilidad y actualizar las imágenes
            for (int i = 0; i < skill.MaxLevel; i++)
            {
                // Verificar si el índice está dentro de los límites del número de hijos
                if (i < skill.RequiredPointsPanel.childCount)
                {
                    // Actualizar la imagen solo si el nivel está desbloqueado
                    if (i < skill.Level)
                    {
                        Transform child = skill.RequiredPointsPanel.GetChild(i);
                        Image imageComponent = child.GetComponent<Image>();
                        if (imageComponent != null)
                        {
                            imageComponent.sprite = skill.NoRequiredPointsImage;

                            imageComponent.color = new Color(1.0f, 0.5f, 0.0f);
                        }
                    }
                }
                else
                {
                    continue;
                }
            }
        }
    }

    // Método para aumentar el nivel de una habilidad
    public void LevelUpSkill(int skillId)
    {
        if (CantLevelUp(skillId))
        {
            
            return;
        }

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
        try
        {

            int[] savedLevels = dataService.LoadData<int[]>($"/player-skillTree-{treeName}.json", true);
            if (savedLevels != null && savedLevels.Length == skillList.Count)
            {
                for (int i = 0; i < skillList.Count; i++)
                {
                    skillList[i].Level = savedLevels[i];
                }
                UpdateAllSkillsUI();
                LoadRequiredPointsImage();
            }
            else
            {
                Debug.LogWarning("Failed to load skill tree data.");
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
}
