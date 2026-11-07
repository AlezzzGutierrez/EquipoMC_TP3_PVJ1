using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "TP3/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public float life = 5f;
    public float speed = 3f;
    public float damage = 1f;
    public float range = 1.5f;
    public float cooldown = 1f;
    public int reward = 10;
}