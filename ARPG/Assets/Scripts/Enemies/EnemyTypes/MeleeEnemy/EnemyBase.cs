using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;

public class EnemyBase : MonoBehaviour, IDamagable, IInteractObject
{

    public EnemiesStats stats;

    protected Transform target;
    int health;
    private float attackrange;

    private Animator anim;
    private SkinnedMeshRenderer _meshRenderer;
    private Material _meshRendererMat;
    [SerializeField] Slider healthBarUI;
    [SerializeField] RewardItem expPrefab;
    [SerializeField] GameObject prefabDmgUI;

    Outline outline;
    private float attackSpeed;
    protected int damage;

    private float attackTimer;

    private NavMeshAgent agent;

    InteractInput interactInput;

    Rigidbody[] ragdollBodies;

    private bool isPoisoned, isStunned;
    protected void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        outline = GetComponentInChildren<Outline>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
    }

    protected void Start()
    {

        _meshRendererMat = _meshRenderer.material;
        target = InstancePlayer.instance.transform;
        interactInput = FindFirstObjectByType<InteractInput>();
        

    }

    protected void OnEnable()
    {

        float scaleMultiplier = stats.scaleCurve.Evaluate(PlayerExp.level);
        
        health = Mathf.RoundToInt(stats.health * scaleMultiplier);
        
        damage = Mathf.RoundToInt(stats.damage * scaleMultiplier);
        
        attackrange = stats.radiusDetection;
        agent.speed = stats.speed;
        attackSpeed = stats.attackspeed;
        InicializeEnemy();

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
        healthBarUI.maxValue = health;
        healthBarUI.value = health;
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
        if (isStunned)
        {
            anim.SetBool("Idle", true);
            agent.SetDestination(transform.position);
        }
        else
        {

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < attackrange)
            {
                if (attackTimer > 0f) { return; }
                if (Dead())
                {
                    agent.isStopped = true;
                    return; // Sal del método si el enemigo está muerto
                }
                attackTimer = attackSpeed;
                DoDamage();
                AnimAttack();

                agent.SetDestination(transform.position);

            }
            else
            {
                if (Dead())
                {
                    agent.isStopped = true;
                    return; // Sal del método si el enemigo está muerto
                }
                anim.SetBool("Idle", true);
                agent.SetDestination(target.position);

            }
        }

    }

    protected virtual void DoDamage()
    {
        target.GetComponent<IDamagable>().GetDamage(damage, false);
    }

    private void AnimAttack()
    {
        anim.SetTrigger("Attack");
        anim.SetBool("Idle", false);
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
        transform.DORotateQuaternion(targetRotation, 0.3f);
    }

    public void GetDamage(int damage, bool crit)
    {
        ShaderDmgAnim(isPoisoned ? Color.green : Color.grey);

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
        anim.enabled = !active;
        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !active;

        }
    }
    private void UpdateHealthBar()
    {
        healthBarUI.value = health;
    }

    public void CreateExp()
    {
        ObjectPoolManager.SpawnObject(expPrefab.gameObject, transform.position, Quaternion.identity);
    }
    public void ShowDamagePopUp(int damage, bool crit)
    {
        GameObject dmgTxt = ObjectPoolManager.SpawnObject(prefabDmgUI, transform.position, Quaternion.identity);
        dmgTxt.GetComponent<DmgPopUp>().InicializeEnemy(DamageValue(damage, crit), crit, isPoisoned ? Color.green : Color.white);

    }

    private bool Dead() => health <= 0;

    private void ShaderDmgAnim(Color color)
    {
        _meshRendererMat.SetColor("_TextureColor", color);
        _meshRendererMat.DOComplete();
        _meshRendererMat.SetFloat("_ValorColor", 1);
        _meshRendererMat.DOFloat(0, "_ValorColor", 0.5f);
    }

    public string ObjectName() => stats.name;
    int[] IInteractObject.Health() => new[] { health, stats.health };

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
}
