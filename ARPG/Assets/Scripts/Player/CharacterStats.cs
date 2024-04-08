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
        healPart.Play();
        CheckHeals();   
    }
    public void GetHeal(int heal)
    {

        Health += (int)(MaxHealth * ((float)heal / 100f));
        healPart.Play();
        CheckHeals();
    }

    private void CheckHeals()
    {
        if (health >= maxHealth) health = maxHealth;
        playerUI.UpdateUI();
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
