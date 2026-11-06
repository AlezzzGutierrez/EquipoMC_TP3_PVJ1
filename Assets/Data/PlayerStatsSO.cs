
using UnityEngine;

[CreateAssetMenu(
    fileName = "Player_Default",
    menuName = "TP3/Player Stats"
)]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Vida")]
    [Min(1)]
    public float maxHealth = 100f;

    [Header("Movimiento")]
    [Min(0)]
    public float moveSpeed = 5f;

    [Header("Combate")]
    [Min(0)]
    public float damage = 10f;

    [Tooltip("Tiempo en segundos entre cada disparo.")]
    [Min(0.01f)]
    public float fireRate = 0.5f;

    [Min(0)]
    public float projectileSpeed = 12f;
}
