using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyDataSO enemyData;
    private float currentHealth;

    public static event Action<int> OnEnemyKilled; // Notifica la recompensa de puntos/monedas

    void Start()
    {
        if (enemyData != null)
        {
            currentHealth = enemyData.life; // Carga la vida inicial desde el SO
        }
    }

    public void RecibirDanio(float danio)
    {
        Debug.Log("EnemyHealth recibió daño: " + danio);
        currentHealth -= danio;
        Debug.Log("Vida restante: " + currentHealth);
        if (currentHealth <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("El enemigo murió.");
        Debug.Log("EnemyData asignado: " + (enemyData != null));

        if (enemyData != null)
        {
            Debug.Log("Enviando evento de muerte.");
            OnEnemyKilled?.Invoke(enemyData.reward);
        }
        else
        {
            Debug.LogWarning("El enemigo no tiene EnemyDataSO asignado.");
        }

        Destroy(gameObject);
    }
}
