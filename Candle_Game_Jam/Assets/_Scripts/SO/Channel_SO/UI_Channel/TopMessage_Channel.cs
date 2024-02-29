using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Top Message Channel", menuName = "Channels/UI/Top Message Channel")]
public class TopMessage_Channel : ScriptableObject
{
    public UnityEvent<Item_Data> OnEventRaised;
    public void RaiseEvent(Item_Data data)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(data);
    }
}
