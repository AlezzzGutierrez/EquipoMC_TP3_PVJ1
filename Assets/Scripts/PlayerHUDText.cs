using UnityEngine;
using TMPro;

public class PlayerHUDText : MonoBehaviour
{
    [Header("Referencias de UI")]
    [SerializeField] private TextMeshProUGUI vidaText;

    // Buscamos el componente PlayerHealth que está en el jugador de la escena
    private PlayerHealth playerHealthRef;

    private void Start()
    {
        BuscarJugador();
    }

    private void Update()
    {
        if (playerHealthRef != null && vidaText != null)
        {
            // 👉 Leemos la vida directamente desde la variable o propiedad que usa el Player Health de tu compañero.
            // (Si en su script la variable se llama 'currentHealth' o 'CurrentHealth', esto la leerá al instante).
            float vidaActual = playerHealthRef.CurrentHealth; // Si te marca error aquí, probá cambiando a playerHealthRef.currentHealth
            
            vidaText.text = "Vida: " + Mathf.Ceil(vidaActual).ToString();
        }
        else
        {
            BuscarJugador();
        }
    }

    private void BuscarJugador()
    {
        // Buscamos directamente el objeto por su Tag "Player" que ya lo tiene asignado en el Inspector
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerHealthRef = playerObj.GetComponent<PlayerHealth>();
        }
    }
}