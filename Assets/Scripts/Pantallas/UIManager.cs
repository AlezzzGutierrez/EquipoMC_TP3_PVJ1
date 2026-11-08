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
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnStateChanged -= HandleState;
    }

    private void HandleState(GameState state)
    {
        pausePanel.SetActive(state == GameState.Paused);
        victoryPanel.SetActive(state == GameState.Victory);
        gameOverPanel.SetActive(state == GameState.GameOver);
    }

    // ---------- Botones (se conectan en On Click) ----------
    public void Reanudar()       => GameManager.Instance.TogglePause();
    public void Reiniciar()      => GameManager.Instance.RestartGame();
    public void IrAlMenu()       => GameManager.Instance.GoToMainMenu();
}