using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

[System.Serializable]
public class SkillData
{
    public int id;
    public string skillName;
    public string skillDescription;
    public int maxLevel;
    public int points;
    public List<int> connectedSkillIds;
    public int pointsRequiredForConnectedSkills;

    
}

public class SkillInitializer : MonoBehaviour
{
    public List<Skill> skills;
    public List<SkillData> skillData;
    void OnEnable()
    {
        AbilityPanel.onInicializeSkills += InitializeSkills;
        
    }

    void InitializeSkills()
    {
        if (skills.Count != skillData.Count)
        {
            Debug.LogError("Number of skills and skill data do not match!");
            return;
        }

      
        for (int i = 0; i < skills.Count; i++)
        {
            Skill skill = skills[i];
            SkillData data = skillData[i];


            skill.Id = data.id;
            skill.SkillName = data.skillName;
            skill.SkillDescription = data.skillDescription;
            skill.MaxLevel = data.maxLevel;
            skill.Level = data.points;
            skill.RequiredPointsForConnectedSkills = data.pointsRequiredForConnectedSkills;

            skill.ConnectedSkills = new List<Skill>();

        }
        Dictionary<int, Skill> skillDictionary = new Dictionary<int, Skill>();
        foreach (Skill skill in skills)
        {
            skillDictionary.Add(skill.Id, skill);
        }

        for (int i = 0; i < skills.Count; i++)
        {
            Skill skill = skills[i];
            SkillData data = skillData[i];

            foreach (int connectedSkillId in data.connectedSkillIds)
            {
                if (skillDictionary.ContainsKey(connectedSkillId))
                {
                    skill.ConnectedSkills.Add(skillDictionary[connectedSkillId]);
                }
                else
                {
                    Debug.LogWarning("Connected skill with ID " + connectedSkillId + " not found for skill " + skill.Id);
                }
            }

        }
        foreach(Skill skill in skills)skill.Initialize();
       
    }
}
