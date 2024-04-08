using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour, IArea
{
   
    int damage, area;

    Rigidbody body;
    SphereCollider sphereCollider;
    ParticleSystem[] parts;

    List<ParticleSystem.ShapeModule> shapeModule= new List<ParticleSystem.ShapeModule>();
    private float timeAlive;

    private float timer;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        parts = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < parts.Length; i++)
        {
            shapeModule.Add(parts[i].shape);
        }
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
        sphereCollider.radius = area;
        for (int i = 0; i < shapeModule.Count; i++)
        {
            // Crear un nuevo ShapeModule con el radio actualizado
            ParticleSystem.ShapeModule newShapeModule = shapeModule[i];
            newShapeModule.radius = area;
            shapeModule[i] = newShapeModule;
        }


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
