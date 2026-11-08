using UnityEngine;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    [Header("Prueba inicial")]
    public List<ItemDataSO> itemsDePrueba = new List<ItemDataSO>();

    [Header("Configuración de UI")]
    public GameObject inventoryPanel;

    [Header("Referencia a la Vista del Inventario")]
    public InventoryView inventoryView;

    [Header("Lista de Elementos del Inventario")]
    public List<ItemDataSO> collectedItems = new List<ItemDataSO>();

    [Header("Selección de Inventario")]
    public int selectedIndex = 0; // Índice del slot actualmente seleccionado en la cuadrícula del inventario
    public int columnsCount = 10; //Cantidad de columnas en la cuadrícula del inventario

    [Header("Interacción en el Mundo")]
    public ItemDataSO currentNearbyItem; // Referencia al ítem cercano que el jugador puede recoger

    [Header("Capacidad del Inventario")]
    public int maxSlots = 20; // Número máximo de slots en el inventario

    private bool isOpen = false;

    // Evento para notificar cuando un ítem es recogido
    public static event System.Action<ItemDataSO> OnItemPickedUp;
    
    // Método para invocar el evento de recogida de ítem
    public static void TriggerItemPickedUp(ItemDataSO item)
    {
        OnItemPickedUp?.Invoke(item);
    }

    //Llamado cuando el objeto se habilita
    void OnEnable()
    {
        OnItemPickedUp += HandleItemPickedUp;
    }

    //Llamado cuando el objeto se deshabilita
    void OnDisable()
    {
        OnItemPickedUp -= HandleItemPickedUp;
    }

    void Start()
    {
        // Inicialmente, el panel del inventario está cerrado
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }

        // Inicializa la referencia a InventoryView si no se ha asignado en el Inspector
        if (inventoryView == null)
        {
            inventoryView = GetComponentInChildren<InventoryView>(true);
        }

        // Inicializa la vista del inventario al inicio con los ítems de prueba si los hay
        if (itemsDePrueba != null && itemsDePrueba.Count > 0)
        {
            foreach (var item in itemsDePrueba)
            {
                AddItem(item);
            }
        }
    }

    void Update()
    {
        // Manejo de entrada para abrir/cerrar el inventario y recoger ítems con la tecla I
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory(); // Alterna la visibilidad del inventario
        }

        // Manejo de entrada para recoger ítems cercanos con la tecla F
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentNearbyItem != null) // Verifica si hay un ítem cercano para recoger
            {
                if (AddItem(currentNearbyItem))
                {
                    currentNearbyItem = null; // Una vez recogido, se elimina la referencia al ítem cercano
                }
            }
        }

        // Manejo de entrada para navegar por los slots del inventario a traves de las teclas de flecha o WASD
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (collectedItems.Count > 0 && selectedIndex < collectedItems.Count - 1)
            {
                selectedIndex++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (selectedIndex > 0)
            {
                selectedIndex--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (selectedIndex + columnsCount < collectedItems.Count)
            {
                selectedIndex += columnsCount;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (selectedIndex - columnsCount >= 0)
            {
                selectedIndex -= columnsCount;
            }
        }

        // Manejo de entrada para eliminar un ítem del inventario con la tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (collectedItems.Count > 0 && selectedIndex < collectedItems.Count)
            {
                ItemDataSO itemToRemoveFromSlot = collectedItems[selectedIndex];

                if (RemoveItem(itemToRemoveFromSlot))
                {
                    if (selectedIndex >= collectedItems.Count && collectedItems.Count > 0)
                    {
                        selectedIndex = collectedItems.Count - 1;
                    }
                }
            }
        }
    }

    // Maneja la recogida de un ítem y lo agrega al inventario
    private void HandleItemPickedUp(ItemDataSO newItem)
    {
        if (newItem != null)
        {
            AddItem(newItem);
        }
    }

    //Cambia el estado del inventario (abierto/cerrado) y actualiza la vista si se abre
    public bool ToggleInventory()
    {
        isOpen = !isOpen;
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(isOpen);
            if (isOpen && inventoryView != null)
            {
                inventoryView.ForceRefresh(); // Actualiza la vista del inventario al abrirlo
            }
        }
        return isOpen; // Devuelve el estado actual del inventario (abierto o cerrado)
    }

    // Método para agregar un ítem al inventario, devuelve true si se agregó con éxito, false si el inventario está lleno
    public bool AddItem(ItemDataSO newItem)
    {
        // Verifica si hay espacio en el inventario antes de agregar el ítem
        if (collectedItems.Count < maxSlots)
        {
            collectedItems.Add(newItem);

            // Refresca la interfaz visual al instante
            if (inventoryView != null)
            {
                inventoryView.ForceRefresh();
            }

            return true;
        }

        return false;
    }

    // Método para eliminar un ítem del inventario, devuelve true si se eliminó con éxito, false si el ítem no estaba en el inventario
    public bool RemoveItem(ItemDataSO itemToRemove)
    {
        if (collectedItems.Contains(itemToRemove))
        {
            collectedItems.Remove(itemToRemove);

            if (inventoryView != null)
            {
                inventoryView.ForceRefresh();
            }

            return true;
        }
        return false;
    }
}