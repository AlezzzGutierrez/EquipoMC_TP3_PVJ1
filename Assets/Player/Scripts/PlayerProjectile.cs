
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerProjectile : MonoBehaviour
{
    private Rigidbody2D rb;

    private float damage;
    private float speed;

    [SerializeField] private float lifetime = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(
        float projectileDamage,
        float projectileSpeed,
        Transform owner
    )
    {
        damage = projectileDamage;
        speed = projectileSpeed;

        // Ignorar todos los colliders del jugador que disparó.
        if (owner != null)
        {
            Collider2D[] ownerColliders =
                owner.GetComponentsInChildren<Collider2D>();

            Collider2D[] projectileColliders =
                GetComponentsInChildren<Collider2D>();

            foreach (Collider2D ownerCollider in ownerColliders)
            {
                foreach (Collider2D projectileCollider in projectileColliders)
                {
                    Physics2D.IgnoreCollision(
                        projectileCollider,
                        ownerCollider
                    );
                }
            }
        }


        rb.linearVelocity = transform.right * speed;

        Destroy(gameObject, lifetime);
    }

    //modificado
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;

        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.RecibirDanio(damage);
            Destroy(gameObject);
        }
    }
}
