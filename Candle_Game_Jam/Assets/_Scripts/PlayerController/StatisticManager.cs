using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticManager : MonoBehaviour
{
    // --- Check if Safe From Darkness
    // --- Check if Health Needs to Change ---

    [HideInInspector]
    public bool InCandleRange = false;

    public void Start()
    {
        GameManager.Statistic_Managers.Add(this);
    }

    // ---

    private float Dark_Meter = 100f;
    private float Hunger_Meter = 100f;
    private float Thirst_Meter = 100f;
    private float Health_Meter = 100f;

    //Functions to Check Certain Parameters ---
    public void Adjust_Hunger(float value)
    {
        Hunger_Meter += value;
        if (Hunger_Meter > 100f)
        {
            Hunger_Meter = 100f;
        }

        UIManager.SetUIHungerBar(Hunger_Meter);
    }

    public void Adjust_Thirst(float value)
    {
        Thirst_Meter += value;
        if (Thirst_Meter > 100f)
        {
            Thirst_Meter = 100f;
        }

        UIManager.SetUIThirstBar(Thirst_Meter);
    }
}
