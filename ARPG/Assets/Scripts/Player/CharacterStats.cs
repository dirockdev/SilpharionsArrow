using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.AI;


public class CharacterStats : MonoBehaviour, IDamagable
{

    [SerializeField]
    private PlayerStats playerStats;
    private PlayerUI playerUI;
    NavMeshAgent agent;
    int health,maxHealth;
    [SerializeField]
    private ParticleSystem healPart;
    [SerializeField]
    private GameObject prefabHealUI;

    public int Health { get => health; set => health = value; }
    public PlayerStats PlayerStats { get => playerStats; set => playerStats = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    private void Awake()
    {
        playerUI = GetComponent<PlayerUI>();
        agent = GetComponent<NavMeshAgent>();
        Health = playerStats.health;
        maxHealth = playerStats.health;
        agent.speed= playerStats.speed;
    }
    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    
    public void GetDamage(int dmg,bool crit = false)
    {
        health -= dmg;
        playerUI.UpdateUI();
        if (Dead()) SceneManager.LoadScene(0);
        CameraEffects.Instance.CameraShake(5f);
    }

    private bool Dead() => health <= 0 ? true : false;
    public void GetFlatHeal(int heal)
    {
        Health += heal;
        ShowDamagePopUp(heal);
        healPart.Play();
        CheckHeals();   
    }
    public void GetHeal(int heal)
    {
        int healAmount= (int)(MaxHealth * ((float)heal / 100f));
        ShowDamagePopUp(healAmount);
        Health += healAmount;
        healPart.Play();
        CheckHeals();
    }

    private void CheckHeals()
    {
        if (health >= maxHealth) health = maxHealth;
        playerUI.UpdateUI();
    }
    public void ShowDamagePopUp(int number)
    {
        GameObject dmgTxt = ObjectPoolManager.SpawnObject(prefabHealUI, transform.position, Quaternion.identity);
        dmgTxt.GetComponent<DmgPopUp>().InicializeHeal(number);

    }
    public void GetStateDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public bool isPoisoned()
    {
        throw new System.NotImplementedException();
    }

    public void GetStunned()
    {
        throw new System.NotImplementedException();
    }
}
