
using UnityEngine;
using TMPro;

public class ChestController : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float interactionRange = 2f;

    [Header("Recompensas posibles")]
    [SerializeField] private WeaponDataSO[] possibleWeapons;

    [Header("Objetos visuales")]
    [SerializeField] private TMP_Text openPrompt;
    [SerializeField] private Transform chestLid;

    [Header("Aparición del arma")]
    [SerializeField]
    private Vector2 lootOffset =
        new Vector2(0f, 0.8f);

    private Transform player;

    private bool isOpened = false;
    private bool playerNearby = false;

    private void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError(
                "No se encontró un objeto con el tag Player."
            );
        }

        ShowPrompt(false);
    }

    private void Update()
    {
        if (isOpened || player == null)
        {
            ShowPrompt(false);
            return;
        }

        float distance = Vector2.Distance(
            player.position,
            transform.position
        );

        playerNearby = distance <= interactionRange;

        ShowPrompt(playerNearby);

        if (playerNearby && Input.GetKeyDown(KeyCode.F))
        {
            OpenChest();
        }
    }

    // =====================================
    // ABRIR COFRE
    // =====================================

    private void OpenChest()
    {
        if (isOpened)
            return;

        isOpened = true;

        ShowPrompt(false);

        // Elegir un arma aleatoria.
        if (possibleWeapons == null ||
            possibleWeapons.Length == 0)
        {
            Debug.LogWarning(
                "El cofre no tiene armas configuradas."
            );

            return;
        }

        WeaponDataSO selectedWeapon =
            possibleWeapons[
                Random.Range(0, possibleWeapons.Length)
            ];

        if (selectedWeapon == null)
        {
            Debug.LogWarning(
                "Una de las armas configuradas es nula."
            );

            return;
        }

        SpawnLoot(selectedWeapon);

        // Abrir visualmente la tapa.
        if (chestLid != null)
        {
            chestLid.localRotation =
                Quaternion.Euler(0f, 0f, -35f);
        }

        Debug.Log(
            "Cofre abierto. Encontraste: " +
            selectedWeapon.weaponName
        );
    }

    // =====================================
    // SOLTAR RECOMPENSA
    // =====================================

    private void SpawnLoot(WeaponDataSO weaponData)
    {
        if (weaponData.pickupPrefab == null)
        {
            Debug.LogError(
                "El arma no tiene Pickup Prefab asignado."
            );

            return;
        }

        Vector2 spawnPosition =
            (Vector2)transform.position + lootOffset;

        GameObject loot = Instantiate(
            weaponData.pickupPrefab,
            spawnPosition,
            Quaternion.identity
        );

        WeaponPickup pickup =
            loot.GetComponent<WeaponPickup>();

        if (pickup != null)
        {
            pickup.Initialize(weaponData);
        }
    }

    // =====================================
    // TEXTO DE INTERACCIÓN
    // =====================================

    private void ShowPrompt(bool show)
    {
        if (openPrompt != null)
        {
            openPrompt.gameObject.SetActive(show);
        }
    }

    // =====================================
    // VISUALIZAR RANGO
    // =====================================

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(
            transform.position,
            interactionRange
        );
    }
}
