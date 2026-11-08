using UnityEngine;
[CreateAssetMenu(fileName = "New_Item", menuName = "Scriptable Objects/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public string itemName = "Objeto";
    public string itemDescription = "Descripción";
    public Sprite itemIcon; 
}