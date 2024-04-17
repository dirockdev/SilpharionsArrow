using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public PlayerExp playerExp;
    public AbilityHandler abilityHandler;

    public SkillTree[] skillTrees;
    public void Save()
    {
        playerExp.SerializeJson();
        abilityHandler.SerializeJson();
        foreach (SkillTree skillTree in skillTrees)
        {
            skillTree.SerializeJson();
        }
    }public void Load()
    {
        playerExp.DeserializeJson();
        abilityHandler.DesSerializeJson();
        foreach(SkillTree skillTree in skillTrees)
        {
            skillTree.DeserializeJson();
        }
    }

}
