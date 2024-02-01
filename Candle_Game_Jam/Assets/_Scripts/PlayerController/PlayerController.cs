using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Player_Controller;
    public static Rigidbody2D Player_RigidBody;
    public static SpriteRenderer Player_Renderer;
    public static Animator Player_Animator;

    StateMachine Player_StateMachine;

    [Header("Player Movement")]
    public float Player_Speed;

    public void Awake()
    {
        Player_Controller = this;
        Player_RigidBody = GetComponent<Rigidbody2D>();

        Player_StateMachine = new StateMachine();
        Player_StateMachine.changeState(new Player_Movement());

        Player_Renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Player_Animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void Update()
    {
        Player_StateMachine.executeStateUpdate();
    }

    public void FixedUpdate()
    {
        Player_StateMachine.executeStateFixedUpdate();
    }

    public void LateUpdate()
    {
        Player_StateMachine.executeStateLateUpdate();
    }
}

#region Player_Input
public static class Player_Input
{
    public static Vector3 Get_Movement()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
    }

    public static bool Get_Sprint()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
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
        if (Player_Input.Get_Sprint())
            Debug.Log("Run Enabled");
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
    }
}
#endregion
