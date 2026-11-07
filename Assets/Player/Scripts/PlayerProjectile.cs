
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

    public void Initialize(float projectileDamage, float projectileSpeed)
    {
        damage = projectileDamage;
        speed = projectileSpeed;

        rb.linearVelocity = transform.right * speed;

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;

        if (other.CompareTag("Enemy"))
        {
            other.SendMessage(
                "TakeDamage",
                damage,
                SendMessageOptions.DontRequireReceiver
            );
        }

        Destroy(gameObject);
    }
}
