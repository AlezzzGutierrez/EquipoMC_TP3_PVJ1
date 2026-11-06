using UnityEngine;

[CreateAssetMenu(fileName = "Player_Default", menuName = "TP3/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Estadísticas de Combate y Movimiento")]
    public float maxHealth = 100f;
    public float moveSpeed = 5f;
    public float damage = 10f;
    public float fireRate = 1f;
    public float staminaRegenRate = 5f;
    public int gold = 0;
    public int defense = 0;
}