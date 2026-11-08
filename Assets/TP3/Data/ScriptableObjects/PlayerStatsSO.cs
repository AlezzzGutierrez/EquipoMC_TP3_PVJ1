using UnityEngine;

[CreateAssetMenu(fileName = "Player_Default", menuName = "TP3/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Vida")]
    public float maxHealth = 100f;

    [Header("Movimiento")]
    public float moveSpeed = 5f;

    [Header("Combate y Economía")]
    public float damage = 10f;
    public float fireRate = 1f;
    public float staminaRegenRate = 5f;
    public int gold = 0;
    public int defense = 0;
}