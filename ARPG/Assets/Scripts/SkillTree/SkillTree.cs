using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillTree : MonoBehaviour
{

    public static int skillPoints = 30;
    public List<Skill> skillList;

    private bool initialized = false;
    CheckTreePoints treePoints;
    public bool Initialized { get => initialized; set => initialized = value; }

    private void Start()
    {
        UISkillController.UpdatePoints();
        treePoints = GetComponent<CheckTreePoints>();
    }
    private void OnEnable()
    {
        if (Initialized)UpdateAllSkillsUI();
    }
    public void UpdateAllSkillsUI()
    {
        foreach (var skill in skillList)
        {
            skill.UpdateUI();
        }
        UISkillController.UpdatePoints();
        if (treePoints != null)treePoints.CheckAllPoints();

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
}
