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
  
    private void OnEnable()
    {
        body = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        part = GetComponentInChildren<ParticleSystem>();
        part.Play();
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
        StartCoroutine(ReturnToPool());
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
            if(itemHit is ZombieEnemy && slow!=0)
            {
                ZombieEnemy enemy = (ZombieEnemy)itemHit;
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
        while (elapsedTime < 3f)
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
    void IProjectile.SetSlow(float getSlow)
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
}