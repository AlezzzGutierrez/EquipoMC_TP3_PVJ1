using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject gameOverPanel;

    private void OnEnable()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnStateChanged += HandleState;
        HandleState(GameManager.Instance.CurrentState);
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged -= HandleState;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged -= HandleState;
    }

    private void HandleState(GameState state)
    {
        Debug.Log("[UIManager] " + name + " recibió: " + state);
        SetPanel(pausePanel, state == GameState.Paused);
        SetPanel(victoryPanel, state == GameState.Victory);
        SetPanel(gameOverPanel, state == GameState.GameOver);
    }

    private void SetPanel(GameObject panel, bool active)
    {
        if (panel == null) return; // el panel fue destruido o no está asignado
        panel.SetActive(active);
    }

    public void Reanudar() => GameManager.Instance.TogglePause();
    public void Reiniciar() => GameManager.Instance.RestartGame();
    public void IrAlMenu() => GameManager.Instance.GoToMainMenu();
}