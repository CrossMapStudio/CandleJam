using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Item_Data : ScriptableObject
{
    private Guid ID;
    public Guid GetID => ID;

    //All Items Need Inventory Sprite
    public Sprite Inventory_ItemSprite;

    [SerializeField] private Material SpriteMaterial;
    public Material Get_Material => SpriteMaterial;

    //All Items Need Stack Capacity
    public int StackCapacity;

    //All Items Need Spawn Object for "Dropping"
    public GameObject DropItem;

    //All Items need a Title
    public string Item_Name;

    //All Items Need Use Case
    [TextArea(2,3)]
    public string Item_UseCase;

    //All Items Need a Lore Description
    [TextArea(5, 8)]
    public string Item_Description;

    public void OnEnable()
    {
        ID = Guid.NewGuid();
    }

    public abstract void OnEquip(Guid ID);
    public abstract void OnUse();
    public abstract void UpdateUI();
}

