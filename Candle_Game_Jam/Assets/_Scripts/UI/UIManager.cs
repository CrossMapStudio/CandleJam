using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.InputSystem;
public class UIManager : MonoBehaviour
{
    public static UIManager Manager;

    //State Machine ---
    private StateMachine UI_StateMachine;
    public StateMachine Get_UIMachine => UI_StateMachine;
    //Free --- //SelectingWeapons

    [SerializeField] private Image UI_HungerBar, UI_ThirstBar;

    [Header("Menus")]
    [SerializeField] private GameObject Inventory;
    [SerializeField] private GameObject Pause;

    [Header("Navigation Collection")]
    [SerializeField] private Menu_Navigator MenuNavigator;
    public Menu_Navigator Get_MenuNavigator => MenuNavigator;

    [SerializeField] private Inventory_Tabs InventoryTabs;
    public Inventory_Tabs Get_InventoryTabs => InventoryTabs;

    [Header("Section 1 Toggle")]
    [SerializeField] private GameObject Eq_Menu, Inv_Menu;

    //Current Menu is used to Save the Menu we are Currently Using
    [SerializeField] private Transform Inventory_Menu_Container;

    private List<UIInventoryItem> UI_Inventory;
    public List<UIInventoryItem> Get_UIInventory => UI_Inventory;

    //Weapon Slots
    [SerializeField] private List<UIInventoryItem> UI_Weapons;

    //Allows us to pass the class ->
    public Section_ItemDescription Item_Description_Section;
    public Section_ItemStatistics Item_Statistics_Section;

    //Popup
    #region Pop-Up UI
    [SerializeField] private Combat_AnimationCaller CenterText_Container;
    [SerializeField] private TMP_Text Center_Text;
    public Action CenterTextCallBack;
    #endregion

    //Top Message on Pickup
    #region Pickup Notification
    public TopMessage_Channel PickupChannel;
    private Queue<Item_Data> TopMessageQueue;
    [SerializeField] private TopMessage_UI TopMessage;
    #endregion

    private void Awake()
    {
        Manager = this;
        UI_Inventory = new List<UIInventoryItem>();

        UI_StateMachine = new StateMachine();
        UI_StateMachine.changeState(new UI_Free());
        //Free

        Get_MenuNavigator.SetTabs();
        Get_InventoryTabs.SetTabs();

        //Populate the List of UI Slots in the Inventory ---
        for (int i = 0; i < 50; i++)
        {
            UI_Inventory.Add(Inventory_Menu_Container.GetChild(i).GetComponent<UIInventoryItem>());
        }

        TopMessageQueue = new Queue<Item_Data>();
        PickupChannel.OnEventRaised.AddListener(QueueTopMessage);
    }

    private void OnEnable()
    {
        Input_Driver.Get_Inventory.performed += ToggleUI;
    }

    public void QueueTopMessage(Item_Data _data)
    {
        TopMessageQueue.Enqueue(_data);
        if (TopMessageQueue.Count == 1)
        {
            StartCoroutine(ShowTopMessage());
        }
    }

    public IEnumerator ShowTopMessage()
    {
        //Show UI Element ---
        TopMessage.gameObject.SetActive(true);
        var Top = TopMessageQueue.Dequeue();
        TopMessage.ShowTopMessage(Top.Inventory_ItemSprite, Top.Get_Material, Top.Item_Name);
        yield return new WaitForSecondsRealtime(3f);
        TopMessage.gameObject.SetActive(false);
        if (TopMessageQueue.Count != 0)
        {
            StartCoroutine(ShowTopMessage());
        }
    }

    public void ToggleUI(InputAction.CallbackContext obj)
    {
        Inventory.SetActive(!Inventory.activeSelf);
        if (Inventory.activeSelf)
        {
            CameraController.Controller.ChangeCamera_Player(1f, new Vector3(1f, 0, 0));
            Change_Inventory(0);
            CursorController.Controller.GetCursorStateMachine.changeState(new Free_Cursor());
            UI_StateMachine.changeState(new UI_Free());
            PlayerController.Player_Controller.Get_PlayerStateMachine.changeState(new Player_Hold());
        }
        else
        {
            CameraController.Controller.ResetCamera_Player();
            CursorController.Controller.GetCursorStateMachine.changeState(new Locked_Cursor());
            PlayerController.Player_Controller.Get_PlayerStateMachine.changeState(new Player_Movement());
        }
    }

    public static void SetUIHungerBar(float currentValue)
    {
        Manager.UI_HungerBar.fillAmount = currentValue / 100f;
    }

    public static void SetUIThirstBar(float currentValue)
    {
        Manager.UI_ThirstBar.fillAmount = currentValue / 100f;
    }

    public static void Update_Inventory(int index)
    {
        Manager.UI_Inventory[index].Inventory_Update(GameManager.Manager.Get_Inventory_Collection[GameManager.Manager.Current_Menu].GetInventory_Data[index]);
    }

    public static void Update_Weapon(int index, Guid ID)
    {
        Manager.UI_Weapons[index].Inventory_Update(ID);
    }

