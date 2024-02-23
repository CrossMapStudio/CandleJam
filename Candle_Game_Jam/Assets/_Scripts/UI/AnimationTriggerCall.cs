using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerCall : MonoBehaviour
{
    private Action Trigger;
    public Action Get_SetTrigger { get { return Trigger; } set { Trigger = value; } }

    public void Call_Trigger()
    {
        Trigger.Invoke();
    }
}
