using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Controller;
    private StateMachine CursorStateMachine;
    public StateMachine GetCursorStateMachine => CursorStateMachine;

    public static System.Guid Selected_ID, Swap_ID;

    public void Awake()
    {
        Controller = this;
        CursorStateMachine = new StateMachine();
    }

    public void Update()
    {
        CursorStateMachine.executeStateUpdate();
    }

    public void FixedUpdate()
    {
        CursorStateMachine.executeStateFixedUpdate();
    }

    public void LateUpdate()
    {
        CursorStateMachine.executeStateLateUpdate();
    }
}

#region States
public class Locked_Cursor : stateDriverInterface
{
    public string ID => "Locked_Cursor";

    public void onEnter()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void onExit()
    {
        //throw new System.NotImplementedException();
    }

    public void onFixedUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public void onGUI()
    {
        //throw new System.NotImplementedException();
    }

    public void onLateUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public void onUpdate()
    {
        //throw new System.NotImplementedException();
    }
}
public class Free_Cursor : stateDriverInterface
{
    public string ID => "Free_Cursor";

    public void onEnter()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void onExit()
    {
        //throw new System.NotImplementedException();
    }

    public void onFixedUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public void onGUI()
    {
        //throw new System.NotImplementedException();
    }

    public void onLateUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public void onUpdate()
    {
        //throw new System.NotImplementedException();
    }
}
public class Select_Cursor : stateDriverInterface
{
    public string ID => "Select_Cursor";
    public Select_Cursor(System.Guid ID)
    {
        CursorController.Selected_ID = ID;
    }

    public void onEnter()
    {

    }

    public void onExit()
    {

    }

    public void onFixedUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public void onGUI()
    {
        //throw new System.NotImplementedException();
    }

    public void onLateUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public void onUpdate()
    {
        //throw new System.NotImplementedException();
    }
}
#endregion
