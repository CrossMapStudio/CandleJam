using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Material", menuName = "ScriptableObjects/Item Material")]
public class CraftingMaterial : Item_Data
{
    public override Item_Data CreateInstance()
    {
        CraftingMaterial Clone = new CraftingMaterial();
        Clone = this;
        return Clone;
    }

    public override void OnEquip()
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