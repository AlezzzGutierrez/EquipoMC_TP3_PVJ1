
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerProjectile : MonoBehaviour
{
    private Rigidbody2D rb;

    private float damage;
    private float speed;
    private bool initialized;

    [SerializeField] private float lifeTime = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(float projectileDamage, float projectileSpeed)
    {
        damage = projectileDamage;
        speed = projectileSpeed;
        initialized = true;

        rb.linearVelocity = (Vector2)transform.up * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!initialized)
            return;

        // No dañar al propio jugador.
        if (other.CompareTag("Player"))
            return;

        // Aplicar daño si el objeto es un enemigo.
        if (other.CompareTag("Enemy"))
        {
            other.SendMessage(
                "TakeDamage",
                damage,
                SendMessageOptions.DontRequireReceiver
            );
        }

        // Destruir el proyectil al impactar.
        Destroy(gameObject);
    }
}
