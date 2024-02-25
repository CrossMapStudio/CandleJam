using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Inventory", menuName = "ScriptableObjects/Player Inventory")]
public class Inventory_Collection : ScriptableObject
{
    private Dictionary<System.Guid, InventoryItem_Stack> Inventory_Dictionary;
    public Dictionary<System.Guid, InventoryItem_Stack> Get_InventoryDictionary => Inventory_Dictionary;


    private System.Guid[] Inventory_DataKeys = new System.Guid[50];
    public System.Guid[] GetInventory_Data => Inventory_DataKeys;

    private void OnEnable()
    {
        Inventory_Dictionary = new Dictionary<System.Guid, InventoryItem_Stack>();
    }

    public bool Add_Item(Item_Data data)
    {
        int SlotTracker = 0;
        int emptySlot = -1;

        foreach(System.Guid element in Inventory_DataKeys)
        {
            if (Inventory_DataKeys[SlotTracker] == System.Guid.Empty)
            {
                if (emptySlot == -1)
                    emptySlot = SlotTracker;

                SlotTracker++;
                continue;
            }

            if (Get_InventoryDictionary[element].data == data)
            {
                if (Get_InventoryDictionary[element].currentStack < data.StackCapacity)
                {
                    Get_InventoryDictionary[element].currentStack++;
                    UIManager.Update_Inventory(Get_InventoryDictionary[element].Inventory_SlotLocation);
                    return true;
                }
            }

            SlotTracker++;
        }

        if (emptySlot != -1)
        {
            var Stack = new InventoryItem_Stack(1, data, emptySlot);
            Inventory_Dictionary.Add(Stack.GetID, Stack);
            Inventory_DataKeys[emptySlot] = Stack.GetID;

            UIManager.Update_Inventory(Stack.Inventory_SlotLocation);
            return true;
        }

        //Inventory Full ---
        return false;


        /*

        for (int i = 0; i < Inventory_Data.Length; i++)
        {
            if (Inventory_Data[i] == null)
            {
                if (emptySlot == -1)
                    emptySlot = i;
                continue;
            }

            //Check if there is matching IDs --- If the Current Stack Amount of the Item is Less than the capacity, add to the number, else find another slot --- no slots available dont add the item return false ---
            if (Inventory_Data[i].data.GetID == data.GetID)
            {
                if (Inventory_Data[i].currentStack < Inventory_Data[i].data.StackCapacity)
                {
                    Inventory_Data[i].currentStack++;
                    UIManager.Update_Inventory(i, Inventory_Data[i]);
                    return true;
                }
                else
                    continue;
            }
        }

        if (emptySlot != -1)
        {
            Inventory_Data[emptySlot] = new InventoryItem_Stack(1, data);
            UIManager.Update_Inventory(emptySlot, Inventory_Data[emptySlot]);
            return true;
        }

        return false;
        */
    }
    public void Drop_Item(int inventorySlot)
    {

    }
    public void Drop_AllItem(int inventorySlot)
    {

    }
    public InventoryItem_Stack Get_Item(System.Guid ID)
    {
        return Inventory_Dictionary[ID];
    }
}
