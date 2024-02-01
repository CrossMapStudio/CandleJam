using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region State Machines
public class StateMachine
{
    private stateDriverInterface currentState, previousState;
    public void changeState(stateDriverInterface newState, GameObject stateIdentity = null)
    {
        if (currentState != null)
        {
            this.currentState.onExit();
        }
        this.previousState = this.currentState;
        this.currentState = newState;
        this.currentState.onEnter();
    }

    public void executeStateUpdate()
    {
        var runningState = this.currentState;
        if (runningState != null)
        {
            this.currentState.onUpdate();
        }
    }

    public void executeStateFixedUpdate()
    {
        var runningState = this.currentState;
        if (runningState != null)
        {
            this.currentState.onFixedUpdate();
        }
    }

    public void executeStateLateUpdate()
    {
        var runningState = this.currentState;
        if (runningState != null)
        {
            this.currentState.onLateUpdate();
        }
    }

    public void executeOnGizmos()
    {
        this.currentState.onGUI();
    }

    public void previousStateSwitch()
    {
        if (this.previousState != null)
        {
            this.currentState.onExit();
            this.currentState = this.previousState;
            this.currentState.onEnter();
        }
        else
        {
            return;
        }
    }

    //To Allow Us to Check for the State
    public stateDriverInterface getCurrentState()
    {
        return currentState;
    }

    public string getCurrentStateName()
    {
        return currentState.ID;
    }

    public void GUICall()
    {
        currentState.onGUI();
    }
}
#endregion
public interface stateDriverInterface
{
    string ID { get; }
    void onEnter();
    void onUpdate();
    void onFixedUpdate();
    void onLateUpdate();
    void onExit();
    void onGUI();
}
