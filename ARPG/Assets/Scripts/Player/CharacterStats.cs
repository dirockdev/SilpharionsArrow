using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;


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
    private bool isDead;
    public static int DamageAtribute=1;
    public int Health { get => health; set => health = value; }
    public PlayerStats PlayerStats { get => playerStats; set => playerStats = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    private void Awake()
    {
        playerUI = GetComponent<PlayerUI>();
        agent = GetComponent<NavMeshAgent>();

        PlayerExp.OnLevelUp += PlayerScale;

        Health = playerStats.health;
        maxHealth = playerStats.health;
        agent.speed= playerStats.speed;
    }

    public void PlayerScale(int level)
    {
        float scaleMultiplier = playerStats.scaleCurve.Evaluate(level);
        DamageAtribute = Mathf.RoundToInt(2*scaleMultiplier);
        maxHealth = Mathf.RoundToInt(playerStats.health * scaleMultiplier);
        health = maxHealth;
        playerUI.UpdateUI();
    }





    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    
    public void GetDamage(int dmg,bool crit = false)
    {
        if (!isDead)
        {
            health -= dmg;
            ShowDamagePopUp(dmg, Color.red);
            AudioManager.instance.PlaySFXWorld("7", transform.position);
            playerUI.UpdateUI();
            if (Dead())
            {
                StartCoroutine(DeadAnim());
            }
            CameraEffects.Instance.CameraShake(5f);

        }
    }

    private IEnumerator DeadAnim()
    {
        isDead = true;
        agent.SetDestination(transform.position);
        AudioManager.instance.PlaySFXWorld("6", default, 4);
        yield return Yielders.Get(4f);
        health = maxHealth;
        playerUI.UpdateUI();
        transform.position=Vector3.zero;
        agent.SetDestination(transform.position);
        isDead = false;
    }

    private bool Dead() => health <= 0 ? true : false;
    public void GetFlatHeal(int heal)
    {
        Health += heal;
        ShowDamagePopUp(heal, Color.green);
        healPart.Play();
        CheckHeals();   
    }
    public void GetHeal(int heal)
    {
        int healAmount= (int)(MaxHealth * ((float)heal / 100f));
        ShowDamagePopUp(healAmount, Color.green);
        Health += healAmount;
        healPart.Play();
        CheckHeals();
    }

    private void CheckHeals()
    {
        if (health >= maxHealth) health = maxHealth;
        playerUI.UpdateUI();
    }
    public void ShowDamagePopUp(int number, Color color)
    {
        GameObject dmgTxt = ObjectPoolManager.SpawnObject(prefabHealUI, transform.position, Quaternion.identity);
        dmgTxt.GetComponent<DmgPopUp>().InicializePlayer(number, color);

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
