using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Player_Controller;
    public static Rigidbody2D Player_RigidBody;

    StateMachine Player_StateMachine;

    [Header("Player Movement")]
    public float Player_Speed;

    public void Awake()
    {
        Player_Controller = this;
        Player_RigidBody = GetComponent<Rigidbody2D>();

        Player_StateMachine = new StateMachine();
        Player_StateMachine.changeState(new Player_Movement());
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

    public void onEnter()
    {

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
    }
}
#endregion
