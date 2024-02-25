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
        Damage.text = "+" + _stats.Damage.ToString();
        Stamina.text = _stats.Stamina.ToString();
        Arcane.text = _stats.Arcane.ToString();
        Speed.text = _stats.Speed.ToString();
        Critical.text ="+"+_stats.Critical_Damage.ToString();
        Critical_Percentage.text = _stats.Critical_Percentage.ToString() + "%";

        H_Damage.text = "+" + _stats.H_Damage.ToString();
        H_Stamina.text = _stats.H_Stamina.ToString();
        H_Arcane.text = _stats.H_Arcane.ToString();
        H_Speed.text = _stats.H_Speed.ToString();
        H_Critical.text = "+" + _stats.H_Critical_Damage.ToString();
        H_Critical_Percentage.text = _stats.H_Critical_Percentage.ToString() + "%";
    }
}
