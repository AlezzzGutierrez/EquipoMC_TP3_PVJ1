
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Arma inicial")]
    [SerializeField] private WeaponDataSO startingWeapon;

    [Header("Posición del arma")]
    [SerializeField]
    private Vector2 weaponLocalPosition =
        new Vector2(0.45f, 0f);

    [Header("Sistema de recogida")]
    [SerializeField] private float pickupRange = 1.5f;

    [SerializeField] private float dropDistance = 0.8f;

    private WeaponDataSO currentWeapon;

    private GameObject weaponInstance;
    private Transform firePoint;

    private Camera mainCamera;

    private float nextFireTime;

    private WeaponPickup nearestPickup;

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
        AimAtMouse();

        UpdateNearbyPickups();

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropCurrentWeapon();
        }

        if (currentWeapon != null && weaponInstance != null)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
    }

    // =====================================
    // EQUIPAR ARMA
    // =====================================

    public void EquipWeapon(WeaponDataSO weaponData)
    {
        if (weaponData == null || weaponData.weaponPrefab == null)
        {
            Debug.LogWarning("No se pudo equipar el arma.");
            return;
        }

        if (weaponInstance != null)
        {
            Destroy(weaponInstance);
        }

        currentWeapon = weaponData;

        weaponInstance = Instantiate(
            currentWeapon.weaponPrefab,
            transform
        );

        weaponInstance.transform.localPosition =
            weaponLocalPosition;

        weaponInstance.transform.localRotation =
            Quaternion.identity;

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

    // =====================================
    // APUNTAR
    // =====================================

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

    // =====================================
    // DISPARAR
    // =====================================

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
                currentWeapon.projectileSpeed,
                transform
            );
        }
    }

    // =====================================
    // BUSCAR ARMAS CERCANAS
    // =====================================

    private void UpdateNearbyPickups()
    {
        // Ocultar el mensaje del objeto anterior.
        if (nearestPickup != null)
        {
            nearestPickup.ShowPrompt(false);
        }

        nearestPickup = null;

        Collider2D[] nearbyObjects =
            Physics2D.OverlapCircleAll(
                transform.position,
                pickupRange
            );

        float closestDistance = Mathf.Infinity;

        foreach (Collider2D col in nearbyObjects)
        {
            if (!col.CompareTag("WeaponPickup"))
                continue;

            WeaponPickup pickup =
                col.GetComponent<WeaponPickup>();

            if (pickup == null || pickup.WeaponData == null)
                continue;

            float distance = Vector2.Distance(
                transform.position,
                pickup.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestPickup = pickup;
            }
        }

        // Mostrar solamente el mensaje del arma más cercana.
        if (nearestPickup != null)
        {
            nearestPickup.ShowPrompt(true);
        }
    }

    // =====================================
    // RECOGER ARMA CON E
    // =====================================

    private void PickupWeapon()
    {
        if (nearestPickup == null)
            return;

        WeaponDataSO newWeapon =
            nearestPickup.WeaponData;

        Vector3 pickupPosition =
            nearestPickup.transform.position;

        // Si ya tenemos un arma, soltarla.
        if (currentWeapon != null)
        {
            Vector2 dropPosition =
                (Vector2)transform.position - GetAimDirection() * dropDistance;

            SpawnWeaponPickup(currentWeapon, dropPosition);
        }

        // Eliminar el objeto que recogimos.
        Destroy(nearestPickup.gameObject);

        nearestPickup = null;

        // Equipar el arma nueva.
        EquipWeapon(newWeapon);

        Debug.Log("Recogiste: " + newWeapon.weaponName);
    }

    // =====================================
    // SOLTAR ARMA CON Q
    // =====================================

    private void DropCurrentWeapon()
    {
        if (currentWeapon == null)
            return;

        Vector2 dropPosition =
            (Vector2)transform.position + GetAimDirection() * dropDistance;

        SpawnWeaponPickup(currentWeapon, dropPosition);

        // Eliminar el arma de las manos del jugador.
        currentWeapon = null;

        if (weaponInstance != null)
        {
            Destroy(weaponInstance);
        }

        weaponInstance = null;
        firePoint = null;

        Debug.Log("Soltaste el arma.");
    }

    // =====================================
    // CREAR OBJETO RECOGIBLE
    // =====================================

    private void SpawnWeaponPickup(
        WeaponDataSO weaponData,
        Vector2 position
    )
    {
        if (weaponData.pickupPrefab == null)
        {
            Debug.LogError(
                "El arma " + weaponData.weaponName +
                " no tiene Pickup Prefab asignado."
            );

            return;
        }

        GameObject pickupObject = Instantiate(
            weaponData.pickupPrefab,
            position,
            Quaternion.identity
        );

        WeaponPickup pickup =
            pickupObject.GetComponent<WeaponPickup>();

        if (pickup != null)
        {
            pickup.Initialize(weaponData);
        }
        else
        {
            Debug.LogError(
                "El prefab no tiene el componente WeaponPickup."
            );
        }
    }

    // =====================================
    // DIRECCIÓN DE APUNTADO
    // =====================================

    private Vector2 GetAimDirection()
    {
        if (weaponInstance != null)
        {
            return weaponInstance.transform.right;
        }

        if (mainCamera != null)
        {
            Vector3 mousePosition =
                mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction =
                mousePosition - transform.position;

            return direction.normalized;
        }

        return Vector2.right;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            pickupRange
        );
    }
}
