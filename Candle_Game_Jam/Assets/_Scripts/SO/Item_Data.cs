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
    public abstract void UpdateUI();
}

