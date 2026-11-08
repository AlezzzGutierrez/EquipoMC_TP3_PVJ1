using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "TP3/Enemies/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Información Básica")]
    [SerializeField] private string enemyName = "Enemigo";
    [SerializeField] private Sprite enemyIcon;

    [Header("Estadísticas de Combate y Movimiento")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1f;

    [Header("Recompensa y Puntuación")]
    [SerializeField] private int rewardValue = 10;

    [Header("Enemigos a Distancia (Opcional)")]
    [SerializeField] private bool isRanged = false;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 5f;

    // Propiedades públicas (con alias para que funcionen tanto MaxHealth/Reward como life/reward)
    public string EnemyName => enemyName;
    public Sprite EnemyIcon => enemyIcon;
    public float MaxHealth => maxHealth;
    public float life => maxHealth; 
    public float MoveSpeed => moveSpeed;
    public float AttackDamage => attackDamage;
    public float AttackRange => attackRange;
    public float AttackCooldown => attackCooldown;
    
    public int Reward => rewardValue;
    public int reward => rewardValue;   

    public bool IsRanged => isRanged;
    public GameObject ProjectilePrefab => projectilePrefab;
    public float ProjectileSpeed => projectileSpeed;
}