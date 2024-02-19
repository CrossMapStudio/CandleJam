using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Weapon", menuName = "ScriptableObjects/Item Weapon")]
public class Weapon : Item_Data
{
    private int Level = 1;
    [SerializeField] private Material Weapon_Mat;
    public int GetWeaponLevel => Level;

    //Used for the Weapon Controller ->
    public RuntimeAnimatorController RunTimeAnim_WeaponController;
    //Animation Corresponding to Keys ->

    public override Item_Data CreateInstance()
    {
        Weapon Clone = new Weapon();
        Clone = this;
        return Clone;
    }

    public override void OnEquip(int index = 0)
    {
        //Update UI
        //Store in the Inventory Slot
        //Spawn the Weapon Controller ---
        //Awake of Weapon Controller -> Set SO
        //Connect to the player controller -> Animations ->
        PlayerController.Player_Controller.Get_WeaponController[index].Set_WeaponProperties(Weapon_Mat, RunTimeAnim_WeaponController);
        UIManager.Manager.WeaponEquipSlotMain.sprite = Inventory_ItemSprite;
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
