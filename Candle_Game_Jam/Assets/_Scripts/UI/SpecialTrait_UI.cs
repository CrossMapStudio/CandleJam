using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpecialTrait_UI : MonoBehaviour
{
    //Name - Percentage - Level - Curse Percentage - Curse Stack
    [SerializeField] private TMP_Text Trait_Name, Trait_Percentage, Trait_Level, Curse_Percentage, Curse_Level;

    public void Update_SpecialTraitUI(Weapon_SpecialTrait _Trait)
    {
        Trait_Name.text = _Trait.Trait_Type.ToString() + ":";
        Trait_Percentage.text = _Trait.Percentage.ToString() + "%";
        Trait_Level.text = "Lvl: " + _Trait.Level.ToString();
        Curse_Percentage.text = _Trait.CursePercentage.ToString() + "%";
        Curse_Level.text = "Lvl: " + _Trait.CurseLevel.ToString();
    }
}
