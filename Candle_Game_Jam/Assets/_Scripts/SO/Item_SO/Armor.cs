using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Armor", menuName = "ScriptableObjects/Item Armor")]
public class Armor : Item_Data
{
    public override void OnEquip(Guid ID)
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