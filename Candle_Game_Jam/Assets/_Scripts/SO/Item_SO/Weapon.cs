using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item Weapon", menuName = "ScriptableObjects/Item Weapon")]
public class Weapon : Item_Data
{
    [SerializeField] private Material Weapon_Mat;
    public Material Get_WeaponMaterial => Weapon_Mat;

    private int Level = 1;
    public int GetWeaponLevel => Level;
    //Used for the Weapon Controller ->
    public AnimatorOverrideController Animation_Override;
    public StatisticGroup Stat_Group;

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

        UIManager.Manager.Item_Statistics_Section.UpdateSpecialTraits(Stat_Group.GetSpecialTraits);
        UIManager.Manager.Item_Statistics_Section.UpdateBaseTraits(Stat_Group.Get_BaseStats);
    }
}

[Serializable]
public class StatisticGroup
{
    public enum W_SpecialTrait
    {
        Burning,
        Corrosion,
        Plague,
        Bleed,
        Faith,
        Arcane,
        Luck,
        Leach,
        Curse,
        Illusion
    }

    public enum W_Requirement
    {
        Strength,
        Stamina,
        Arcane,
        Agility,
        Faith,
        SingleWeapon,
        DuelWeapon,
        TwoHandedWeapon,
        DefensiveWeapon
    }

    [SerializeField] private Weapon_BaseStats Stats;
    public Weapon_BaseStats Get_BaseStats => Stats;
    
    //Special Trait Values ---
    [SerializeField] private List<Weapon_SpecialTrait> Traits;
    public List<Weapon_SpecialTrait> GetSpecialTraits => Traits;


    [SerializeField] private List<Weapon_Requirements> Requirements;
    public List<Weapon_Requirements> Get_Requirements => Requirements;
}

[Serializable]
public class Weapon_BaseStats
{
    [Range(0f, 100f)]
    public float Damage, Stamina, Arcane, Speed, Critical_Damage, Critical_Percentage;

    [Range(0f, 100f)]
    public float H_Damage, H_Stamina, H_Arcane, H_Speed, H_Critical_Damage, H_Critical_Percentage;
}

[Serializable]
public class Weapon_SpecialTrait
{
    public StatisticGroup.W_SpecialTrait Trait_Type;
    
    [Range(0f, 100f)]
    public float Percentage;

    [Range(0f, 100f)]
    public float Level;

    [Range(0f, 100f)]
    public float CursePercentage;

    [Range(0f, 100f)]
    public float CurseLevel;
}

[Serializable]
public class Weapon_Requirements
{
    public StatisticGroup.W_Requirement Stat_Type;
    [Range(0, 100)]
    public int Requirement_Level;
}
