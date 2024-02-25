using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerCall : MonoBehaviour
{
    public AnimationActionSO AnimationEventTrigger;

    public void On_AnimationEvent()
    {
        AnimationEventTrigger.OnEventRaised.Invoke();
    }
}
