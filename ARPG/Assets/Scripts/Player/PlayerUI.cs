using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    CharacterStats playerStats;
    [SerializeField] TextMeshProUGUI currentHealth;
    [SerializeField] Image imageHpValue;
    [SerializeField] Image deadImage;

    public Button dashButton; 
    public Button shotGunButton; 
    public Button arrowRainButton; 
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
        yield return Yielders.Get(4f);
        deadImage.enabled = false;


    }

    void Start()
    {
        imageHpValue.fillAmount = playerStats.Health/ (float)playerStats.MaxHealth;
        UpdateUI();
    }
    public void UpdateUI()
    {
        imageHpValue.fillAmount = playerStats.Health/ (float)playerStats.MaxHealth;
        currentHealth.SetText(playerStats.Health.ToString()+ "/" + playerStats.MaxHealth.ToString());
    }
}
