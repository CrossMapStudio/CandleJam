using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Inventory", menuName = "ScriptableObjects/Player Inventory")]
public class Player_Inventory : ScriptableObject
{
    private InventoryItem_Stack[] Inventory_Data = new InventoryItem_Stack[50];
    public InventoryItem_Stack[] GetInventory_Data => Inventory_Data;
    public bool Add_Item(Item_Data data)
    {
        int emptySlot = -1;
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
    }
    public void Drop_Item(int inventorySlot)
    {

    }
    public void Drop_AllItem(int inventorySlot)
    {

    }
}
