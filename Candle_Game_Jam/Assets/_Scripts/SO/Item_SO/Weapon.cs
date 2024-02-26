using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item Weapon", menuName = "ScriptableObjects/Item Weapon")]
public class Weapon : Item_Data
{
    //Used for the Weapon Controller ->
    public WeaponType Weapon_Type;

    public override void OnEquip(Guid ID)
    {
        WeaponController.Controller.Set_WeaponProperties(0, this);
        UIManager.Update_Weapon(0, ID);
        return;
    }

    public override void OnUse()
    {

    }

    public override void UpdateUI()
    {
        UIManager.Manager.Item_Description_Section.Get_ItemTitle.text = Item_Name;
        UIManager.Manager.Item_Description_Section.Get_UseDescription.text = Item_UseCase;
        UIManager.Manager.Item_Description_Section.Get_ItemLevel.text = "Lvl. 1";
        UIManager.Manager.Item_Description_Section.Get_ItemLoreDescription.text = Item_Description;
        UIManager.Manager.Item_Description_Section.Get_ItemLargeImage.sprite = Inventory_ItemSprite;
        UIManager.Manager.Item_Description_Section.Get_ItemLargeImage.material = Get_Material;

        UIManager.Manager.Item_Statistics_Section.UpdateSpecialTraits(Weapon_Type.Stat_Group.GetSpecialTraits);
        UIManager.Manager.Item_Statistics_Section.UpdateBaseTraits(Weapon_Type.Stat_Group.Get_BaseStats);
    }
}