    public void Change_Inventory(int index)
    {
        var Collection = GameManager.Manager.Get_Inventory_Collection[index];
        GameManager.Manager.Current_Menu = index;

        for (int i = 0; i < Collection.GetInventory_Data.Length; i++)
        {
            Manager.UI_Inventory[i].Inventory_Update(Collection.GetInventory_Data[i]);
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

    public void Equip_Weapon(int index)
    {
        Manager.UI_StateMachine.changeState(new UI_SelectingWeapons());
    }

    //Used for Regions - Achievements, Etc. --- Break into a seperate state ---
    #region Page Display
    public void StartCenterText(string _text, Action _CenterTextCallBack)
    {
        Center_Text.text = _text;
        CenterText_Container.gameObject.SetActive(true);
        //CenterText_Container.Get_SetTrigger = EndCenterText;
        CenterTextCallBack = _CenterTextCallBack;
    }

    public void EndCenterText()
    {
        CenterText_Container.gameObject.SetActive(false);
        CenterTextCallBack();
    }
    #endregion

    /*
    public void Equip_Weapon()
    {
        //Adding Button Listeners for Call Back ---
        foreach (UIInventoryItem element in UI_Inventory)
        {
            element.GetComponent<Button>().onClick.AddListener(() => { GameManager.Equip_Weapon(GameManager.Manager.Weapon_Inventory.GetInventory_Data[element.Index].data); });
        }
    }
    */
}

[System.Serializable]
public class Inventory_Tabs
{
    [SerializeField] private Button Weapon_Tab, Armor_Tab, Accessory_Tab, Usable_Tab, Material_Tab;
    Button[] Tabs;
    public void SetTabs()
    {
        Tabs = new Button[] { Weapon_Tab, Armor_Tab, Accessory_Tab, Usable_Tab, Material_Tab };
    }


    public void enableAll()
    {
        for(int i = 0; i < Tabs.Length; i++)
        {
            Tabs[i].interactable = true;
        }
    }

    public void disableAll()
    {
        for (int i = 0; i < Tabs.Length; i++)
        {
            Tabs[i].interactable = false;
        }
    }
}

[System.Serializable]
public class Menu_Navigator
{
    [SerializeField] private Button Equipment_Tab, Inventory_Tab, Stat_Tab;
    Button[] Navigators;
    public void SetTabs()
    {
        Navigators = new Button[] { Equipment_Tab, Inventory_Tab, Stat_Tab };
    }


    public void enableAll()
    {
        for (int i = 0; i < Navigators.Length; i++)
        {
            Navigators[i].interactable = true;
        }
    }

    public void disableAll()
    {
        for (int i = 0; i < Navigators.Length; i++)
        {
            Navigators[i].interactable = false;
        }
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

[Serializable]
public class Section_ItemStatistics
{
    [SerializeField] private BaseTrait_UI BaseStats;
    public BaseTrait_UI Get_BaseStats => BaseStats;

    [SerializeField] private List<SpecialTrait_UI> SpecialTraits;
    public List<SpecialTrait_UI> Get_SpecialTraits => SpecialTraits;

    public void UpdateBaseTraits(Weapon_BaseStats _stats)
    {
        Get_BaseStats.Update_BaseTraitUI(_stats);
    }

    public void UpdateSpecialTraits(List<Weapon_SpecialTrait> _TraitList)
    {
        for (int i = 0; i < Get_SpecialTraits.Count; i++)
        {
            Get_SpecialTraits[i].Update_SpecialTraitUI(_TraitList[i]);
        }
    }

    [SerializeField] private List<TMP_Text> RequiredStats;
    public List<TMP_Text> Get_RequiredStats => RequiredStats;
}

#region UI Manager States
public class UI_Free : stateDriverInterface
{
    public string ID => "UI Free";

    public void onEnter()
    {
        Debug.Log("Entered Free State");
    }

    public void onExit()
    {
        Debug.Log("Exited Free State");
    }

    public void onFixedUpdate()
    {
        throw new NotImplementedException();
    }

    public void onGUI()
    {
        throw new NotImplementedException();
    }

    public void onLateUpdate()
    {
        throw new NotImplementedException();
    }

    public void onUpdate()
    {
        throw new NotImplementedException();
    }
}
public class UI_SelectingWeapons : stateDriverInterface
{
    public string ID => "UI Weapon Select";

    public void onEnter()
    {
        UIManager.Manager.Get_InventoryTabs.disableAll();
        UIManager.Manager.Get_MenuNavigator.disableAll();
        UIManager.Manager.Section_Switch(1);
        UIManager.Manager.Change_Inventory(0);

        foreach (UIInventoryItem element in UIManager.Manager.Get_UIInventory)
        {
            EventTrigger.Entry OnButtonEquip = new EventTrigger.Entry();
            OnButtonEquip.eventID = EventTriggerType.PointerClick;

            if (GameManager.Manager.Weapon_Inventory.GetInventory_Data[element.Index] != Guid.Empty)
                OnButtonEquip.callback.AddListener((CallBack) => { GameManager.Equip_Weapon(GameManager.Manager.Weapon_Inventory.Get_InventoryDictionary[GameManager.Manager.Weapon_Inventory.GetInventory_Data[element.Index]], 0); });
            else
                OnButtonEquip.callback.AddListener((CallBack) => { GameManager.Equip_Weapon(null, 0); });

            element.Get_Trigger.triggers.Add(OnButtonEquip);
        }
    }

    public void onExit()
    {
        UIManager.Manager.Get_InventoryTabs.enableAll();
        UIManager.Manager.Get_MenuNavigator.enableAll();
        UIManager.Manager.Section_Switch(0);

        foreach (UIInventoryItem element in UIManager.Manager.Get_UIInventory)
        {
            element.Get_Trigger.triggers.RemoveAt(element.Get_Trigger.triggers.Count - 1);
        }
    }

    public void onFixedUpdate()
    {
        throw new NotImplementedException();
    }

    public void onGUI()
    {
        throw new NotImplementedException();
    }

    public void onLateUpdate()
    {
        throw new NotImplementedException();
    }

    public void onUpdate()
    {
        throw new NotImplementedException();
    }
}
#endregion
