using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PowerUpButton : MonoBehaviour
{
    private Button button;
    public string powerUpType;
    public int idAbility;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(UpgradePowerUp);
        
    }
    
    private void UpgradePowerUp()
    {
        PowerUpManagerFactory.instance.UpgradePowerUp(powerUpType, idAbility);
    }
}
