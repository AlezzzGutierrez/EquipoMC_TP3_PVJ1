using UnityEngine;

public class Meta : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.Instance == null)
        {
            Debug.LogWarning(" ¿Empezaste desde MainMenu?");
            return;
        }

        GameManager.Instance.LevelCompleted();
    }
}