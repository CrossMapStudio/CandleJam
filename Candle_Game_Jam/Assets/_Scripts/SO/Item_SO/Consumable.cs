using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Consumable", menuName = "ScriptableObjects/Item Consumable")]
public class Consumable : Item_Data
{
    public override Item_Data CreateInstance()
    {
        Consumable Clone = new Consumable();
        Clone = this;
        return Clone;
    }

    //Values to be used in Inventory Use Selection
    public override void OnEquip(int index = 0)
    {

    }
    public override void OnUse()
    {

    }

    public override void UpdateUI()
    {
        UIManager.Manager.Item_Description_Section.Get_ItemTitle.text = Item_Name;
        UIManager.Manager.Item_Description_Section.Get_UseDescription.text = Item_UseCase;
        UIManager.Manager.Item_Description_Section.Get_ItemLevel.text = "---";
        UIManager.Manager.Item_Description_Section.Get_ItemLoreDescription.text = Item_Description;
        UIManager.Manager.Item_Description_Section.Get_ItemLargeImage.sprite = Inventory_ItemSprite;
    }
}
