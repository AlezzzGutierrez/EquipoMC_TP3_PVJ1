
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Estadísticas")]
    [SerializeField] private PlayerStatsSO playerStats;

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }

    public event Action OnPlayerDeath;

    private bool isDead;

    private void Awake()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerHealth: falta asignar PlayerStatsSO.");
            return;
        }

        MaxHealth = playerStats.maxHealth;
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isDead || amount <= 0)
            return;

        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        Debug.Log("Vida del jugador: " + CurrentHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        Debug.Log("El jugador ha muerto.");

        // Detener el movimiento.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // Desactivar los controles.
        PlayerController2D controller =
            GetComponent<PlayerController2D>();

        if (controller != null)
            controller.enabled = false;

        PlayerCombat combat = GetComponent<PlayerCombat>();

        if (combat != null)
            combat.enabled = false;

        // Avisar al sistema externo.
        OnPlayerDeath?.Invoke();
    }
}
