using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerExp : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Slider expBar;
    [SerializeField]
    private TMP_Text levelText;

    [Range(1f, 300f)]
    float additionMultiplier = 300;
    [Range(2f, 4f)]
    float powerMultiplier = 2;
    [Range(7f, 14f)]
    float divisionMultiplier = 7;
    const string experienceTag = "Experience";


    int currentExp;
    int requiredExp;
    public static int level=1;


    public static event Action<int> OnLevelUp;
    private void Awake()
    {
        RestartStats();
        expBar.maxValue = requiredExp;
        expBar.minValue = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(experienceTag))
        {
            RewardItem ExperienceOBJ = other.GetComponentInParent<RewardItem>();
            IncreaseExp(ExperienceOBJ.experience);
            ObjectPoolManager.ReturnObjectToPool(ExperienceOBJ.gameObject);
            AudioManager.instance.PlaySFXWorld("5", other.transform.position);
        }
    }
    private void IncreaseExp(int expAmount)
    {

        currentExp += expAmount;
        expBar.value = currentExp;
        if (currentExp >= requiredExp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        SkillTree.skillPoints++;
        currentExp = Mathf.RoundToInt(currentExp - requiredExp);
        requiredExp = CalculateRequiredExp();
        UpdateExpUI();
        UISkillController.UpdatePoints();
        AudioManager.instance.PlaySFXWorld("4",default,2.5f,0.07f);

        OnLevelUp?.Invoke(level);

    }

    private void UpdateExpUI()
    {
        expBar.value = currentExp;
        expBar.maxValue = requiredExp;
        levelText.SetText(level.ToString());
    }

    private int CalculateRequiredExp()
    {
        return Mathf.FloorToInt(level + additionMultiplier * Mathf.Pow(powerMultiplier, level / divisionMultiplier));
    }
    void RestartStats()
    {
        
        currentExp = 0;
        expBar.value = currentExp;
        level = 1;
        requiredExp = CalculateRequiredExp();
        expBar.maxValue = requiredExp;
    }

}
