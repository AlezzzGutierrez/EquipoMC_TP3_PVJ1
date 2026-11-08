using UnityEngine;

public class EventoSala : MonoBehaviour
{
    [Header("Configuración de la sala")]
    [SerializeField] private GameObject cierresSala;
    [SerializeField] private SpawnerEnemigos spawner;

    private bool salaActivada = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (salaActivada) return;

        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            salaActivada = true;

            cierresSala.SetActive(true);
            spawner.ActivarSpawner();

            Debug.Log("¡Sala activada por: " + other.tag + "!");
        }
    }

    public void AbrirPuertas()
    {
        cierresSala.SetActive(false);
        Debug.Log("¡Sala completada! Puertas abiertas.");
    }
}