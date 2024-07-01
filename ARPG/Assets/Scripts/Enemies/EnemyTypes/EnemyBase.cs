using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(BoxCollider))] 
[RequireComponent(typeof(NavMeshAgent))] 
    public class EnemyBase : MonoBehaviour, IDamagable, IInteractObject
{

    protected Transform target;

    public EnemiesStats stats;

    int health, maxHealth;
    private float attackSpeed;
    protected int damage;
    private float attackrange;
    private int experience;


    private Animator anim;
    private SkinnedMeshRenderer _meshRenderer;
    private Material[] _meshRendererMat;

    [SerializeField] int scaleFactor=2;


    [SerializeField] Slider healthBarUI;
    [SerializeField] Slider easeHealthBarUI;
    [SerializeField] RewardItem expPrefab;
    [SerializeField] GameObject prefabDmgUI;

    Outline outline;

    private float attackTimer;

    private NavMeshAgent agent;

    InteractInput interactInput;

    Rigidbody[] ragdollBodies;

    private bool isAttacking;
    private bool isPoisoned, isStunned, isElite;
    private bool canPoison, canStun;

    public delegate void MaterialAction(Material material);

    protected void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        outline = GetComponentInChildren<Outline>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        _meshRendererMat = _meshRenderer.materials;
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
    }

    protected void Start()
    {

        
        interactInput = FindFirstObjectByType<InteractInput>();

        agent.acceleration = stats.acceleration;
        agent.angularSpeed = stats.angularVelocity;
        canStun=stats.canStun;
        canPoison=stats.canPoison;
    }

    protected void OnEnable()
    {
        isElite = Random.value<0.1f;
        
        float scaleMultiplier = stats.scaleCurve.Evaluate(PlayerExp.level);
        
        health = Mathf.RoundToInt(stats.health * scaleMultiplier);
        damage = Mathf.RoundToInt(stats.damage * scaleMultiplier);

       


        target = null;
        if (!isElite)
        {

            transform.transform.localScale = Vector3.one * scaleFactor;
            attackrange = stats.radiusDetection;
            ApplyActionToMaterials(DisableKeyword);

        }
        else
        {
            attackrange = stats.radiusDetection*2f;
            health = health * 2; 
            damage *= 2; 
            transform.localScale=Vector3.one* scaleFactor*2;

            ApplyActionToMaterials(EnableKeyword);
        }


        
        agent.speed = stats.speed;
        attackSpeed = stats.attackspeed;
        InicializeEnemy();

    }
    private void ApplyActionToMaterials(MaterialAction action)
    {
        foreach (var material in _meshRendererMat)
        {
            action(material);
        }
    }
    private void DisableKeyword(Material material)
    {
        material.DisableKeyword("_ISELITE");
    }
    private void EnableKeyword(Material material)
    {
        material.EnableKeyword("_ISELITE");
    }
    protected void Update()
    {
        ProcessCooldownHit();
        ProcessAttack();

    }


    private void InicializeEnemy()
    {
        isPoisoned = false;
        isStunned = false;
        maxHealth = health;
        healthBarUI.maxValue = health;
        healthBarUI.value = health; 
        easeHealthBarUI.maxValue = health;
        easeHealthBarUI.value = health;
        RagdollState(false);
    }
    private void ProcessCooldownHit()
    {
        if (attackTimer >= 0f)
        {

            attackTimer -= Time.deltaTime;
        }
    }

    void ProcessAttack()
    {
        if (target == null)
        {
            if (Vector3.Distance(transform.position, InstancePlayer.instance.transform.position) < 50)
            {
                target = InstancePlayer.instance.transform;
                anim.SetBool("Idle", true);
            }
        }

        if (isStunned || CharacterStats.isDead || target == null)
        {
            anim.SetBool("Idle", false);
            agent.SetDestination(transform.position);
            target = null;
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < attackrange)
        {
            if (attackTimer > 0f) { return; }
            if (Dead())
            {
                agent.isStopped = true;
                return; // Sal del método si el enemigo está muerto
            }
            attackTimer = attackSpeed; // Establecer la duración de la animación
            AnimAttack();
            agent.SetDestination(transform.position); // No moverse mientras ataca
            return;
        }

        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                isAttacking = false;
                anim.SetBool("Idle", true);
                agent.SetDestination(target.position);
            }
            return; 
        }

        if (Dead()) agent.isStopped = true;
        else
        {
            anim.SetBool("Idle", true);
            agent.SetDestination(target.position);
        }
    }

    protected virtual void DoDamage()
    {
        
        if (canStun)
        {
            bool stunned = Random.value <= 0.2f;
            if (stunned)
            {
                target.GetComponent<IDamagable>().GetStunned();
            }
            
        }
        if (canPoison)
        {
            bool poisoned = Random.value <= 0.2f;
            if (poisoned)
            {
                target.GetComponent<IDamagable>().GetStateDamage(damage/2);
            }
            else
            {
                target.GetComponent<IDamagable>().GetDamage(damage, false);
            }
            return;
        }
        target.GetComponent<IDamagable>().GetDamage(damage, false);

    }

    private void AnimAttack()
    {
        isAttacking = true; // Indicar que estamos atacando
        anim.SetTrigger("Attack"); // Iniciar la animación de ataque
        DoDamage(); // Realizar daño cuando la animación ha terminado
        anim.SetBool("Idle", false); // Puede que no necesites esto dependiendo de tu animación

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
        transform.DORotateQuaternion(targetRotation, 0.3f);
    }

    public void GetDamage(int damage, bool crit)
    {
        ShaderDmgAnim(isPoisoned ? Color.green : Color.white);

        ShowDamagePopUp(damage, crit);

        health -= DamageValue(damage, crit);
        interactInput?.UpdateUI(this);
        UpdateHealthBar();

        if (Dead())
        {
            //Disable();
            RagdollState(true);
            CreateExp();
            ObjectPoolManager.ReturnToPool(2, gameObject);
        }


    }

    private int DamageValue(int damage, bool crit)
    {
        if (crit) return (int)(damage * 1.5f);
        else return damage;
    }

    private void RagdollState(bool active)
    {
        GetComponent<BoxCollider>().enabled = !active;
        if (ragdollBodies.Length == 0 && active)
        {
            anim.SetTrigger("Dead");
            return;
        }
        
        anim.enabled = !active;
        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !active;

        }
    }
    private void UpdateHealthBar()
    {
        healthBarUI.value = health;
        easeHealthBarUI.DOValue(health, 0.6f);
    }

    public void CreateExp()
    {
        GameObject newExp=ObjectPoolManager.SpawnObject(expPrefab.gameObject, transform.position, Quaternion.identity);
        float scaleMultiplier = stats.scaleCurve.Evaluate(PlayerExp.level);
        newExp.GetComponent<RewardItem>().experience = Mathf.RoundToInt(stats.exp * scaleMultiplier / 3f);
    }
    public void ShowDamagePopUp(int damage, bool crit)
    {
        GameObject dmgTxt = ObjectPoolManager.SpawnObject(prefabDmgUI, transform.position, Quaternion.identity);
        dmgTxt.GetComponent<DmgPopUp>().InicializeEnemy(DamageValue(damage, crit), crit, isPoisoned ? Color.green : Color.white);

    }

    private bool Dead() => health <= 0;

    private void ShaderDmgAnim(Color color)
    {
        foreach (var item in _meshRendererMat)
        {
            HitMaterial(item, color);
        }
    }
    public void HitMaterial(Material material, Color color)
    {
        material.SetColor("_HitColor", color);
        material.DOComplete();
        material.SetFloat("_ValorColor", 1);
        material.DOFloat(0, "_ValorColor", 0.5f);
    }

    public string ObjectName() => stats.name;
    int[] IInteractObject.Health() => new[] { health, maxHealth };

    public Outline outLine() => outline;

    public void SlowDown(float targetSpeed)
    {
        agent.speed = stats.speed - targetSpeed;
    }

    public void GetHeal(int heal)
    {
        health += heal;
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
            isPoisoned = true;
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
            isStunned = true;
            StartCoroutine(StunCoroutine());
        }
    }
    IEnumerator PoisonCoroutine(int damage)
    {
        float timer = 0f;
        while (timer < 2f && !Dead())
        {
            // Aplicar daño de veneno por segundo
            GetDamage(damage, false);
            timer += 0.5f;
            yield return new WaitForSeconds(0.5f); // Espera al siguiente frame
        }
        isPoisoned = false;
    }
    IEnumerator StunCoroutine()
    {
        yield return new WaitForSeconds(3f); // Espera al siguiente frame
        isStunned = false;
    }

    bool IDamagable.isPoisoned()
    {
        return isPoisoned;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
