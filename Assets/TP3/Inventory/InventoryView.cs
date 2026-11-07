using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryView : MonoBehaviour
{
    [Header("References")]
    public InventoryController inventoryController;
    public Transform gridContainer;
    public RectTransform selectionCursor;

    [Header("Header Panel (Tooltip)")]
    public Image headerIcon;
    public TextMeshProUGUI headerTitleText;
    public TextMeshProUGUI headerDescriptionText;

    private Image[] slotItemImages;

    void Start()
    {
        // Si no enganchamos el controlador, lo buscamos en el mismo objeto
        if (inventoryController == null)
            inventoryController = GetComponent<InventoryController>();

        // Guardamos las referencias de los iconitos de cada slot al arrancar
        if (gridContainer != null)
        {
            slotItemImages = new Image[gridContainer.childCount];

            for (int i = 0; i < gridContainer.childCount; i++)
            {
                Transform iconChild = gridContainer.GetChild(i).Find("ItemIcon");
                if (iconChild != null)
                {
                    slotItemImages[i] = iconChild.GetComponent<Image>();
                }
            }
        }
    }

    void Update()
    {
        if (inventoryController == null) return;

        UpdateSlots();
        MoveCursor();
        UpdateHeaderInfo();
    }

    // Pinta o limpia los iconos de los slots según lo que tengamos guardado
    void UpdateSlots()
    {
        var items = inventoryController.collectedItems;

        for (int i = 0; i < slotItemImages.Length; i++)
        {
            if (slotItemImages[i] == null) continue;

            bool hasItem = i < items.Count && items[i] != null;

            if (hasItem && items[i].itemIcon != null)
            {
                slotItemImages[i].sprite = items[i].itemIcon;
                slotItemImages[i].enabled = true;
                slotItemImages[i].color = Color.white;
            }
            else
            {
                slotItemImages[i].sprite = null;
                slotItemImages[i].enabled = false; // Apaga la imagen si el slot está vacío
            }
        }
    }

    // Salta el cursor al slot que estamos seleccionando
    void MoveCursor()
    {
        if (selectionCursor == null || gridContainer == null) return;

        int index = inventoryController.selectedIndex;

        if (index >= 0 && index < gridContainer.childCount)
        {
            Transform activeSlot = gridContainer.GetChild(index);

            // Lo metemos como hijo del slot activo y lo centramos
            selectionCursor.SetParent(activeSlot, false);
            selectionCursor.anchoredPosition = Vector2.zero;
            selectionCursor.localScale = Vector3.one;
            
            selectionCursor.gameObject.SetActive(true);
            selectionCursor.SetAsLastSibling(); // Lo mandamos al frente para que no se tape
        }
    }

    // Muestra el nombre, descripción e icono arriba cuando seleccionas algo
    void UpdateHeaderInfo()
    {
        var items = inventoryController.collectedItems;
        int index = inventoryController.selectedIndex;

        bool isValidIndex = index >= 0 && index < items.Count && items[index] != null;

        if (isValidIndex)
        {
            ItemDataSO item = items[index];

            if (headerIcon) { headerIcon.sprite = item.itemIcon; headerIcon.enabled = true; headerIcon.color = Color.white; }
            if (headerTitleText) headerTitleText.text = item.itemName;
            if (headerDescriptionText) headerDescriptionText.text = item.itemDescription;
        }
        else
        {
            // Si el slot está vacío, limpiamos el panel de arriba
            if (headerIcon) headerIcon.enabled = false;
            if (headerTitleText) headerTitleText.text = "";
            if (headerDescriptionText) headerDescriptionText.text = "";
        }
    }
}