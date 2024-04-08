using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour, IArea
{
   
    int damage, area;

    Rigidbody body;
    SphereCollider sphereCollider;
    ParticleSystem part;

    ParticleSystem.MainModule mainModule;
    private float timeAlive;

    private float timer;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        part = GetComponentInChildren<ParticleSystem>();
        
    }
    private void OnEnable()
    {
        part.Play();
        sphereCollider.enabled = true;

    }

    private void OnTriggerStay(Collider other)
    {
        IDamagable itemHit = other.GetComponent<IDamagable>();

        if (itemHit != null)
        {

            // Timer to control damage ticks
            timer += Time.deltaTime;
            if (timer >= 0.4f)
            {
                // Deal damage to the enemy
                itemHit.GetDamage(damage);
                timer = 0f; // Reset timer
            }

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
 

    public void SetArea(int area)
    {
        this.area = area;
    }

    public void SetDamage(int getDamage)
    {
        damage=getDamage;
    }

    public void SetTimeAlive(float time)
    {
        timeAlive = time;
        StartCoroutine(ReturnToPool());
    }

}
