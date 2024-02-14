using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    private int Index;
    [SerializeField] private Sprite Empty_Sprite;
    [SerializeField] private Image Inventory_Image;
    [SerializeField] private TMPro.TMP_Text Inventory_Amount;

    public void Awake()
    {
        Index = transform.parent.GetSiblingIndex();
    }

    public void Inventory_Update(InventoryItem_Stack Stack)
    {
        if (Stack == null)
        {
            Inventory_Amount.text = "0";
            Inventory_Image.sprite = Empty_Sprite;
        }
        else
        {
            Inventory_Amount.text = Stack.currentStack.ToString();
            Inventory_Image.sprite = Stack.data.Inventory_ItemSprite;
        }
    }
}
