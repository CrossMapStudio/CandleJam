using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "ScriptableObjects/InventoryItem")]
public class Item_Data : ScriptableObject
{
    public string ID;
    public Sprite Inventory_ItemSprite;
    public int StackCapacity;


    public GameObject SpawnObject;
    public bool Consumable;
    public float Health_Value;
    public float Food_Value;
    public float Thirst_Value;
}

