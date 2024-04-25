using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    CharacterStats playerStats;
    [SerializeField] TextMeshProUGUI currentHealth;
    [SerializeField] Image imageHpValue; 
    [SerializeField] TextMeshProUGUI currentMana;
    [SerializeField] Image imageManaValue;
    [SerializeField] Image deadPanel;
    [SerializeField] Image deadImage;

    public Button dashButton; 
    public Button shotGunButton; 
    public Button arrowRainButton;

    private Material hpLiquidMat;
    private Material manaLiquidMat;
    private void Awake()
    {
        playerStats = GetComponent<CharacterStats>();
        dashButton.interactable = false;
        shotGunButton.interactable = false;
        arrowRainButton.interactable = false;
        AbilityHandler.onDashAbilityUnlocked += ActivateDashButton;
        AbilityHandler.onShotgunAbilityUnlocked += ActivateShotgunButton;
        AbilityHandler.onArrowRainAbilityUnlocked += ActivateArrowRainButton;
        CharacterStats.onPlayerDead += ActivateDeadAnim;
        AbilityHandler.onManaUpdate += UpdateManaUI;
        hpLiquidMat = imageHpValue.material;
        manaLiquidMat = imageManaValue.material;
    }
    private void OnDisable()
    {
        AbilityHandler.onDashAbilityUnlocked -= ActivateDashButton;
        AbilityHandler.onShotgunAbilityUnlocked -= ActivateShotgunButton;
        AbilityHandler.onArrowRainAbilityUnlocked -= ActivateArrowRainButton;
        AbilityHandler.onManaUpdate -= UpdateManaUI;
        CharacterStats.onPlayerDead -= ActivateDeadAnim;
    }
    private void ActivateDashButton()
    {
        dashButton.interactable = true;
    }

    private void ActivateShotgunButton()
    {
        shotGunButton.interactable = true;
    }

    private void ActivateArrowRainButton()
    {
        arrowRainButton.interactable = true;
    }
    private void ActivateDeadAnim()
    {
        StartCoroutine(DeadUI());
    }

    private IEnumerator DeadUI()
    {
        deadImage.enabled = true;
        deadPanel.enabled= true;
        yield return Yielders.Get(4f);
        deadImage.enabled = false;
        deadPanel.enabled = false;


    }

    void Start()
    {
        
        UpdateUI();
    }
    public void UpdateUI()
    {
        hpLiquidMat.DOFloat(playerStats.Health / (float)playerStats.MaxHealth, "_Progress", 0.6f);
        currentHealth.SetText(playerStats.Health.ToString()+ "/" + playerStats.MaxHealth.ToString());
        manaLiquidMat.DOFloat(playerStats.Mana / (float)playerStats.MaxMana, "_Progress", 0.6f);
        currentMana.SetText(playerStats.Mana.ToString()+ "/" + playerStats.MaxMana.ToString());
    }

    public void UpdateManaUI()
    {
        manaLiquidMat.DOFloat(playerStats.Mana / (float)playerStats.MaxMana, "_Progress", 0.6f);
        currentMana.SetText(playerStats.Mana.ToString() + "/" + playerStats.MaxMana.ToString());
    }
    public void UpdateHealthUI()
    {
        hpLiquidMat.DOFloat(playerStats.Health / (float)playerStats.MaxHealth, "_Progress", 0.6f);
        currentHealth.SetText(playerStats.Health.ToString() + "/" + playerStats.MaxHealth.ToString());
    }
}
