using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Driver : SingletonPersistent<Input_Driver>
{
    #region Inputs
    //Subscribe through the Player Controller ---
    private Player_Input Input_Controller;
    public Player_Input Get_Input => Input_Controller;

    private static InputAction Movement;
    public static InputAction Get_Movement => Movement;

    private static InputAction Interact;
    public static InputAction Get_Interact => Interact;

    private static InputAction Inventory;
    public static InputAction Get_Inventory => Inventory;

    private static InputAction Attack_Light;
    public static InputAction Get_AttackLight => Attack_Light;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        //Create the Input System ---
        //Create a new instance for the Input ---
        Input_Controller = new Player_Input();

        Movement = Input_Controller.Player_Base.Movement;
        Interact = Input_Controller.Player_Base.Interact;
        Inventory = Input_Controller.Player_Base.Inventory;
        Attack_Light = Input_Controller.Player_Base.Attack;
    }

    public void OnEnable()
    {
        //Enable the Input ---
        Movement.Enable();
        Interact.Enable();
        Inventory.Enable();
        Attack_Light.Enable();
    }
}
