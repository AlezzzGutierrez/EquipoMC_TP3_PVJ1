
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private PlayerStatsSO playerStats;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private float nextFireTime;

    private void Update()
    {
        if (playerStats == null || projectilePrefab == null)
            return;

        if (Input.GetMouseButton(0))
        {
            TryShoot();
        }
    }

    private void TryShoot()
    {
        if (Time.time < nextFireTime)
            return;

        Shoot();

        nextFireTime = Time.time + playerStats.fireRate;
    }

    private void Shoot()
    {
        Vector3 spawnPosition = firePoint != null
            ? firePoint.position
            : transform.position + transform.up * 0.6f;

        GameObject projectile = Instantiate(
            projectilePrefab,
            spawnPosition,
            transform.rotation
        );

        PlayerProjectile projectileScript =
            projectile.GetComponent<PlayerProjectile>();

        if (projectileScript != null)
        {
            projectileScript.Initialize(
                playerStats.damage,
                playerStats.projectileSpeed
            );
        }
    }
}
