using UnityEngine;
using System.Collections;

public class SpawnerEnemigos : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField] private Transform[] puntosSpawn;
    [SerializeField] private EventoSala eventoSala;

    [Header("Tiempo del Spawner")]
    [SerializeField] private float duracionSpawner = 10f;
    [SerializeField] private float intervaloSpawn = 2f;

    private int enemigosVivos = 0;
    private bool activo = false;
    private bool terminoDeGenerar = false;

    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += AlMorirEnemigo;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= AlMorirEnemigo;
    }

    public void ActivarSpawner()
    {
        if (activo) return;

        activo = true;
        StartCoroutine(GenerarEnemigos());
    }

    private IEnumerator GenerarEnemigos()
    {
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionSpawner)
        {
            Transform punto = puntosSpawn[
                Random.Range(0, puntosSpawn.Length)
            ];
            Debug.Log("Spawner: " + gameObject.name);
            Debug.Log("Punto de spawn: " + punto.name);
            Debug.Log("Posición: " + punto.position);
            Instantiate(
                enemigoPrefab,
                punto.position,
                punto.rotation
            );

            enemigosVivos++;

            Debug.Log("¡Enemigo generado! Vivos: " + enemigosVivos);

            yield return new WaitForSeconds(intervaloSpawn);

            tiempoTranscurrido += intervaloSpawn;
        }

        Debug.Log("¡El Spawner dejó de generar enemigos!");

        terminoDeGenerar = true;

        if (enemigosVivos == 0)
        {
            FinalizarSala();
        }
    }

    private void AlMorirEnemigo(int recompensa)
    {
        if (!activo || enemigosVivos <= 0) return;

        enemigosVivos--;

        Debug.Log("Enemigos restantes: " + enemigosVivos);

        if (terminoDeGenerar && enemigosVivos == 0)
        {
            FinalizarSala();
        }
    }

    private void FinalizarSala()
    {
        activo = false;
        eventoSala.AbrirPuertas();
    }
}