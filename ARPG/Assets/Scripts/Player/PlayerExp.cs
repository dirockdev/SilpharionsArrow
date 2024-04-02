using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExp : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Slider expBar;

    [Range(1f, 300f)]
    float additionMultiplier = 300;
    [Range(2f, 4f)]
    float powerMultiplier = 2;
    [Range(7f, 14f)]
    float divisionMultiplier = 7;
    const string experienceTag = "Experience";


    int currentExp;
    int requiredExp;
    private static int level;

    
    private void Start()
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
        expBar.value = currentExp;
        expBar.maxValue = requiredExp;
        UISkillController.UpdatePoints();
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
