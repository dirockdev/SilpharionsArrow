using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;


public class ProjectController3 : MonoBehaviour, IProjectile
{

    bool canPenetrate;
    int velocity,damage,probCrit,poisonDmg;
    float state;
    Rigidbody body;
    BoxCollider boxCollider;
    ParticleSystem part;
    private float widthProj;
    ParticleSystem.ShapeModule shapeModule;
    private float timeAlive;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        part = GetComponentInChildren<ParticleSystem>();
        shapeModule= part.GetComponentInChildren<ParticleSystem>().shape;
    }
    private void OnEnable()
    {
        part.Play();
        boxCollider.enabled = true;
        
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

            bool isPoisonState = Random.value < state;
            if (isPoisonState)itemHit.GetStateDamage(poisonDmg);

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
        this.poisonDmg = stateDmg;
    }

    public void SetWidthProj(float width)
    {
        widthProj = width;
        boxCollider.size = new Vector3(widthProj == 1 ? 1 : widthProj * 10, 1, 1);
        shapeModule.radius = 0.4f * widthProj;
    }

    public void SetTimeAlive(float time)
    {
        timeAlive = time;
        StartCoroutine(ReturnToPool());
    }
}
