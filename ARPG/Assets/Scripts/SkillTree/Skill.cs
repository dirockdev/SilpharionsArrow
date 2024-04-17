using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{

    public TMP_Text titleText;
    public TMP_Text descriptionText;
    private Image skillImage;
    private Button skillButton;
    private SkillTree skillTree;

    private List<Skill> connectedSkills; // Variable para almacenar las habilidades conectadas
    private int id;
    private int maxLevel;
    private int level;

    private string skillName;
    private string skillDescription;
    private int requiredPointsForConnectedSkills;
    public Button SkillButton { get => skillButton; set => skillButton = value; }
    public int Id { get => id; set => id = value; }
    public int MaxLevel { get => maxLevel; set => maxLevel = value; }
    public int Level { get => level; set => level = value; }
    public string SkillName { get => skillName; set => skillName = value; }
    public string SkillDescription { get => skillDescription; set => skillDescription = value; }
    public int RequiredPointsForConnectedSkills { get => requiredPointsForConnectedSkills; set => requiredPointsForConnectedSkills = value; }
    public List<Skill> ConnectedSkills { get => connectedSkills; set => connectedSkills = value; }
    public Image SkillImage { get => skillImage; set => skillImage = value; }

    private void Awake()
    {
        SkillImage = GetComponent<Image>();
        skillButton = GetComponent<Button>();
        skillButton.onClick.AddListener(() => AudioManager.instance.PlaySFXWorld("3", transform.position));
        skillTree = GetComponentInParent<SkillTree>();
        skillButton.enabled = false;
        
    }
  
    public void Initialize()
    {
        UpdateUI();
        skillTree.Initialized = true;
    }

    public void UpdateUI()
    {
        if (id == 0 && SkillTree.skillPoints >= 1 && level<maxLevel)
        {
            skillButton.enabled =true ;

        }
        else if(SkillTree.skillPoints < 1 && id == 0)
        {
            skillButton.enabled =false ;
        }
        titleText.SetText($"{Level}/{MaxLevel}\n{SkillName}");
        descriptionText.SetText(SkillDescription);
        // Determina si la habilidad está desbloqueada para mejorar
        bool isUnlockedForUpgrade = SkillTree.skillPoints >= 1 && skillButton.enabled==true;

        // Determina si la habilidad está desbloqueada para mejorar las habilidades conectadas
        bool areConnectedSkillsUnlocked = Level >= RequiredPointsForConnectedSkills;

        SkillImage.color = Level >= MaxLevel ? Color.yellow :
                isUnlockedForUpgrade  ? Color.green : Color.grey;
       

        foreach (var connectedSkill in connectedSkills)
        {
            connectedSkill.skillButton.enabled = areConnectedSkillsUnlocked && SkillTree.skillPoints >= 1 && !(connectedSkill.Level >= connectedSkill.MaxLevel);
            connectedSkill.UpdateUI();
        }



    }

    // Método llamado cuando se hace clic en la habilidad para subir de nivel
    public void LevelUp()
    {
        skillTree.LevelUpSkill(Id);
    }
}
