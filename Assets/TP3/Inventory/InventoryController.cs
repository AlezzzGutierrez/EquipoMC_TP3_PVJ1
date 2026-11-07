using UnityEngine;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    [Header("Configuración de UI")]
    public GameObject inventoryPanel; // Referencia al panel del inventario en la interfaz de usuario

    [Header("Lista de Elementos del Inventario")]
    public List<ItemDataSO> collectedItems = new List<ItemDataSO>(); // Lista que almacena los elementos recolectados por el jugador

    [Header("Selección de Inventario")]
    public int selectedIndex = 0; // Representa el índice del elemento actualmente seleccionado en la lista de elementos recolectados
    public int columnsCount = 10;  // Cantidad de columnas por fila en la cuadrícula visual

    [Header("Interacción en el Mundo")]
    public ItemDataSO currentNearbyItem; // Referencia al ítem cercano que el jugador puede recoger

    [Header("Capacidad del Inventario")]
    public int maxSlots = 20; // Número máximo de elementos que el inventario puede contener

    private bool isOpen = false; //Variable para controlar si el inventario está abierto o cerrado

    void Start()
    {
        // Asegura que el panel del inventario esté cerrado al inicio del juego
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Detecta la tecla "I" para abrir o cerrar el inventario
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        // Detecta la tecla "F" exclusivamente para recoger el ítem cercano del suelo
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentNearbyItem != null)
            {
                // Intenta agregar el ítem al inventario usando tu función
                if (AddItem(currentNearbyItem))
                {
                    // Limpia la referencia del suelo una vez recogido con éxito
                    currentNearbyItem = null;
                }
            }
        }

        // Detecta las teclas de movimiento horizontal (Derecha / D)
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            // Mueve la selección a la derecha si no está en el último elemento
            if (collectedItems.Count > 0 && selectedIndex < collectedItems.Count - 1)
            {
                // Aumenta el índice seleccionado para moverse a la derecha
                selectedIndex++;
            }
        }
        // Detecta las teclas de movimiento horizontal (Izquierda / A)
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            // Mueve la selección a la izquierda si no está en el primer elemento
            if (selectedIndex > 0)
            {
                // Disminuye el índice seleccionado para moverse a la izquierda
                selectedIndex--;
            }
        }
        // Detecta las teclas de movimiento vertical hacia ABAJO (Abajo / S)
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            // Mueve la selección hacia abajo en la cuadrícula si no está en la última fila
            if (selectedIndex + columnsCount < collectedItems.Count)
            {
                // Aumenta el índice seleccionado para moverse hacia abajo en la cuadrícula
                selectedIndex += columnsCount;
            }
        }
        // Detecta las teclas de movimiento vertical hacia ARRIBA (Arriba / W)
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            // Mueve la selección hacia arriba en la cuadrícula si no está en la primera fila
            if (selectedIndex - columnsCount >= 0)
            {
                // Disminuye el índice seleccionado para moverse hacia arriba en la cuadrícula
                selectedIndex -= columnsCount;
            }
        }

        // Detecta la tecla (R) para eliminar el elemento seleccionado en el inventario
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Verifica que haya elementos en el inventario y que el índice seleccionado sea válido
            if (collectedItems.Count > 0 && selectedIndex < collectedItems.Count)
            {
                // Obtiene el ítem que se desea eliminar del inventario
                ItemDataSO itemToRemoveFromSlot = collectedItems[selectedIndex];

                // Usa tu misma función RemoveItem
                if (RemoveItem(itemToRemoveFromSlot))
                {
                    // Ajusta el índice seleccionado para que no se quede fuera de rango si borramos el último
                    if (selectedIndex >= collectedItems.Count && collectedItems.Count > 0)
                    {
                        // Si el índice seleccionado es mayor o igual al número de elementos, ajusta el índice al último elemento
                        selectedIndex = collectedItems.Count - 1;
                    }
                }
            }
        }
    }

    // Función para abrir o cerrar el inventario
    public bool ToggleInventory()
    {
        // Cambia el estado del inventario
        isOpen = !isOpen;
        // Muestra u oculta el panel del inventario según el estado
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(isOpen);
        }
        return isOpen;
    }

    // Función para agregar un ítem al inventario
    public bool AddItem(ItemDataSO newItem)
    {
        // Verifica si hay espacio en el inventario antes de agregar el nuevo ítem
        if (collectedItems.Count < maxSlots)
        {
            // Agrega el nuevo ítem a la lista de elementos recolectados
            collectedItems.Add(newItem);
            return true;
        }
        // Si no hay espacio, devuelve false indicando que no se pudo agregar el ítem
        return false;
    }

    //Función para eliminar un ítem del inventario
    public bool RemoveItem(ItemDataSO itemToRemove)
    {
        // Verifica si el ítem existe en la lista de recolectados
        if (collectedItems.Contains(itemToRemove))
        {
            // Elimina el ítem de la lista de elementos recolectados
            collectedItems.Remove(itemToRemove);
            return true;
        }
        return false;
    }
}