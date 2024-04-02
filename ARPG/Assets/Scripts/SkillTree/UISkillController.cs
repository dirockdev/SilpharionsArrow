using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISkillController : MonoBehaviour
{
    public static TMP_Text pointsText;
    private void Awake()
    {
        pointsText = GetComponentInChildren<TMP_Text>();
    }
    public static void UpdatePoints(){

        pointsText.SetText(SkillTree.skillPoints.ToString());
    }
}
