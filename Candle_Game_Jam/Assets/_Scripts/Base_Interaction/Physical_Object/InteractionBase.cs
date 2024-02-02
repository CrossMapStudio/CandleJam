using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBase : MonoBehaviour
{
    public Physical_Object Pickup_Object { get; private set; }
    public enum ObjectType
    {
        Inventory_Item,
        Door,
        Chest,
        Button
    }

    public ObjectType Object_Type;

    [Header("Inventory Item Data")]
    public Item_Data ItemData;

    //Active in Bubble ---
    //UI Prompt
    //Add to Selection

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
        if (PlayerController.Player_Controller.Add_Item(Base.ItemData))
        {
            GameObject.Destroy(Base.gameObject);
        }

    }
}
