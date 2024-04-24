using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;


public class ProjectController1 : MonoBehaviour, IProjectile
{

    bool canPenetrate;
    int velocity,damage,probCrit;
    float slow;
    Rigidbody body;
    MeshRenderer meshRenderer;
    BoxCollider boxCollider;
    ParticleSystem part;
    TrailRenderer trail;
    Light lightProj;
    private float timeAlive;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        part = GetComponentInChildren<ParticleSystem>();
        trail = GetComponentInChildren<TrailRenderer>();
        lightProj = GetComponentInChildren<Light>();
    }
    private void OnEnable()
    {
        lightProj.enabled = true;
        part.Play();
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
        AudioManager.instance.PlaySFXWorld("0", transform.position);

    }
    private void OnDisable()
    {
        trail.Clear();
    }
    public void SetDirection(Vector3 direction)
    {
        // Establece la rotaci�n del proyectil hacia la direcci�n especificada.
        transform.rotation = Quaternion.LookRotation(direction);
        // Establece la velocidad en la direcci�n especificada.
        body.velocity=direction* velocity;
    }


    private void OnTriggerEnter(Collider other)
    {
        IDamagable itemHit = other.GetComponent<IDamagable>();

        if (itemHit != null)
        {
            bool crit=Random.value<((float)probCrit/100);
            itemHit.GetDamage(damage,crit);
            if(itemHit is EnemyBase && slow!=0)
            {
                EnemyBase enemy = (EnemyBase)itemHit;
                enemy.SlowDown(slow);
            }
            if (!canPenetrate)
            {
                EndProjectile();
            }
          
        }
        else
        {
            EndProjectile();

        }
    }
    
    private void EndProjectile()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        lightProj.enabled = false;
        part.Stop();
        trail.Clear();
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
    void IProjectile.SetState(float getSlow)
    {
        slow = getSlow;
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
        
    }

    public void SetWidthProj(float width)
    {
     
    }

    public void SetTimeAlive(float time)
    {
        timeAlive = time;
        ObjectPoolManager.ReturnToPool(timeAlive, gameObject);
    }

    public void SetHealOnCrits(bool canHeal)
    {
        
    }

    public void SetReduceCooldown(bool canReduceCooldown)
    {
       
    }

    public void SetStun(float stunProb)
    {
    }
}
