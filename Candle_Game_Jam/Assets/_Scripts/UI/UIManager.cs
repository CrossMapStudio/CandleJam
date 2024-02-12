using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager UI_Controller;
    [SerializeField] private Image UI_HungerBar, UI_ThirstBar;
    [SerializeField] private List<UIInventoryItem> Inventory_Slots;

    private void Awake()
    {
        UI_Controller = this;
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
        UI_Controller.Inventory_Slots[index].Inventory_Update(Stack);
    }
}
