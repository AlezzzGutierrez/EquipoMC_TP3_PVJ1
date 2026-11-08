using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Start, Playing, Paused, Victory, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; } = GameState.Start;

    [Header("Nombres de escenas (deben coincidir exactamente)")]
    [SerializeField] private string mainMenuScene = "MainMenu";
    [SerializeField] private string firstLevelScene = "Nivel1";
    [SerializeField] private string secondLevelScene = "Nivel2";

    // La UI se suscribe a este evento. El GameManager no conoce a la UI.
    public event Action<GameState> OnStateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            TogglePause();
    }

    // ---------- Máquina de estados ----------
    public void SetState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("[GameManager] Estado: " + newState);

        switch (newState)
        {
            case GameState.Playing:
            case GameState.Start:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
            case GameState.Victory:
            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
        }

        OnStateChanged?.Invoke(newState);
    }

    // ---------- Flujo de juego ----------
    public void StartGame()
    {
        SceneManager.LoadScene(firstLevelScene);
        SetState(GameState.Playing);
    }

    public void TogglePause()
    {
        if (CurrentState == GameState.Playing) SetState(GameState.Paused);
        else if (CurrentState == GameState.Paused) SetState(GameState.Playing);
    }

    // ---------- Eventos que notifican los demás scripts ----------
    public void PlayerDied()
    {
        Debug.Log("[GameManager] El jugador murió");
        if (CurrentState == GameState.Playing) SetState(GameState.GameOver);
    }

    public void LevelCompleted()
    {
        Debug.Log("[GameManager] Nivel completado");
        if (CurrentState != GameState.Playing) return;

        if (SceneManager.GetActiveScene().name == firstLevelScene)
        {
            SceneManager.LoadScene(secondLevelScene);
            SetState(GameState.Playing);
        }
        else
        {
            SetState(GameState.Victory);
        }
    }

    // ---------- Acciones de los botones ----------
    public void RestartGame()
    {
        SceneManager.LoadScene(firstLevelScene);
        SetState(GameState.Playing);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
        SetState(GameState.Start);
    }

    public void QuitGame()
    {
        Debug.Log("[GameManager] Saliendo del juego");
        Application.Quit();
    }
}