using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Weapon", menuName = "ScriptableObjects/Item Weapon")]
public class Weapon : Item_Data
{
    private int Level = 1;
    [SerializeField] private Material Weapon_Mat;
    public int GetWeaponLevel => Level;

    [SerializeField] private WeaponController Weapon_Controller;
    public WeaponController Get_WeaponController => Weapon_Controller;

    public override Item_Data CreateInstance()
    {
        Weapon Clone = new Weapon();
        Clone = this;
        return Clone;
    }

    public override void OnEquip()
    {
        //Update UI
        //Store in the Inventory Slot
        //Spawn the Weapon Controller ---
        //Awake of Weapon Controller -> Set SO
        //Connect to the player controller -> Animations ->
        if (Weapon_Controller != null)
            GameObject.Instantiate(Weapon_Controller.gameObject, PlayerController.Player_Controller.transform.position, Quaternion.identity, PlayerController.Player_Controller.transform);

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
