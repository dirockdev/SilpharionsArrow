
using System.Collections;
using UnityEngine;


public class ProjectController3 : MonoBehaviour, IProjectile
{

    bool canPenetrate, canHeal, canReduceCooldown;
    int velocity, damage, probCrit;
    float state;
    Rigidbody body;
    BoxCollider boxCollider;
    ParticleSystem part;
    private float widthProj;
    ParticleSystem.MainModule mainModule;
    private float timeAlive;
    private int heal;
    
    AbilityHandler abilityHandler;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        part = GetComponentInChildren<ParticleSystem>();
        mainModule= part.main;
        abilityHandler=InstancePlayer.instance.GetComponent<AbilityHandler>();
    }
    private void OnEnable()
    {
        part.Play();
        boxCollider.enabled = true;
        AudioManager.instance.PlaySFXWorld("2", transform.position);
    }
    
   

    public void SetDirection(Vector3 direction)
    {
        // Establece la rotación del proyectil hacia la dirección especificada.
        transform.rotation = Quaternion.LookRotation(direction);
        // Establece la velocidad en la dirección especificada.
        body.velocity=direction* velocity;
    }


    private void OnTriggerEnter(Collider other)
    {
        IDamagable itemHit = other.GetComponent<IDamagable>();

        if (itemHit != null)
        {
            bool crit=Random.value<((float)probCrit/100);
            itemHit.GetDamage(damage,crit);
            if (crit && canHeal) InstancePlayer.instance.GetComponent<CharacterStats>().GetFlatHeal(heal);
            if (itemHit.isPoisoned()&&canReduceCooldown) abilityHandler.Abilities[3].currentCooldown-=2f;

            if (!canPenetrate)
            {
                EndProjectile();
            }

        }
        else
        {
            if (!canPenetrate)
            {
                EndProjectile();
            }

        }
    }
   private void EndProjectile()
    {
        boxCollider.enabled = false;
        part.Stop();
        
        body.velocity = Vector3.zero;
    }

    void IProjectile.SetDamage(int getDamage)
    {
        damage = getDamage;
    }

    void IProjectile.SetVelocity(int getVelocity)
    {
        velocity = getVelocity;
    }
    void IProjectile.SetState(float getState)
    {
        state = getState;
    }
    public void SetCanPenetrate(bool getCanPenetrate)
    {
        canPenetrate = getCanPenetrate;
    }

    public void SetCrit(int getCrit)
    {
        probCrit = getCrit;
    }

    public void SetStateDmg(int stateDmg)
    {
        heal = stateDmg;
    }

    public void SetWidthProj(float width)
    {
        widthProj = width;
        boxCollider.size = new Vector3(widthProj == 1 ? 1 : widthProj * 2, 1, 1);
        
        mainModule.startSpeedMultiplier = 15+(state*5);
    }

    public void SetTimeAlive(float time)
    {
        timeAlive = time;
        ObjectPoolManager.ReturnToPool(timeAlive,gameObject);
    }

    public void SetHealOnCrits(bool canHeal)
    {
        this.canHeal = canHeal;
    }

    public void SetReduceCooldown(bool canReduceCooldown)
    {
        this.canReduceCooldown = canReduceCooldown;
    }

    public void SetStun(float stunProb)
    {
    }
}
