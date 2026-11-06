using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [Header("Referencias de Datos de ScriptableObjects: Stats del Jugador")]
    public PlayerStatsSO playerStats;

    [Header("Referencias de UI para Mensajes")]
    public TextMeshProUGUI messageText;     // Referencia al componente TextMeshProUGUI para mostrar mensajes al jugador

    [Header("Mensaje por defecto")]
    public string insufficientFundsMessage = "No tienes suficientes monedas para comprar esta mejora.";

    [Header("Costos Inicial de Mejora")]
    public int healthUpgradeCost = 40;
    public int speedUpgradeCost = 20;
    public int damageUpgradeCost = 30;
    public int attackSpeedUpgradeCost = 25;
    public int staminaUpgradeCost = 15;

    [Header("Incremento de Costo por Compra")]
    public int healthCostIncrease = 15;
    public int speedCostIncrease = 10;
    public int damageCostIncrease = 10;
    public int attackSpeedCostIncrease = 10;
    public int staminaCostIncrease = 10;

    [Header("Contadores de Compras")]
    public int healthUpgradeCount = 0;
    public int speedUpgradeCount = 0;
    public int damageUpgradeCount = 0;
    public int attackSpeedUpgradeCount = 0;
    public int staminaUpgradeCount = 0;

    [Header("Valores de Incremento a los Stats del Jugador")]
    public int healthIncrease = 20;
    public float speedIncrease = 1.5f;
    public int damageIncrease = 5;
    public float attackSpeedIncrease = 0.2f;
    public float staminaIncrease = 5f; 

    //-- MEJORA DE VIDA --
    public void BuyHealthUpgrade()
    {
        // Verifica si playerStats no es nulo antes de acceder a sus propiedades
        if (playerStats == null) return;

        // Calcula el costo actual en base a las compras realizadas
        int currentCost = healthUpgradeCost + (healthUpgradeCount * healthCostIncrease);

        // Comprueba si el jugador tiene suficientes monedas para comprar la mejora de vida
        if (playerStats.gold >= currentCost)
        {
            // Resta el costo de la mejora de vida de las monedas del jugador
            playerStats.gold -= currentCost;
            // Aumenta la vida máxima del jugador en la cantidad especificada
            playerStats.maxHealth += healthIncrease;
            // Incrementa la cantidad de veces compradas
            healthUpgradeCount++;
        }
        else
        {
            ShowMessage(insufficientFundsMessage);
        }
    }

    //-- MEJORA DE VELOCIDAD --
    public void BuySpeedUpgrade()
    {
        // Verifica si playerStats no es nulo antes de acceder a sus propiedades
        if (playerStats == null) return;

        // Calcula el costo actual en base a las compras realizadas
        int currentCost = speedUpgradeCost + (speedUpgradeCount * speedCostIncrease);

        // Comprueba si el jugador tiene suficientes monedas para comprar la mejora de velocidad
        if (playerStats.gold >= currentCost)
        {
            // Resta el costo de la mejora de velocidad de las monedas del jugador
            playerStats.gold -= currentCost;
            // Aumenta la velocidad de movimiento del jugador en la cantidad especificada
            playerStats.moveSpeed += speedIncrease;
            // Incrementa la cantidad de veces compradas
            speedUpgradeCount++;
        }
        else
        {
            ShowMessage(insufficientFundsMessage);
        }
    }

    //-- MEJORA DE DAÑO --
    public void BuyDamageUpgrade()
    {
        // Verifica si playerStats no es nulo antes de acceder a sus propiedades
        if (playerStats == null) return;

        // Calcula el costo actual en base a las compras realizadas
        int currentCost = damageUpgradeCost + (damageUpgradeCount * damageCostIncrease);

        // Comprueba si el jugador tiene suficientes monedas para comprar la mejora de daño
        if (playerStats.gold >= currentCost)
        {
            // Resta el costo de la mejora de daño de las monedas del jugador
            playerStats.gold -= currentCost;
            // Aumenta el daño del jugador en la cantidad especificada
            playerStats.damage += damageIncrease;
            // Incrementa la cantidad de veces compradas
            damageUpgradeCount++;
        }
        else
        {
            ShowMessage(insufficientFundsMessage);
        }
    }

    //-- MEJORA DE VELOCIDAD DE ATAQUE --
    public void BuyAttackSpeedUpgrade()
    {
        // Verifica si playerStats no es nulo antes de acceder a sus propiedades
        if (playerStats == null) return;

        // Calcula el costo actual en base a las compras realizadas
        int currentCost = attackSpeedUpgradeCost + (attackSpeedUpgradeCount * attackSpeedCostIncrease);

        // Comprueba si el jugador tiene suficientes monedas para comprar la mejora de velocidad de ataque
        if (playerStats.gold >= currentCost)
        {
            // Resta el costo de la mejora de velocidad de ataque de las monedas del jugador
            playerStats.gold -= currentCost;
            // Aumenta la velocidad de ataque del jugador en la cantidad especificada
            playerStats.fireRate += attackSpeedIncrease;
            // Incrementa la cantidad de veces compradas
            attackSpeedUpgradeCount++;
        }
        else
        {
            ShowMessage(insufficientFundsMessage);
        }
    }

    //-- MEJORA DE ESTAMINA --
    public void BuyStaminaUpgrade()
    {
        // Verifica si playerStats no es nulo antes de acceder a sus propiedades
        if (playerStats == null) return;

        // Calcula el costo actual en base a las compras realizadas
        int currentCost = staminaUpgradeCost + (staminaUpgradeCount * staminaCostIncrease);

        // Comprueba si el jugador tiene suficientes monedas para comprar la mejora de estamina
        if (playerStats.gold >= currentCost)
        {
            // Resta el costo de la mejora de estamina de las monedas del jugador
            playerStats.gold -= currentCost;
            // Aumenta la estamina del jugador en la cantidad especificada
            playerStats.staminaRegenRate += staminaIncrease;
            // Incrementa la cantidad de veces compradas
            staminaUpgradeCount++;
        }
        else
        {
            ShowMessage(insufficientFundsMessage);
        }
    }

    // Función auxiliar para mostrar mensajes en pantalla rápidamente
    private void ShowMessage(string message)
    {
        // Verifica si messageText no es nulo antes de intentar asignar el texto
        if (messageText != null)
        {
            // Asigna el mensaje al componente TextMeshProUGUI para mostrarlo en pantalla
            messageText.text = message;
        }
    }
}