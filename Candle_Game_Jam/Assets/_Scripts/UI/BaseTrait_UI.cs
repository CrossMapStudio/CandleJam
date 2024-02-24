using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BaseTrait_UI : MonoBehaviour
{
    [SerializeField] private TMP_Text Damage, Stamina, Arcane, Speed, Critical, Critical_Percentage;
    [SerializeField] private TMP_Text H_Damage, H_Stamina, H_Arcane, H_Speed, H_Critical, H_Critical_Percentage;

    public void Update_BaseTraitUI(Weapon_BaseStats _stats)
    {
        Damage.text = "+" + _stats.Damage.ToString() + " Dmg";
        Stamina.text = _stats.Stamina.ToString() + " Sta";
        Arcane.text = _stats.Arcane.ToString() + " Arc";
        Speed.text = _stats.Speed.ToString() + " Spe";
        Critical.text ="+"+_stats.Critical_Damage.ToString() + " Dmg";
        Critical_Percentage.text = _stats.Critical_Percentage.ToString() + "%";

        H_Damage.text = "+" + _stats.H_Damage.ToString() + " Dmg";
        H_Stamina.text = _stats.H_Stamina.ToString() + " Sta";
        H_Arcane.text = _stats.H_Arcane.ToString() + " Arc";
        H_Speed.text = _stats.H_Speed.ToString() + " Spe";
        H_Critical.text = "+" + _stats.H_Critical_Damage.ToString() + " Dmg";
        H_Critical_Percentage.text = _stats.H_Critical_Percentage.ToString() + "%";
    }
}
