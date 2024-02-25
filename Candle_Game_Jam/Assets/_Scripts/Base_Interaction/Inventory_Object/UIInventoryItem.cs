using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventoryItem : MonoBehaviour
{
    public int Index;
    [SerializeField] private Sprite Empty_Sprite;
    private Material Sprite_DefaultMaterial;
    [SerializeField] private Image Inventory_Image;
    [SerializeField] private TMPro.TMP_Text Inventory_Amount;
    private InventoryItem_Stack CurrentStack;

    //Used for the Button on Different Behaviors ---
    private EventTrigger Trigger;
    public EventTrigger Get_Trigger => Trigger;

    public void Awake()
    {
        Index = transform.GetSiblingIndex();
        Trigger = GetComponent<EventTrigger>();
        Sprite_DefaultMaterial = Inventory_Image.material;
    }

    public void Inventory_Update(System.Guid ID)
    {
        if (ID == System.Guid.Empty)
        {
            if (Inventory_Amount)
                Inventory_Amount.text = "0";
            Inventory_Image.sprite = Empty_Sprite;
            Inventory_Image.material = Sprite_DefaultMaterial;
            CurrentStack = null;
        }
        else
        {
            var _Stack = GameManager.Manager.Get_Inventory_Collection[GameManager.Manager.Current_Menu].Get_InventoryDictionary[ID];

            if (Inventory_Amount)
                Inventory_Amount.text = _Stack.currentStack.ToString();
            Inventory_Image.sprite = _Stack.data.Inventory_ItemSprite;
            Inventory_Image.material = _Stack.data.Get_Material;
            CurrentStack = _Stack;
        }
    }

    public void Pointer_Entered()
    {
        if (CursorController.Controller.GetCursorStateMachine.getCurrentStateName() == "Select_Cursor")
        {
            if (CurrentStack != null)
                CursorController.Swap_ID = CurrentStack.GetID;
        }
        else
        {
            if (CurrentStack != null)
            {
                CurrentStack.data.UpdateUI();
                Debug.Log("On Hover: " + CurrentStack.GetID);
            }
            else
            {
                UIManager.Manager.Item_Description_Section.Get_ItemTitle.text = "Empty Inventory Slot";
                UIManager.Manager.Item_Description_Section.Get_UseDescription.text = "Used for storing various items found within the dungeon.";
                UIManager.Manager.Item_Description_Section.Get_ItemLevel.text = "---";
                UIManager.Manager.Item_Description_Section.Get_ItemLoreDescription.text = "---";
                UIManager.Manager.Item_Description_Section.Get_ItemLargeImage.sprite = Empty_Sprite;
            }
        }
    }

    public void Pointer_Exited()
    {
        CursorController.Swap_ID = System.Guid.Empty;
    }

    public void OnDrag_Started()
    {
        CursorController.Controller.GetCursorStateMachine.changeState(new Select_Cursor(CurrentStack.GetID));
    }

    public void OnDrag_Ended()
    {
        if (CursorController.Swap_ID != System.Guid.Empty)
        {
            //Swap the Elements within the Index ---
            GameManager.Swap_Item(CursorController.Selected_ID, CursorController.Swap_ID);
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
