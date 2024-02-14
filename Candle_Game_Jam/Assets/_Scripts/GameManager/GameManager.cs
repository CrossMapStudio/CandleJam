using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // --- Game Over State
    // --- Game Win State
    // --- Game Pause State
    // --- Item Spawn

    public static GameManager Manager;
    public static List<StatisticManager> Statistic_Managers;

    public float HungerTick_Target;
    public float ThirstTick_Target;
    public float DarknessTick_Target;
    public float HealthTick_Target;

    private float HungerTick_Current;
    private float ThirstTick_Current;
    private float DarknessTick_Current;
    private float HealthTick_Current;

    #region Inventory Lists
    //Weapons, Armor, Accessories, Materials --- Eventually
    private List<Inventory_Collection> Inventory_Collections;
    public List<Inventory_Collection> Get_Inventory_Collection => Inventory_Collections;

    public Inventory_Collection Weapon_Inventory;
    public Inventory_Collection Armor_Inventory;
    public Inventory_Collection Accessory_Inventory;
    public Inventory_Collection Consumable_Inventory;
    public Inventory_Collection Material_Inventory;
    #endregion

    public void Awake()
    {
        Manager = this;
        Statistic_Managers = new List<StatisticManager>();

        Inventory_Collections = new List<Inventory_Collection>();
        Inventory_Collections.Add(Weapon_Inventory); //Index 0
        Inventory_Collections.Add(Armor_Inventory); //Index 1
        Inventory_Collections.Add(Accessory_Inventory); //Index 2
        Inventory_Collections.Add(Consumable_Inventory); //Index 3
        Inventory_Collections.Add(Material_Inventory); //Index 4
    }

    public void Update()
    {
        #region Tick Values for Player / Companion
        Darkness_Tick();
        Hunger_Tick();
        Thirst_Tick();
        Health_Tick();
        #endregion
    }

    public static void Game_Over(int GameState = 0)
    {
        if (GameState == 0)
            Debug.Log("You Suck!");
        else if (GameState == 1)
            Debug.Log("You Win!");
        else if (GameState == 2)
            Debug.Log("Game Paused");
    }
    // --- Handle Independent Stats --- Allows for Calling Static
    #region Timers
    public void Darkness_Tick()
    {
        //Close to a Candle Check ---
    }

    public void Hunger_Tick()
    {
        //Time Based
        if (HungerTick_Current < HungerTick_Target)
        {
            HungerTick_Current += Time.deltaTime;
            if (HungerTick_Current >= HungerTick_Target)
            {
                HungerTick_Current = 0f;
                foreach(StatisticManager element in Statistic_Managers)
                {
                    element.Adjust_Hunger(Random.Range(-1f, -2f));
                }
            }
        }
    }

    public void Thirst_Tick()
    {
        //Time Based
        if (ThirstTick_Current < ThirstTick_Target)
        {
            ThirstTick_Current += Time.deltaTime;
            if (ThirstTick_Current >= ThirstTick_Target)
            {
                ThirstTick_Current = 0f;
                foreach (StatisticManager element in Statistic_Managers)
                {
                    element.Adjust_Thirst(Random.Range(-1f, -2f));
                }
            }
        }
    }

    public void Health_Tick()
    {
        //Hunger - Darkness - Thirst Based --- End Game Condition
    }
    #endregion
    #region Inventory
    public static bool Add_Item(Item_Data _Data)
    {
        if (_Data.GetType() == typeof(Consumable))
        {
            if (Manager.Consumable_Inventory.Add_Item(_Data)) return true; else return false;
        }

        return false;
    }
    #endregion
}

public class Player_Stats
{
    //This will hold all the player stats and will be Referenced from the Game Manager ---
}

public class InventoryItem_Stack
{
    public InventoryItem_Stack(int _currentStack, Item_Data _data)
    {
        currentStack = _currentStack;
        data = _data;
    }

    public int currentStack;
    public Item_Data data;
}