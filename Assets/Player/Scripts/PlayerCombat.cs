
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Arma inicial")]
    [SerializeField] private WeaponDataSO startingWeapon;

    [Header("Posición del arma")]
    [SerializeField]
    private Vector2 weaponLocalPosition =
        new Vector2(0.45f, 0f);

    private WeaponDataSO currentWeapon;

    private GameObject weaponInstance;
    private Transform firePoint;

    private Camera mainCamera;

    private float nextFireTime;

    private void Start()
    {
        mainCamera = Camera.main;

        if (startingWeapon != null)
        {
            EquipWeapon(startingWeapon);
        }
    }

    private void Update()
    {
        if (currentWeapon == null || weaponInstance == null)
            return;

        AimAtMouse();

        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    public void EquipWeapon(WeaponDataSO weaponData)
    {
        if (weaponData == null || weaponData.weaponPrefab == null)
        {
            Debug.LogWarning("No se pudo equipar el arma.");
            return;
        }

        // Eliminar el arma anterior.
        if (weaponInstance != null)
        {
            Destroy(weaponInstance);
        }

        currentWeapon = weaponData;

        // Crear el modelo visual del arma.
        weaponInstance = Instantiate(
            currentWeapon.weaponPrefab,
            transform
        );

        weaponInstance.transform.localPosition =
            weaponLocalPosition;

        weaponInstance.transform.localRotation =
            Quaternion.identity;

        // Buscar el punto desde donde salen los disparos.
        firePoint = weaponInstance.transform.Find("FirePoint");

        if (firePoint == null)
        {
            Debug.LogError(
                "El arma no tiene un objeto llamado FirePoint."
            );
        }

        nextFireTime = 0f;

        Debug.Log("Arma equipada: " + currentWeapon.weaponName);
    }

    private void AimAtMouse()
    {
        if (mainCamera == null || weaponInstance == null)
            return;

        Vector3 mousePosition =
            mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction =
            mousePosition - weaponInstance.transform.position;

        float angle = Mathf.Atan2(
            direction.y,
            direction.x
        ) * Mathf.Rad2Deg;

        weaponInstance.transform.rotation =
            Quaternion.Euler(0f, 0f, angle);
    }

    private void Shoot()
    {
        if (firePoint == null)
            return;

        if (Time.time < nextFireTime)
            return;

        if (currentWeapon.projectilePrefab == null)
            return;

        nextFireTime = Time.time + currentWeapon.fireRate;

        GameObject projectile = Instantiate(
            currentWeapon.projectilePrefab,
            firePoint.position,
            weaponInstance.transform.rotation
        );

        PlayerProjectile projectileScript =
            projectile.GetComponent<PlayerProjectile>();

        if (projectileScript != null)
        {
            projectileScript.Initialize(
                currentWeapon.damage,
                currentWeapon.projectileSpeed
            );
        }
    }
}
