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
    private float timeAlive;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        part = GetComponentInChildren<ParticleSystem>();
    }
    private void OnEnable()
    {
        part.Play();
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
        AudioManager.instance.PlaySFXWorld("0", transform.position);

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
    IEnumerator ReturnToPool()
    {
        float elapsedTime = 0f;
        while (elapsedTime < timeAlive)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
    private void EndProjectile()
    {
        meshRenderer.enabled = false;
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
        StartCoroutine(ReturnToPool());
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
