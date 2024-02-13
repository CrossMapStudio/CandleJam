using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item_Data : ScriptableObject
{
    //All Items Need ID's for Sorting
    [SerializeField] private string ID;
    public string GetID => ID;
    
    //All Items Need Inventory Sprite
    public Sprite Inventory_ItemSprite;

    //All Items Need Stack Capacity
    public int StackCapacity;

    //All Items Need Spawn Object for "Dropping"
    public GameObject SpawnObject;

    //All Items need a Title
    public string Item_Name;

    //All Items Need Use Case
    [TextArea(2,3)]
    public string Item_UseCase;

    //All Items Need a Lore Description
    [TextArea(5, 8)]
    public string Item_Description;

    public abstract void OnDeploy();
    public abstract void OnUse();
}

[CreateAssetMenu(fileName = "Item Weapon", menuName = "ScriptableObjects/Item Weapon")]
public class Weapon : Item_Data
{
    public override void OnDeploy()
    {

    }
    public override void OnUse()
    {

    }
}

[CreateAssetMenu(fileName = "Item Armor", menuName = "ScriptableObjects/Item Armor")]
public class Armor : Item_Data
{
    public override void OnDeploy()
    {

    }
    public override void OnUse()
    {

    }
}

[CreateAssetMenu(fileName = "Item Accessory", menuName = "ScriptableObjects/Item Accessory")]
public class Accesory : Item_Data
{
    public override void OnDeploy()
    {

    }
    public override void OnUse()
    {

    }
}

[CreateAssetMenu(fileName = "Item Consumable", menuName = "ScriptableObjects/Item Consumable")]
public class Consumable : Item_Data
{
    //Values to be used in Inventory Use Selection
    public override void OnDeploy()
    {

    }
    public override void OnUse()
    {

    }
}

[CreateAssetMenu(fileName = "Item Material", menuName = "ScriptableObjects/Item Material")]
public class Crafting_Material : Item_Data
{
    public override void OnDeploy()
    {

    }
    public override void OnUse()
    {

    }
}

