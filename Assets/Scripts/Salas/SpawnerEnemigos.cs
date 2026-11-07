
using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField] private Transform[] puntosSpawn;

    private bool yaSpawneo = false;

    public void ActivarSpawner()
    {
        if (yaSpawneo) return;

        yaSpawneo = true;

        foreach (Transform punto in puntosSpawn)
        {
            Instantiate(
                enemigoPrefab,
                punto.position,
                punto.rotation
            );
        }

        Debug.Log("¡Enemigos generados!");
    }
}
