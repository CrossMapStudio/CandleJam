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
    public StateMachine Get_PlayerStateMachine => Player_StateMachine;

    [HideInInspector]
    public InteractionBase Interactable_Object;

    [Header("Player Movement")]
    public float Player_Speed;

    [Header("Interaction")]
    public float Interaction_Distance;
    public LayerMask Interaction_Layer;

    public void Awake()
    {
        Player_Controller = this;
        Player_RigidBody = GetComponent<Rigidbody2D>();

        Player_StateMachine = new StateMachine();
        Player_StateMachine.changeState(new Player_Movement());

        Player_Renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Player_Animator = transform.GetChild(0).GetComponent<Animator>();
        Player_Transform = transform;
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

        /*
        CurrentAnimationSet = true ? Movement.magnitude != 0 : false;
        
        if (StoredAnimationSet != CurrentAnimationSet)
            StoredAnimationSet = CurrentAnimationSet;
        PlayerController.Player_Animator.SetBool("Player_Moving", StoredAnimationSet);
        */

        if (Movement.x == 0 && Movement.y == 0)
        {
            PlayerController.Player_Animator.Play("Idle", 0);
        }

        if (Movement.x != 0 && Movement.y == 0)
        {
            PlayerController.Player_Animator.Play("SideRun", 0);
        }

        if (Movement.y != 0 && Movement.x == 0)
        {
            if (Movement.y > 0)
            {
                PlayerController.Player_Animator.Play("UpRun", 0);
            }
            else
            {
                PlayerController.Player_Animator.Play("DownRun", 0);
            }
        }

        if (Movement.x != 0 && Movement.y != 0)
        {
            if (Movement.y > 0)
            {
                PlayerController.Player_Animator.Play("UpRunDiagonal", 0);
            }
            else
            {
                PlayerController.Player_Animator.Play("DownRunDiagonal", 0);
            }
        }




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
public class Player_Hold : stateDriverInterface
{
    public string ID => "Player_Hold";

    public void onEnter()
    {
        PlayerController.Player_Animator.Play("Idle", 0);
    }

    public void onExit()
    {

    }

    public void onFixedUpdate()
    {

    }

    public void onGUI()
    {

    }

    public void onLateUpdate()
    {

    }

    public void onUpdate()
    {

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
