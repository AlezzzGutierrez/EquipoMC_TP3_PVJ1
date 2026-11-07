
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewWeapon",
    menuName = "TP3/Weapon Data"
)]
public class WeaponDataSO : ScriptableObject
{
    [Header("Información")]
    public string weaponName = "Nueva arma";

    [Header("Combate")]
    [Min(0)]
    public float damage = 10f;

    [Tooltip("Segundos entre disparos.")]
    [Min(0.01f)]
    public float fireRate = 0.5f;

    [Min(0)]
    public float projectileSpeed = 12f;

    [Header("Prefabs")]
    public GameObject weaponPrefab;
    public GameObject projectilePrefab;
}
