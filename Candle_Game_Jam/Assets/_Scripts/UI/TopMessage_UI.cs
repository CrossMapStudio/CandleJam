using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopMessage_UI : MonoBehaviour
{
    [SerializeField] private Image Icon;
    [SerializeField] private TMP_Text Item;

    public void ShowTopMessage(Sprite _Icon, Material _IconMat, string _Item)
    {
        //Play Anim ---
        Icon.sprite = _Icon;
        Icon.material = _IconMat;
        Item.text = "Collected: " + _Item;
    }
}
