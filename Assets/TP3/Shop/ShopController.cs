using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [Header("Referencias de Datos de Stats del Jugador")]
    public PlayerStatsSO playerStats;

    [Header("Referencia al Inventario")]
    public InventoryController inventoryController;

    [Header("Lista de Ítems (ItemDataSO) Disponibles para la Tienda")]
    public List<ItemDataSO> allPossibleItems;

    [Header("Referencias a los Slots de la UI")]
    public ShopSlotUI[] shopSlots;

    [Header("Costos Iniciales de Mejora")]
    public int healthUpgradeCost = 40;
    public int speedUpgradeCost = 20;
    public int damageUpgradeCost = 30;
    public int attackSpeedUpgradeCost = 25;
    public int staminaUpgradeCost = 15;
    public int defenseUpgradeCost = 35;

    [Header("Incremento de Costo por Compra")]
    public int healthCostIncrease = 15;
    public int speedCostIncrease = 10;
    public int damageCostIncrease = 10;
    public int attackSpeedCostIncrease = 10;
    public int staminaCostIncrease = 10;
    public int defenseCostIncrease = 10;

    [Header("Contadores de Compras")]
    public int healthUpgradeCount = 0;
    public int speedUpgradeCount = 0;
    public int damageUpgradeCount = 0;
    public int attackSpeedUpgradeCount = 0;
    public int staminaUpgradeCount = 0;
    public int defenseUpgradeCount = 0;

    [Header("Valores de Incremento a los Stats tras Comprar")]
    public int healthIncrease = 20;
    public float speedIncrease = 1.5f;
    public int damageIncrease = 5;
    public float attackSpeedIncrease = 0.2f;
    public float staminaIncrease = 5f;
    public int defenseIncrease = 5;

    private ItemDataSO[] currentShopOffers = new ItemDataSO[4];

    [Header("Panel Visual de la Tienda a Cerrar")]
    public GameObject shopPanelObject;

    void OnEnable()
    {
        GenerateRandomShopOffers();
    }

    public void GenerateRandomShopOffers()
    {
        if (allPossibleItems == null || allPossibleItems.Count == 0) return;
        List<ItemDataSO> tempItems = new List<ItemDataSO>(allPossibleItems);

        for (int i = 0; i < tempItems.Count; i++)
        {
            ItemDataSO temp = tempItems[i];
            int randomIndex = Random.Range(i, tempItems.Count);
            tempItems[i] = tempItems[randomIndex];
            tempItems[randomIndex] = temp;
        }

        for (int i = 0; i < shopSlots.Length; i++)
        {
            if (i < tempItems.Count && shopSlots[i] != null)
            {
                currentShopOffers[i] = tempItems[i];
                ConfigureSlotUI(i, currentShopOffers[i]);
            }
        }
    }

    private int GetItemCost(ItemDataSO item)
    {
        if (item == null || string.IsNullOrEmpty(item.itemName)) return 30;

        string nameLower = item.itemName.ToLower();

        if (nameLower.Contains("espada filosa"))
            return damageUpgradeCost + (damageUpgradeCount * damageCostIncrease);

        if (nameLower.Contains("escudo protector"))
            return defenseUpgradeCost + (defenseUpgradeCount * defenseCostIncrease);

        if (nameLower.Contains("elíxir de reflejos"))
            return attackSpeedUpgradeCost + (attackSpeedUpgradeCount * attackSpeedCostIncrease);

        if (nameLower.Contains("corazón de vitalidad"))
            return healthUpgradeCost + (healthUpgradeCount * healthCostIncrease);

        if (nameLower.Contains("diamante céfiro"))
            return speedUpgradeCost + (speedUpgradeCount * speedCostIncrease);

        if (nameLower.Contains("estrella de vigor"))
            return staminaUpgradeCost + (staminaUpgradeCount * staminaCostIncrease);

        return 30;
    }

    private string GetDetailedDescription(ItemDataSO item)
    {
        if (item == null || string.IsNullOrEmpty(item.itemName)) return "";

        string baseDesc = item.itemDescription;
        string nameLower = item.itemName.ToLower();
        string statInfo = "";

        if (nameLower.Contains("espada filosa"))
            statInfo = $"\n\n<b>Efecto:</b> +{damageIncrease} de Daño";
        else if (nameLower.Contains("escudo protector"))
            statInfo = $"\n\n<b>Efecto:</b> +{defenseIncrease} de Defensa";
        else if (nameLower.Contains("elíxir de reflejos"))
            statInfo = $"\n\n<b>Efecto:</b> +{attackSpeedIncrease} Vel. Ataque";
        else if (nameLower.Contains("corazón de vitalidad"))
            statInfo = $"\n\n<b>Efecto:</b> +{healthIncrease} de Vida Máx.";
        else if (nameLower.Contains("diamante céfiro"))
            statInfo = $"\n\n<b>Efecto:</b> +{speedIncrease} Vel. Movimiento";
        else if (nameLower.Contains("estrella de vigor"))
            statInfo = $"\n\n<b>Efecto:</b> +{staminaIncrease}s Regén. Estamina";

        return baseDesc + statInfo;
    }

    private void ConfigureSlotUI(int slotIndex, ItemDataSO chosenItem)
    {
        if (chosenItem != null && shopSlots[slotIndex] != null)
        {
            int currentIndex = slotIndex;
            int calculatedPrice = GetItemCost(chosenItem);
            string detailedDesc = GetDetailedDescription(chosenItem);

            shopSlots[currentIndex].SetupSlot(
                chosenItem.itemName,
                detailedDesc,
                chosenItem.itemIcon,
                calculatedPrice,
                () => TryBuyItem(currentIndex) 
            );
        }
    }

    public void TryBuyItem(int slotIndex)
    {
        if (playerStats == null) return;
        if (slotIndex < 0 || slotIndex >= currentShopOffers.Length) return;

        ItemDataSO item = currentShopOffers[slotIndex];
        if (item == null) return;

        bool purchaseSuccess = false;
        int cost = GetItemCost(item);

        Debug.Log($"Intentando comprar slot {slotIndex}: {item.itemName} por {cost} oro. Oro actual: {playerStats.gold}");

        if (playerStats.gold >= cost)
        {
            playerStats.gold -= cost;
            string nameLower = item.itemName != null ? item.itemName.ToLower() : "";

            if (nameLower.Contains("espada filosa"))
            {
                playerStats.damage += damageIncrease;
                damageUpgradeCount++;
                purchaseSuccess = true;
            }
            else if (nameLower.Contains("escudo protector"))
            {
                playerStats.defense += defenseIncrease;
                defenseUpgradeCount++;
                purchaseSuccess = true;
            }
            else if (nameLower.Contains("elíxir de reflejos"))
            {
                playerStats.fireRate += attackSpeedIncrease;
                attackSpeedUpgradeCount++;
                purchaseSuccess = true;
            }
            else if (nameLower.Contains("corazón de vitalidad"))
            {
                playerStats.maxHealth += healthIncrease;
                healthUpgradeCount++;
                purchaseSuccess = true;
            }
            else if (nameLower.Contains("diamante céfiro"))
            {
                playerStats.moveSpeed += speedIncrease;
                speedUpgradeCount++;
                purchaseSuccess = true;
            }
            else if (nameLower.Contains("estrella de vigor"))
            {
                playerStats.staminaRegenRate += staminaIncrease;
                staminaUpgradeCount++;
                purchaseSuccess = true;
            }
        }
        else
        {
            Debug.Log("No hay suficiente oro para esta compra.");
        }

        if (purchaseSuccess)
        {
            if (inventoryController != null)
            {
                inventoryController.AddItem(item);
            }

            shopSlots[slotIndex].MarkAsPurchased();
            Debug.Log("¡Compra exitosa! Slot marcado como comprado.");
        }
    }

    public void CloseShop()
    {
        if (shopPanelObject != null)
        {
            shopPanelObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}