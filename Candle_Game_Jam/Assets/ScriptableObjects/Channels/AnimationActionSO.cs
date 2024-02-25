using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Subscription", menuName = "ScriptableObjects/Channel")]
public class AnimationActionSO : ScriptableObject
{
    public UnityEvent OnEventRaised;
    public void RaiseEvent()
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke();
    }
}
