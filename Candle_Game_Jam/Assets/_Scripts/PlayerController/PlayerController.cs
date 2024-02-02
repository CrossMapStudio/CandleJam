using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Player_Controller;
    public static Transform Player_Transform;
    public static Rigidbody2D Player_RigidBody;
    public static SpriteRenderer Player_Renderer;
    public static Animator Player_Animator;

    StateMachine Player_StateMachine;

    [HideInInspector]
    public InteractionBase Interactable_Object;

    [Header("Player Movement")]
    public float Player_Speed;

    [Header("Interaction")]
    public float Interaction_Distance;
    public LayerMask Interaction_Layer;

    [Header("Inventory")]
    InventoryItem_Stack[] Inventory;

    public void Awake()
    {
        Player_Controller = this;
        Player_RigidBody = GetComponent<Rigidbody2D>();

        Player_StateMachine = new StateMachine();
        Player_StateMachine.changeState(new Player_Movement());

        Player_Renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Player_Animator = transform.GetChild(0).GetComponent<Animator>();
        Player_Transform = transform;

        Inventory = new InventoryItem_Stack[8];
    }

    public void Update()
    {
        Player_StateMachine.executeStateUpdate();
    }

    public void FixedUpdate()
    {
        Player_StateMachine.executeStateFixedUpdate();
        Interactable_Object = PlayerInteractionBase.CallInteractionCheck();
    }

    public void LateUpdate()
    {
        Player_StateMachine.executeStateLateUpdate();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Interaction_Distance);
    }

    public bool Add_Item(Item_Data data)
    {
        int emptySlot = -1;
        for(int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == null) {
                if (emptySlot == -1)
                    emptySlot = i;
                continue;
            }

            //Check if there is matching IDs --- If the Current Stack Amount of the Item is Less than the capacity, add to the number, else find another slot --- no slots available dont add the item return false ---
            if (Inventory[i].data.ID == data.ID)
            {
                if (Inventory[i].currentStack < Inventory[i].data.StackCapacity)
                {
                    Inventory[i].currentStack++;
                    UIManager.Update_Inventory(i, Inventory[i]);
                    return true;
                }
                else
                    continue;
            }
        }

        if (emptySlot != -1)
        {
            Inventory[emptySlot] = new InventoryItem_Stack(1, data);
            UIManager.Update_Inventory(emptySlot, Inventory[emptySlot]);
            return true;
        }

        return false;
    }

    public void Drop_Item(int inventorySlot)
    {

    }

    public void Drop_AllItem(int inventorySlot)
    {

    }
}

#region Player_Input
public static class Player_Input
{
    public static Vector3 Get_Movement()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
    }

    public static bool Get_Interact()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}
#endregion
#region Player_States
public class Player_Movement : stateDriverInterface
{
    public string ID => "Player_Movement";
    private Vector2 Movement;

    private bool CurrentAnimationSet, StoredAnimationSet;

    public void onEnter()
    {
        StoredAnimationSet = CurrentAnimationSet;
    }

    public void onExit()
    {

    }

    public void onFixedUpdate()
    {
        PlayerController.Player_RigidBody.MovePosition(PlayerController.Player_RigidBody.position + (Movement * PlayerController.Player_Controller.Player_Speed * Time.deltaTime));
    }

    public void onGUI()
    {

    }

    public void onLateUpdate()
    {

    }

    public void onUpdate()
    {
        Movement = Player_Input.Get_Movement().normalized;

        CurrentAnimationSet = true ? Movement.magnitude != 0 : false;
        if (StoredAnimationSet != CurrentAnimationSet)
            StoredAnimationSet = CurrentAnimationSet;
        PlayerController.Player_Animator.SetBool("Player_Moving", StoredAnimationSet);

        if (Movement.x < 0)
        {
            PlayerController.Player_Renderer.flipX = true;
        }
        else if (Movement.x > 0)
        {
            PlayerController.Player_Renderer.flipX = false;
        }

        if (Player_Input.Get_Interact())
        {
            if (PlayerController.Player_Controller.Interactable_Object != null)
            {
                PlayerController.Player_Controller.Interactable_Object.Pickup_Object.On_Interact();
            }
        }
    }
}
#endregion
public static class PlayerInteractionBase 
{ 
    public static InteractionBase CallInteractionCheck()
    {
        Collider2D[] Interactable_Objects = Physics2D.OverlapCircleAll(PlayerController.Player_Transform.position, PlayerController.Player_Controller.Interaction_Distance, PlayerController.Player_Controller.Interaction_Layer);

        float DistanceToNearestObject = PlayerController.Player_Controller.Interaction_Distance;
        Collider2D ActiveObject = null;

        if (Interactable_Objects.Length != 0)
        {
            foreach (Collider2D element in Interactable_Objects)
            {
                if (Vector2.Distance(element.transform.position, PlayerController.Player_Transform.position) <= DistanceToNearestObject)
                {
                    DistanceToNearestObject = Vector2.Distance(element.transform.position, PlayerController.Player_Transform.position);
                    if (ActiveObject != element)
                        ActiveObject = element;
                }
            }
            return ActiveObject.GetComponent<InteractionBase>();
        }
        else
        {
            return null;
        }
    }
}

public class InventoryItem_Stack
{
    public InventoryItem_Stack(int _currentStack, Item_Data _data)
    {
        currentStack = _currentStack;
        data = _data;
    }

    public int currentStack;
    public Item_Data data;
}
