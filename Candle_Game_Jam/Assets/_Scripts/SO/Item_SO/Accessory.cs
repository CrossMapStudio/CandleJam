using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Accessory", menuName = "ScriptableObjects/Item Accessory")]
public class Accesory : Item_Data
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
