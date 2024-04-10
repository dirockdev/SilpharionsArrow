using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour, IArea
{

    int damage, area;
    bool isHoming;
    SphereCollider sphereCollider;
    ParticleSystem[] parts;

    List<ParticleSystem.ShapeModule> shapeModule = new List<ParticleSystem.ShapeModule>();
    private float timeAlive;

    private float timer;

    public LayerMask targetLayer; // Capa de los objetos que pueden ser objetivos
    private float detectionRadius = 30f; // Radio de detecci�n para buscar objetivos
    private int homingSpeed; // Velocidad a la que el �rea de efecto se dirigir� hacia el objetivo
   
    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        parts = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < parts.Length; i++)
        {
            shapeModule.Add(parts[i].shape);
        }
    }

    private Transform target; // El objetivo al que se dirigir� la lluvia de flechas

    private void Update()
    {
        if (isHoming)
        {
            SearchForTarget();

            if (target != null)
            {
                MoveTowardsTarget();
            }
        }
    }

    private void SearchForTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

        if (colliders.Length > 0)
        {
            // Encontrar el objetivo m�s cercano
            Transform nearestTarget = null;
            float nearestDistance = Mathf.Infinity;

            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTarget = collider.transform;
                }
            }

            if (nearestTarget != null)
            {
                SetTarget(nearestTarget);
            }
        }
    }

    private void MoveTowardsTarget()
    {
        // Calcula la direcci�n en el plano horizontal (XZ) hacia el objetivo
        Vector3 direction = new Vector3(target.position.x - transform.position.x, transform.position.y, target.position.z - transform.position.z).normalized;

        // Mueve el �rea en esa direcci�n, manteniendo su posici�n en Y
        transform.Translate(direction * Time.deltaTime * homingSpeed, Space.World);
    }

    // M�todo para establecer el objetivo hacia el que se dirigir� la lluvia de flechas
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
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
        damage = getDamage;
    }

    public void SetTimeAlive(float time)
    {
        timeAlive = time;
        StartCoroutine(ReturnToPool());
    }

    public void SetIsHoming(bool canHoming)
    {
        isHoming = canHoming;


    }

    public void SetHomingSpeed(int homingSpeed)
    {
        this.homingSpeed = homingSpeed;
    }
}
