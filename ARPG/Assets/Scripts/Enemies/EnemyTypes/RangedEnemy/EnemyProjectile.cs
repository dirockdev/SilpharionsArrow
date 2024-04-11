
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private int damage;
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;
    private Rigidbody body;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        ObjectPoolManager.ReturnToPool(3, gameObject);
    }
    private void OnEnable()
    {
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamagable itemHit = other.GetComponent<IDamagable>();

        if (itemHit != null)
        {
            itemHit.GetDamage(damage);
            EndProjectile();
        }
    }
    public void SetDirection(Vector3 direction,int velocity)
    {
        // Establece la rotación del proyectil hacia la dirección especificada.
        transform.rotation = Quaternion.LookRotation(direction);
        // Establece la velocidad en la dirección especificada.
        body.velocity = direction * velocity;
    }
    private void EndProjectile()
    {
        boxCollider.enabled = false;
        meshRenderer.enabled = false;
        body.velocity = Vector3.zero;
    }
}
