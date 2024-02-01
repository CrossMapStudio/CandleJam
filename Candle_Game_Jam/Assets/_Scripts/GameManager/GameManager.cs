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
    }

    public void Thirst_Tick()
    {
        //Time Based / Stamina Based
    }

    public void Health_Tick()
    {
        //Hunger - Darkness - Thirst Based --- End Game Condition
    }
}
