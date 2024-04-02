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
    private void Awake()
    {
        playerStats = GetComponent<CharacterStats>();
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
