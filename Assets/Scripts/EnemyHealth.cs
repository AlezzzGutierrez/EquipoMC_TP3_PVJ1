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
        currentHealth -= danio;
        if (currentHealth <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        // Emitimos el evento de muerte enviando la recompensa configurada
        if (enemyData != null)
        {
            OnEnemyKilled?.Invoke(enemyData.reward);
        }

        // Destruimos el objeto del enemigo
        Destroy(gameObject);
    }
}
