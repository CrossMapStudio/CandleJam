using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item Weapon", menuName = "ScriptableObjects/Item Weapon")]
public class Weapon : Item_Data
{
    private int Level = 1;
    [SerializeField] private Material Weapon_Mat;
    public Material Get_WeaponMaterial => Weapon_Mat;
    public int GetWeaponLevel => Level;
    //Used for the Weapon Controller ->
    public AnimatorOverrideController Animation_Override;

    public override Item_Data CreateInstance()
    {
        Weapon Clone = new Weapon();
        Clone = this;
        return Clone;
    }

    public override void OnEquip(int UI_Index = 0, int List_Index = 0)
    {
        WeaponController.Controller.Set_WeaponProperties(0, this);
        UIManager.Update_Weapon(0, GameManager.Manager.Weapon_Inventory.GetInventory_Data[List_Index]);
        return;
    }

    public override void OnUse()
    {

    }

    public override void UpdateUI()
    {
        UIManager.Manager.Item_Description_Section.Get_ItemTitle.text = Item_Name;
        UIManager.Manager.Item_Description_Section.Get_UseDescription.text = Item_UseCase;
        UIManager.Manager.Item_Description_Section.Get_ItemLevel.text = "Lvl. " + GetWeaponLevel.ToString();
        UIManager.Manager.Item_Description_Section.Get_ItemLoreDescription.text = Item_Description;
        UIManager.Manager.Item_Description_Section.Get_ItemLargeImage.sprite = Inventory_ItemSprite;
        UIManager.Manager.Item_Description_Section.Get_ItemLargeImage.material = Weapon_Mat;
    }
}
