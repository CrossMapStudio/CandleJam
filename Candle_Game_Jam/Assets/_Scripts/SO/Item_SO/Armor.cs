using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Armor", menuName = "ScriptableObjects/Item Armor")]
public class Armor : Item_Data
{
    public override Item_Data CreateInstance()
    {
        Armor Clone = new Armor();
        Clone = this;
        return Clone;
    }

    public override void OnEquip(int index = 0)
    {

    }
    public override void OnUse()
    {

    }

    public override void UpdateUI()
    {
        throw new System.NotImplementedException();
    }
}