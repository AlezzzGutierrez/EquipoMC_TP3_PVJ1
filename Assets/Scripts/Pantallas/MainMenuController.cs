using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;

    public void Iniciar()
    {
        GameManager.Instance.StartGame();
    }

    public void MostrarCreditos(bool mostrar)
    {
        creditsPanel.SetActive(mostrar);
    }

    public void Salir()
    {
        GameManager.Instance.QuitGame();
    }
}