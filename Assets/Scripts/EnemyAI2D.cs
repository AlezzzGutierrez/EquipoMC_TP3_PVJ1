using UnityEngine;

public class EnemyAI2D : MonoBehaviour
{
    [Header("Configuración de Datos")]
    public EnemyDataSO enemyData; // Se asigna el ScriptableObject correspondiente

    private Transform player;
    private float currentCooldown;

    void Start()
    {
        // Busca al jugador por tag o etiqueta en la escena
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Comportamiento según el rango definido en el ScriptableObject
        if (distanceToPlayer <= enemyData.range)
        {
            // Lógica de ataque o disparo si está en rango
            AtacarJugador();
        }
        else if (distanceToPlayer <= 10f /* Rango de detección opcional */)
        {
            // Persecución en vista cenital (2D top-down)
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyData.speed * Time.deltaTime);
        }

        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    void AtacarJugador()
    {
        if (currentCooldown <= 0)
        {
            // Dependiendo si es melee o shooter, disparamos proyectil o hacemos daño directo
            // (Si es shooter, aquí se instancia EnemyProjectile)
            currentCooldown = enemyData.cooldown;
        }
    }
}