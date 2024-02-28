using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

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

    //Used for Combat if no Input Direction is Given ---
    private Vector3 storedDirection;
    public Vector3 Get_StoredDirection => storedDirection;

    /// <summary>
    /// Connected Channels through Scriptable Objects
    /// </summary>
    #region Channels
    [SerializeField] private CombatInvoke AnimationTrigger;
    public CombatInvoke Get_Channel => AnimationTrigger;
    #endregion


    public void Awake()
    {
        Player_Controller = this;
        Player_RigidBody = GetComponent<Rigidbody2D>();

        Player_StateMachine = new StateMachine();

        Player_Renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Player_Animator = transform.GetChild(0).GetComponent<Animator>();
        Player_Transform = transform;
    }

    public void Start()
    {
        Player_StateMachine.changeState(new Player_Movement());
    }

    public void Update()
    {
        Player_StateMachine.executeStateUpdate();

        //For Keeping the Last Direction the Player Moved ---
        Vector2 stored = Input_Driver.Get_Movement.ReadValue<Vector2>();
        storedDirection = stored == Vector2.zero ? storedDirection : stored;
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
#region Player_States
public class Player_Movement : stateDriverInterface
{
    public string ID => "Player_Movement";
    private Vector2 Movement;

    private bool CurrentAnimationSet, StoredAnimationSet;

    public void onEnter()
    {
        StoredAnimationSet = CurrentAnimationSet;
        PlayerController.Player_Animator.Play("Movement");
        WeaponController.Controller.Weapon_StartMove();

        Input_Driver.Get_Interact.performed += On_Interact;
        Input_Driver.Get_AttackLight.performed += On_LightAttack;
    }

    public void onExit()
    {
        Input_Driver.Get_Interact.performed -= On_Interact;
        Input_Driver.Get_AttackLight.performed -= On_LightAttack;
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
        Movement = Input_Driver.Get_Movement.ReadValue<Vector2>();

        PlayerController.Player_Animator.SetFloat("XMovement", Movement.x);
        PlayerController.Player_Animator.SetFloat("YMovement", Movement.y);
        PlayerController.Player_Renderer.flipX = true ? PlayerController.Player_Controller.Get_StoredDirection.x < 0 : false;

        WeaponController.Controller.Weapon_OnMove(Movement);
        WeaponController.Controller.Weapon_Flip(true ? PlayerController.Player_Controller.Get_StoredDirection.x < 0 : false);
    }

    public void On_Interact(InputAction.CallbackContext context)
    {
        if (PlayerController.Player_Controller.Interactable_Object != null)
        {
            PlayerController.Player_Controller.Interactable_Object.Pickup_Object.On_Interact();
        }
    }

    public void On_LightAttack(InputAction.CallbackContext context)
    {
        if (context.interaction is HoldInteraction)
        {
            Debug.Log("Player Held - Heavy Attack");
        }
        else
        {
            PlayerController.Player_Controller.Get_PlayerStateMachine.changeState(new Player_LightAttack());
        }
    }

    public void On_HeavyAttack(InputAction.CallbackContext obj)
    {
        
    }
}
public class Player_Hold : stateDriverInterface
{
    public string ID => "Player_Hold";

    public void onEnter()
    {
        if (WeaponController.Controller.Check_MainWeapon())
        {
            PlayerController.Player_Animator.Play("Light_Attack0");
            WeaponController.Controller.On_InventoryWeaponInspect();
        }

        PlayerController.Player_Animator.SetFloat("XMovement", 0f);
        PlayerController.Player_Animator.SetFloat("YMovement", 0f);
        WeaponController.Controller.Weapon_OnMove(Vector2.zero);
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
public class Player_LightAttack : stateDriverInterface
{
    public string ID => "Player_LightAttack";

    int comboCount = 0;
    int comboCountLimit = 0;

    //Used for Shifting the Player On Attack ---
    Vector2 TargetMovePoint;
    float StepDistance = 1.8f;

    float MovementInfluence = .35f;

    private UnityEngine.Events.UnityEvent QueueAction;

    public Player_LightAttack()
    {
        comboCountLimit = WeaponController.Controller.Get_AssignedWeaponType.Attack_CombinationLimit;
        WeaponController.Controller.Get_AssignedWeaponType.Get_OnAttackFinish.AddListener(On_AttackFinished);
    }

    public void onEnter()
    {
        TargetMovePoint = Input_Driver.Get_Movement.ReadValue<Vector2>().normalized;
        TargetMovePoint *= StepDistance;
        PerformAttack();
        Input_Driver.Get_AttackLight.performed += QueueAttack;
    }

    public void onExit()
    {
        QueueAction = null;
        Input_Driver.Get_AttackLight.performed -= QueueAttack;
        WeaponController.Controller.Get_AssignedWeaponType.Get_OnAttackFinish.RemoveListener(On_AttackFinished);
    }

    public void onFixedUpdate()
    {
        PlayerController.Player_RigidBody.MovePosition(PlayerController.Player_RigidBody.position + (TargetMovePoint + (Input_Driver.Get_Movement.ReadValue<Vector2>() * MovementInfluence)) * Time.deltaTime);
    }

    public void onGUI()
    {
        
    }

    public void onLateUpdate()
    {
        
    }

    public void onUpdate()
    {
        TargetMovePoint = Vector3.Lerp(TargetMovePoint, Vector2.zero, Time.deltaTime * 10f);
    }

    private void QueueAttack(InputAction.CallbackContext context)
    {
        if (QueueAction == null)
        {
            QueueAction = new UnityEngine.Events.UnityEvent();
            if (context.interaction is HoldInteraction)
            {
                Debug.Log("Player Held - Heavy Attack");
                //Change the State on Perform Attack to Heavy Attack ---
                QueueAction.AddListener(PerformHeavyAttack);
            }
            else
            {
                comboCount++;
                QueueAction.AddListener(PerformAttack);
            }
        }
    }

    private void On_AttackFinished()
    {
        if (QueueAction != null)
        {
            QueueAction.Invoke();
            QueueAction.RemoveAllListeners();
            QueueAction = null;
        }
        else
        {
            PlayerController.Player_Controller.Get_PlayerStateMachine.changeState(new Player_Movement());
        }
    }

    //Called from the Enter State ---
    private void PerformAttack()
    {
        if (comboCount < comboCountLimit)
        {
            TargetMovePoint = Input_Driver.Get_Movement.ReadValue<Vector2>();
            TargetMovePoint *= StepDistance;

            PlayerController.Player_Renderer.flipX = true ? PlayerController.Player_Controller.Get_StoredDirection.x < 0 : false;
            WeaponController.Controller.Weapon_Flip(true ? PlayerController.Player_Controller.Get_StoredDirection.x < 0 : false);

            PlayerController.Player_Animator.Play("Light_Attack" + comboCount, 0, 0);
            WeaponController.Controller.On_LightAttackInitialize(comboCount);
        }
        else
        {
            PlayerController.Player_Controller.Get_PlayerStateMachine.changeState(new Player_Movement());
        }
    }

    private void PerformHeavyAttack()
    {
        Debug.Log("Heavy Attack Performed");
        PlayerController.Player_Controller.Get_PlayerStateMachine.changeState(new Player_Movement());
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