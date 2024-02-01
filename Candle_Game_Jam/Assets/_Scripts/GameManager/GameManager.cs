using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // --- Game Over State
    // --- Game Win State
    // --- Game Pause State
    // --- Item Spawn

    public static List<StatisticManager> Statistic_Managers;

    public float HungerTick_Target;
    public float ThirstTick_Target;
    public float DarknessTick_Target;
    public float HealthTick_Target;

    private float HungerTick_Current;
    private float ThirstTick_Current;
    private float DarknessTick_Current;
    private float HealthTick_Current;

    public void Awake()
    {
        Statistic_Managers = new List<StatisticManager>();
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
}
