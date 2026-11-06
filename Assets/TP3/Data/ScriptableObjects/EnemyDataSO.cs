using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_Data", menuName = "Scriptable Objects/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public string nameEnemy = "Enemigo";
    public string description = "Descripción";
    public int maxHealth = 50;
    public int damage = 10;
    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    public int coinReward = 10;
}