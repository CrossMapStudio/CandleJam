using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventoryItem : MonoBehaviour
{
    public int Index;
    [SerializeField] private Sprite Empty_Sprite;
    [SerializeField] private Image Inventory_Image;
    [SerializeField] private TMPro.TMP_Text Inventory_Amount;
    private InventoryItem_Stack CurrentStack;

    public void Awake()
    {
        Index = transform.GetSiblingIndex();
    }

    public void Inventory_Update(InventoryItem_Stack Stack)
    {
        if (Stack == null)
        {
            Inventory_Amount.text = "0";
            Inventory_Image.sprite = Empty_Sprite;
            CurrentStack = null;
        }
        else
        {
            Inventory_Amount.text = Stack.currentStack.ToString();
            Inventory_Image.sprite = Stack.data.Inventory_ItemSprite;
            CurrentStack = Stack;
        }
    }

    public void Pointer_Entered()
    {
        Debug.Log("Pointer Entered" + Index);

        if (CursorController.Controller.GetCursorStateMachine.getCurrentStateName() == "Select_Cursor")
        {
            CursorController.Swap_Index = Index;
        }
        else
        {
            if (CurrentStack != null)
            {
                CurrentStack.data.UpdateUI();
            }
            else
            {
                UIManager.UI_Controller.Item_Description_Section.Get_ItemTitle.text = "Empty Inventory Slot";
                UIManager.UI_Controller.Item_Description_Section.Get_UseDescription.text = "Used for storing various items found within the dungeon.";
                UIManager.UI_Controller.Item_Description_Section.Get_ItemLevel.text = "---";
                UIManager.UI_Controller.Item_Description_Section.Get_ItemLoreDescription.text = "---";
                UIManager.UI_Controller.Item_Description_Section.Get_ItemLargeImage.sprite = Empty_Sprite;
            }
        }
    }

    public void Pointer_Exited()
    {
        Debug.Log("Pointer Exited" + Index);
        CursorController.Swap_Index = -1;
    }

    public void OnDrag_Started()
    {
        Debug.Log("Drag Started");
        CursorController.Controller.GetCursorStateMachine.changeState(new Select_Cursor(Index));
    }

    public void OnDrag_Ended()
    {
        Debug.Log("Drag Ended" + Index);
        if (CursorController.Swap_Index != -1)
        {
            //Swap the Elements within the Index ---
            GameManager.Swap_Item(CursorController.Selected_Index, CursorController.Swap_Index);
        }

        CursorController.Controller.GetCursorStateMachine.changeState(new Free_Cursor());
    }

    /*
    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        CursorController.Controller.GetCursorStateMachine.changeState(new Select_Cursor(CurrentStack));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Hover ---> We Need to Determine whether the pointer is:
        //In an Open State ->
        //In an Inventory Drag State ->
        //In a Selecting Active Item State ->
        Debug.Log("Mouse Entered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
    */
}
