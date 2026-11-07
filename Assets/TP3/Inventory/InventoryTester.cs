using UnityEngine;
using System.Collections.Generic;

public class InventoryTester : MonoBehaviour
{
    [Header("Referencias")]
    public InventoryController inventoryController;

    [Header("Lista de elementos para poblar al iniciar")]
    public List<ItemDataSO> itemsDePrueba = new List<ItemDataSO>(); 

    void Start()
    {
        if (inventoryController == null)
            inventoryController = GetComponent<InventoryController>(); 
        
        //Si    
        if (inventoryController != null && itemsDePrueba.Count > 0) 
        {
            //Agrega items como prueba desde el inspector
            foreach (var item in itemsDePrueba)
            {
                inventoryController.AddItem(item);
            }
        }
    }
}