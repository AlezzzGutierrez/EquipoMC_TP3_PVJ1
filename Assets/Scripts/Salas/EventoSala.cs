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

        if (other.CompareTag("Player"))
        {
            salaActivada = true;
            cierresSala.SetActive(true);
            spawner.ActivarSpawner();
        }
    }

    public void AbrirPuertas()
    {
        cierresSala.SetActive(false);
        Debug.Log("¡Sala completada! Puertas abiertas.");
    }
}