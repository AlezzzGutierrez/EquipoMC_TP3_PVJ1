using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ShopSlotUI : MonoBehaviour
{
    [Header("Referencias UI del Slot")]
    public Image itemIcon;
    public TextMeshProUGUI nameItemText;
    public TextMeshProUGUI descriptionItemText;
    public TextMeshProUGUI priceItemText;
    public Button buyButton;

    // Recibe los datos del Slot de la tienda y los asigna a los elementos de UI correspondientes
    public void SetupSlot(string itemName, string itemDescription, Sprite itemSprite, int itemPrice, UnityAction onBuyAction)
    {
        //Si el texto de nombre y descripción no es nulo, se asignan los valores correspondientes
        if (nameItemText != null)
            nameItemText.text = itemName;

        if (descriptionItemText != null)
            descriptionItemText.text = itemDescription;

        // Muestra el precio calculado en el slot de la tienda, si el texto de precio no es nulo
        if (priceItemText != null)
            priceItemText.text = itemPrice.ToString() + " monedas";

        // Configura el icono del ítem, si el icono no es nulo
        if (itemIcon != null)
        {
            // Se activa el icono y se asigna el sprite correspondiente si el sprite no es nulo; de lo contrario, se desactiva el icono
            if (itemSprite != null)
            {
                itemIcon.gameObject.SetActive(true);
                itemIcon.sprite = itemSprite;
            }
            else
            {
                itemIcon.gameObject.SetActive(false);
            }
        }

        // Configura el botón de compra
        if (buyButton != null)
        {
            TextMeshProUGUI btnText = buyButton.GetComponentInChildren<TextMeshProUGUI>(); // Obtiene el texto del botón para asegurarse de que esté activo y con el texto correcto

            // Restablece el botón para asegurar que esté activo y con el texto correcto en cada nueva oferta
            buyButton.enabled = true; // Asegura que el botón esté habilitado
            buyButton.interactable = true; // Asegura que el botón esté interactuable
            if (btnText != null) btnText.text = "COMPRAR"; // Cambia el texto del botón a "COMPRAR"

            if (onBuyAction != null) // Se asegura de que la acción no sea nula antes de agregarla
            {
                buyButton.onClick.RemoveAllListeners(); // Limpia cualquier listener previo para evitar duplicados
                buyButton.onClick.AddListener(onBuyAction); // Agrega la acción de compra al botón
            }
        }
    }

    // Método para bloquear el botón una vez comprado
    public void MarkAsPurchased()
    {
        // Desactiva el botón de compra y cambia su texto a "Comprado"
        if (buyButton != null)
        {
            buyButton.interactable = false; // Desactiva la interacción del botón para que no pueda ser presionado nuevamente
            buyButton.enabled = false; // Desactiva el botón para que no pueda ser presionado nuevamente
            TextMeshProUGUI btnText = buyButton.GetComponentInChildren<TextMeshProUGUI>(); // Obtiene el texto del botón para cambiarlo a "Comprado"
            if (btnText != null) btnText.text = "Comprado"; // Cambia el texto del botón a "Comprado"
        }
    }
}