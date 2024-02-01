using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticManager : MonoBehaviour
{
    public void Awake()
    {
        GameManager.Statistic_Managers.Add(this);
    }

    // ---

    private float Dark_Meter = 100f;
    private float Hunger_Meter = 100f;
    private float Thirst_Meter = 100f;
    private float Health_Meter = 100f;
}
