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
    [SerializeField] GameObject deadResources;


    [SerializeField] Image PoisonStateImage;
    [SerializeField] Image StunStateImage;

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
        CharacterStats.onPlayerStunned += UpdateStunState;
        CharacterStats.onPlayerPoisoned += UpdatePoisonState;
        AbilityHandler.onManaUpdate += UpdateManaUI;
        


        hpLiquidMat = imageHpValue.material;
        manaLiquidMat = imageManaValue.material;
    }
    private void OnDisable()
    {
        AbilityHandler.onDashAbilityUnlocked -= ActivateDashButton;
        AbilityHandler.onShotgunAbilityUnlocked -= ActivateShotgunButton;
        AbilityHandler.onArrowRainAbilityUnlocked -= ActivateArrowRainButton;
        CharacterStats.onPlayerStunned -= UpdateStunState;
        CharacterStats.onPlayerPoisoned -= UpdatePoisonState;
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
        deadResources.SetActive(true);
        deadPanel.enabled= true;
        yield return Yielders.Get(4f);
        deadResources.SetActive(false);
        deadPanel.enabled = false;


    }

    void Start()
    {
        
        UpdateUI();
    }
    private void UpdateStunState(bool active)
    {
        StunStateImage.gameObject.SetActive(active);
    }
    private void UpdatePoisonState(bool active)
    {
        PoisonStateImage.gameObject.SetActive(active);

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
        int manaShown = (int)playerStats.Mana;
        currentMana.SetText(manaShown.ToString() + "/" + playerStats.MaxMana.ToString());
    }
    public void UpdateHealthUI()
    {
        hpLiquidMat.DOFloat(playerStats.Health / (float)playerStats.MaxHealth, "_Progress", 0.6f);
        currentHealth.SetText(playerStats.Health.ToString() + "/" + playerStats.MaxHealth.ToString());
    }
}
