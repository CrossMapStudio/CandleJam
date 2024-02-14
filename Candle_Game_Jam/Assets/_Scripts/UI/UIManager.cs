using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class UIManager : MonoBehaviour
{
    private static UIManager UI_Controller;
    [SerializeField] private Image UI_HungerBar, UI_ThirstBar;

    [Header("Menus")]
    [SerializeField] private GameObject Inventory;
    [SerializeField] private GameObject Pause;

    [Header("Section 1 Toggle")]
    [SerializeField] private GameObject Eq_Menu, Inv_Menu;

    [SerializeField] private Transform Inventory_Menu_Container;
    private List<UIInventoryItem> UI_Inventory;

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
                CameraController.Controller.ChangeCameraTargets(1f, new Vector3(1f, 0, 0));
                Change_Inventory(0);
            }
            else
                CameraController.Controller.ResetCameraTargets();
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
}
