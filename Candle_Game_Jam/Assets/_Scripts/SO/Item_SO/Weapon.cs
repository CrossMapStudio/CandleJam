using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Weapon", menuName = "ScriptableObjects/Item Weapon")]
public class Weapon : Item_Data
{
    private int Level = 1;
    public int GetWeaponLevel => Level;

    public override void OnDeploy()
    {

    }
    public override void OnUse()
    {

    }

    public override void UpdateUI()
    {
        UIManager.UI_Controller.Item_Description_Section.Get_ItemTitle.text = Item_Name;
        UIManager.UI_Controller.Item_Description_Section.Get_UseDescription.text = Item_UseCase;
        UIManager.UI_Controller.Item_Description_Section.Get_ItemLevel.text = "Lvl. " + GetWeaponLevel.ToString();
        UIManager.UI_Controller.Item_Description_Section.Get_ItemLoreDescription.text = Item_Description;
        UIManager.UI_Controller.Item_Description_Section.Get_ItemLargeImage.sprite = Inventory_ItemSprite;
    }
}
