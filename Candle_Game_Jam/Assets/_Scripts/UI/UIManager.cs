using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class UIManager : MonoBehaviour
{
    public static UIManager UI_Controller;
    [SerializeField] private Image UI_HungerBar, UI_ThirstBar;

    [Header("Menus")]
    [SerializeField] private GameObject Inventory;
    [SerializeField] private GameObject Pause;

    [Header("Section 1 Toggle")]
    [SerializeField] private GameObject Eq_Menu, Inv_Menu;

    //Current Menu is used to Save the Menu we are Currently Using
    [SerializeField] private Transform Inventory_Menu_Container;
    private List<UIInventoryItem> UI_Inventory;

    //Allows us to pass the class ->
    public Section_ItemDescription Item_Description_Section;

    //Popup
    #region Pop-Up UI
    [SerializeField] private UIManagerAnimationTriggerCall CenterText_Container;
    [SerializeField] private TMP_Text Center_Text;
    public Action CenterTextCallBack;
    #endregion

    private void Awake()
    {
        UI_Controller = this;
        UI_Inventory = new List<UIInventoryItem>();

        //Populate the List of UI Slots in the Inventory ---
        for (int i = 0; i < 50; i++)
        {
            UI_Inventory.Add(Inventory_Menu_Container.GetChild(i).GetComponent<UIInventoryItem>());
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Inventory.SetActive(!Inventory.activeSelf);

            if (Inventory.activeSelf)
            {
                CameraController.Controller.ChangeCamera_Player(1f, new Vector3(1f, 0, 0));
                Change_Inventory(0);
                CursorController.Controller.GetCursorStateMachine.changeState(new Free_Cursor());
            }
            else
            {
                CameraController.Controller.ResetCamera_Player();
                CursorController.Controller.GetCursorStateMachine.changeState(new Locked_Cursor());
            }   
        }
    }

    public static void SetUIHungerBar(float currentValue)
    {
        UI_Controller.UI_HungerBar.fillAmount = currentValue / 100f;
    }

    public static void SetUIThirstBar(float currentValue)
    {
        UI_Controller.UI_ThirstBar.fillAmount = currentValue / 100f;
    }

    public static void Update_Inventory(int index, InventoryItem_Stack Stack)
    {
        UI_Controller.UI_Inventory[index].Inventory_Update(Stack);
    }

    public void Change_Inventory(int index)
    {
        var Collection = GameManager.Manager.Get_Inventory_Collection[index];
        GameManager.Manager.Current_Menu = index;

        for (int i = 0; i < Collection.GetInventory_Data.Length; i++)
        {
            UI_Controller.UI_Inventory[i].Inventory_Update(Collection.GetInventory_Data[i]);
        }
    }

    public void Section_Switch(int Menu_ToggleValue)
    {
        switch (Menu_ToggleValue)
        {
            case 0:
                Eq_Menu.SetActive(true);
                Inv_Menu.SetActive(false);
                break;
            case 1:
                Eq_Menu.SetActive(false);
                Inv_Menu.SetActive(true);
                break;
            default:
                return;
        }
    }


    //Used for Regions - Achievements, Etc.
    public void StartCenterText(string _text, Action _CenterTextCallBack)
    {
        Center_Text.text = _text;
        CenterText_Container.gameObject.SetActive(true);
        CenterText_Container.Get_SetTrigger = EndCenterText;
        CenterTextCallBack = _CenterTextCallBack;
    }

    public void EndCenterText()
    {
        CenterText_Container.gameObject.SetActive(false);
        CenterTextCallBack();
    }
}

[System.Serializable]
public class Section_ItemDescription
{
    //Section 2 --- Eventually Need the Crafted Weapon Slot Upgrades ---
    [Header("Section 2 - UI Elements")]
    [SerializeField] private TMP_Text Item_Title;
    public TMP_Text Get_ItemTitle => Item_Title;


    [SerializeField] private TMP_Text Item_Level;
    public TMP_Text Get_ItemLevel => Item_Level;


    [SerializeField] private TMP_Text Item_UseDescription;
    public TMP_Text Get_UseDescription => Item_UseDescription;


    [SerializeField] private TMP_Text Item_LoreDescription;
    public TMP_Text Get_ItemLoreDescription => Item_LoreDescription;


    [SerializeField] private Image Item_LargeImage;
    public Image Get_ItemLargeImage => Item_LargeImage;
}
