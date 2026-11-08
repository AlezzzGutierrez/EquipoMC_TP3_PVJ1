using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryView : MonoBehaviour
{
    [Header("Referencias")]
    public InventoryController inventoryController;
    public Transform gridContainer;
    public RectTransform selectionCursor;

    [Header("Header Panel (Tooltip)")]
    public Image headerIcon;
    public TextMeshProUGUI headerTitleText;
    public TextMeshProUGUI headerDescriptionText;

    private Image[] slotItemImages; // Array para almacenar las referencias a los iconos de los slots

    void Start()
    {
        // Si no se ha asignado el InventoryController desde el Inspector, intenta obtenerlo
        if (inventoryController == null)
            inventoryController = GetComponentInParent<InventoryController>();

        InitializeSlots(); // Inicializa las referencias de los iconos de los slots
        ForceRefresh(); // Fuerza la actualización visual al inicio
    }

    void Update()
    {
        if (inventoryController == null) return;

        MoveCursor();
        UpdateHeaderInfo();
    }

    // Inicializa las referencias de los iconos de los slots
    void InitializeSlots()
    {
        // Asegura de que gridContainer no sea nulo
        if (gridContainer != null)
        {
            // Crea un array del tamaño del número de hijos en gridContainer
            slotItemImages = new Image[gridContainer.childCount];

            // Recorre cada hijo del gridContainer y obtiene la referencia al componente Image del icono del slot
            for (int i = 0; i < gridContainer.childCount; i++)
            {
                Transform iconChild = gridContainer.GetChild(i).Find("ItemIcon");
                if (iconChild != null)
                {
                    slotItemImages[i] = iconChild.GetComponent<Image>(); // Almacena la referencia al componente Image del icono del slot
                }
            }
        }
    }

    // Método público para forzar la actualización de los slots y la información del header
    public void ForceRefresh()
    {
        if (slotItemImages == null || slotItemImages.Length == 0)
        {
            InitializeSlots();
        }

        UpdateSlots();
        UpdateHeaderInfo();
    }

    // Pinta o limpia los iconos de los slots según lo que tengamos guardado
    void UpdateSlots()
    {
        // Asegura de que inventoryController y slotItemImages no sean nulos
        if (inventoryController == null || slotItemImages == null) return;

        var items = inventoryController.collectedItems; // Obtiene la lista de items del InventoryController

        // Recorre cada slot y actualiza su icono según el item correspondiente
        for (int i = 0; i < slotItemImages.Length; i++)
        {
            // Si el slot no tiene un Image asignado, salta al siguiente
            if (slotItemImages[i] == null) continue;

            bool hasItem = i < items.Count && items[i] != null;

            // Si hay un item en el slot y tiene un icono, lo asigna; de lo contrario, limpia el slot
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

    // Mueve el cursor de selección al slot activo según el índice seleccionado en el InventoryController
    void MoveCursor()
    {
        if (selectionCursor == null || gridContainer == null) return;

        int index = inventoryController.selectedIndex;

        if (index >= 0 && index < gridContainer.childCount)
        {
            Transform activeSlot = gridContainer.GetChild(index);

            selectionCursor.SetParent(activeSlot, false);
            selectionCursor.anchoredPosition = Vector2.zero;
            selectionCursor.localScale = Vector3.one;

            selectionCursor.gameObject.SetActive(true);
            selectionCursor.SetAsLastSibling();
        }
    }

    // Actualiza la información del header (tooltip) según el item seleccionado
    void UpdateHeaderInfo()
    {
        if (inventoryController == null) return;

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
            if (headerIcon) headerIcon.enabled = false;
            if (headerTitleText) headerTitleText.text = "";
            if (headerDescriptionText) headerDescriptionText.text = "";
        }
    }
}