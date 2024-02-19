using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Accessory", menuName = "ScriptableObjects/Item Accessory")]
public class Accesory : Item_Data
{
    public override Item_Data CreateInstance()
    {
        Accesory Clone = new Accesory();
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
