using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;
using System.Collections;


public class CharacterStats : MonoBehaviour, IDamagable
{

    [SerializeField]
    private PlayerStats playerStats;
    private PlayerUI playerUI;
    NavMeshAgent agent;

    int health,maxHealth,mana, maxMana;
    
    public static Vector3 spawnPoint;

    [SerializeField]
    private ParticleSystem healPart;
    [SerializeField]
    private GameObject prefabHealUI;

    public static bool isDead;
    public static int DamageAtribute=1;

    private bool isPoisoned,isStunned;


    public static event Action onPlayerDead;
    public static event Action<bool> onPlayerStunned;
    public static event Action<bool> onPlayerPoisoned;



    public int Health { get => health; set => health = value; }
    public PlayerStats PlayerStats { get => playerStats; set => playerStats = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Mana { get => mana; set => mana = value; }
    public int MaxMana { get => maxMana; set => maxMana = value; }

    private void Awake()
    {
        playerUI = GetComponent<PlayerUI>();
        agent = GetComponent<NavMeshAgent>();

        PlayerExp.OnLevelUp += PlayerScale;
        DashAbility.onHealDashing += GetHeal;
        PotionAbility.onPotionUse += GetHeal;
        ProjectController1.onHitMana += GetMana;
        Health = playerStats.health;
        maxHealth = playerStats.health;
        mana = playerStats.mana;
        maxMana = playerStats.mana;
        agent.speed= playerStats.speed;
    }
    private void OnDisable()
    {
        PlayerExp.OnLevelUp -= PlayerScale;
        DashAbility.onHealDashing -= GetHeal;
        PotionAbility.onPotionUse -= GetHeal;
        ProjectController1.onHitMana -= GetMana;
    }
    private void Start()
    {
        
        PlayerScale(PlayerExp.level);
    }
    public void PlayerScale(int level)
    {
        float scaleMultiplier = playerStats.scaleCurve.Evaluate(level);
        DamageAtribute = Mathf.RoundToInt(2*scaleMultiplier);
        maxHealth = Mathf.RoundToInt(playerStats.health * scaleMultiplier);
        health = maxHealth;
        maxMana = Mathf.RoundToInt(playerStats.mana * scaleMultiplier);
        mana = maxMana;

        spawnPoint = Vector3.zero;
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
            playerUI.UpdateHealthUI();
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
        onPlayerDead?.Invoke();
        agent.SetDestination(transform.position);
        AudioManager.instance.PlaySFXWorld("6", default, 4);
        agent.speed = 0;
        yield return Yielders.Get(4f);
        agent.speed = playerStats.speed;
        health = maxHealth;
        playerUI.UpdateUI();
        transform.position= spawnPoint;
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
    public void GetMana(int mana)
    {
        
        ShowDamagePopUp(mana, Color.blue);
        this.mana += mana;
        
        CheckMana();
    }

    private void CheckHeals()
    {
        if (health >= maxHealth) health = maxHealth;
        playerUI.UpdateHealthUI();
    }   
    private void CheckMana()
    {
        if (mana >=maxMana) mana = maxMana;
        playerUI.UpdateManaUI();
    }
    public void ShowDamagePopUp(int number, Color color)
    {
        GameObject dmgTxt = ObjectPoolManager.SpawnObject(prefabHealUI, transform.position, Quaternion.identity);
        dmgTxt.GetComponent<DmgPopUp>().InicializePlayer(number, color);

    }
    public void GetStateDamage(int damage)
    {
        if (!isPoisoned)
        {
            isPoisoned = true;
            StartCoroutine(PoisonCoroutine(damage));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(PoisonCoroutine(damage));
        }
    }
    public void GetStunned()
    {
        if (!isStunned)
        {
            isStunned = true;
            StartCoroutine(StunCoroutine());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(StunCoroutine());
        }
    }
    IEnumerator PoisonCoroutine(int damage)
    {
        onPlayerPoisoned?.Invoke(isPoisoned);
        float timer = 0f;
        while (timer < 2f && !Dead())
        {
            // Aplicar da�o de veneno por segundo
            GetDamage(damage, false);
            timer += 0.5f;
            yield return new WaitForSeconds(0.5f); // Espera al siguiente frame
        }
        isPoisoned = false;
        onPlayerPoisoned?.Invoke(isPoisoned);
    }
    IEnumerator StunCoroutine()
    {
        onPlayerStunned?.Invoke(isStunned);
        agent.speed = 0;
        yield return new WaitForSeconds(1f); // Espera al siguiente frame
        if(!Dead())agent.speed = PlayerStats.speed;
        isStunned = false;
        onPlayerStunned?.Invoke(isStunned);
    }

    bool IDamagable.isPoisoned()
    {
        return isPoisoned;
    }

}
