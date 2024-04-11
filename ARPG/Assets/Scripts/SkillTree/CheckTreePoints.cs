using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTreePoints : MonoBehaviour
{
    SkillInitializer skillData;
    int maxPointInSkillTree = 0;
    int pointsInSkillTree=0;
    [SerializeField]
    int idAbility;
    public void CountMaxPoints()
    {
        maxPointInSkillTree=0;
        skillData = GetComponent<SkillInitializer>();
        foreach (var item in skillData.skillData)
        {
            maxPointInSkillTree += item.maxLevel;
        }
    }
    public void CheckAllPoints()
    {
        pointsInSkillTree = 1;
        foreach (var item in skillData.skills)
        {
            pointsInSkillTree += item.Level;
        }

        if (pointsInSkillTree == maxPointInSkillTree)
        {
            skillData.skills[idAbility].SkillButton.enabled = true;
            skillData.skills[idAbility].SkillImage.color = Color.green;
        }
    }

}
