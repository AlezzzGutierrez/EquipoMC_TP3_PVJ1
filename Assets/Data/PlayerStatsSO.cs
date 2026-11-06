
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
}
