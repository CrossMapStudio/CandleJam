using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBase : MonoBehaviour
{
    public Physical_Object Pickup_Object { get; private set; }
    public enum ObjectType
    {
        Inventory_Item
    }

    public ObjectType Object_Type;

    [Header("Inventory Item Data")]
    public Item_Data ItemData;

    [Header("UI Channel - Top Message")]
    public TopMessage_Channel UI_Channel;

    private void Awake()
    {
        Pickup_Object = new Inventory_Item_Pickup(this);
    }
}

public interface Physical_Object
{
    public void On_Interact();
}

public class Inventory_Item_Pickup : Physical_Object
{
    InteractionBase Base;
    public Inventory_Item_Pickup(InteractionBase _Base)
    {
        Base = _Base;
    }

    public void On_Interact()
    {
        if (GameManager.Add_Item(Base.ItemData))
        {
            //Call To UI SO Link ---
            Base.UI_Channel.RaiseEvent(Base.ItemData);
            GameObject.Destroy(Base.gameObject);
        }
    }
}
