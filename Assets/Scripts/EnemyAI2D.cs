using UnityEngine;

public class EnemyAI2D : MonoBehaviour
{
    [Header("Configuración de Datos")]
    [SerializeField] private EnemyDataSO enemyData;

    private Transform player;
    private float currentHealth;
    private float lastAttackTime;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (enemyData != null)
        {
            currentHealth = enemyData.MaxHealth;
        }
        else
        {
            Debug.LogWarning($"El enemigo {gameObject.name} no tiene asignado un EnemyDataSO.");
        }

        // Busca automáticamente al jugador por etiqueta ("Player")
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void Update()
    {
        if (player == null || enemyData == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > enemyData.AttackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            TryAttack();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * enemyData.MoveSpeed, rb.linearVelocity.y);
    }

    private void TryAttack()
    {
        // Detiene el movimiento al atacar
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        if (Time.time >= lastAttackTime + enemyData.AttackCooldown)
        {
            lastAttackTime = Time.time;
            ExecuteAttack();
        }
    }

private void ExecuteAttack()
    {
        if (enemyData.IsRanged && enemyData.ProjectilePrefab != null)
        {
            // Lógica de ataque a distancia
            GameObject proj = Instantiate(enemyData.ProjectilePrefab, transform.position, Quaternion.identity);
            Vector2 dir = (player.position - transform.position).normalized;
            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            if (projRb != null)
            {
                projRb.linearVelocity = dir * enemyData.ProjectileSpeed;
            }
        }
        else
        {
            // Lógica de ataque cuerpo a cuerpo
            Debug.Log($"{enemyData.EnemyName} atacó al jugador haciendo {enemyData.AttackDamage} de daño.");

            // 👉 AGREGAR ESTO: Buscar el componente de salud del jugador y restarle vida
            if (player != null)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(enemyData.AttackDamage);
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{enemyData.EnemyName} ha muerto.");
        Destroy(gameObject);
    }
}