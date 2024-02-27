using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public static WeaponController Controller;
    [SerializeField] private List<WeaponGroup> WeaponGroups;

    [SerializeField] private Collider2D HitRadius;

    //Melee ->
    [SerializeField] private Combat_Channel Combat_Channel;

    //Weapon Type
    private WeaponType Assigned_WeaponType;
    [SerializeField] private CombatInvoke On_LightInitialized, On_LightEvent, On_LightFinished;

    private bool Attack_Queue;
  
    public void Awake()
    {
        Controller = this;
        Weapon_Check();
    }

    public void Update()
    {
        /*
        if (Player_Input.Get_PlayerAttack() && Controller.Check_MainWeapon())
        {
            if (PlayerController.Player_Controller.Get_PlayerStateMachine.getCurrentStateName() == "Player_Movement")
            {
                PlayerController.Player_Controller.Get_PlayerStateMachine.changeState(new Player_LightAttack());
            }
            else if (PlayerController.Player_Controller.Get_PlayerStateMachine.getCurrentStateName() == "Player_LightAttack")
            {
                Attack_Queue = true;
            }
        }
        */
    }

    public void Set_WeaponProperties(int index, Weapon data)
    {
        WeaponGroups[index]._renderer.gameObject.SetActive(true);

        WeaponGroups[index]._data = data;
        WeaponGroups[index]._renderer.material = data.Get_Material;

        if (data.Weapon_Type.Animation_Override != null)
            WeaponGroups[index]._animator.runtimeAnimatorController = data.Weapon_Type.Animation_Override;

        Assigned_WeaponType = WeaponGroups[index]._data.Weapon_Type;

        //Clear Listeners on New Weapon Input ---
        On_LightInitialized.OnEventRaised.RemoveAllListeners();
        On_LightEvent.OnEventRaised.RemoveAllListeners();
        On_LightFinished.OnEventRaised.RemoveAllListeners();

        On_LightInitialized.OnEventRaised.AddListener(Assigned_WeaponType.OnLightAttack_Initialized);
        On_LightEvent.OnEventRaised.AddListener(Assigned_WeaponType.OnLightAttack_AnimationEvent);
        On_LightFinished.OnEventRaised.AddListener(Assigned_WeaponType.OnLightAttack_Finished);
        Assigned_WeaponType.Get_OnAttackFinish.AddListener(On_LightAttackFinished);

        Weapon_Check();
    }

    public void Weapon_StartMove()
    {
        foreach (WeaponGroup element in WeaponGroups)
        {
            element._animator.Play("Weapon_Movement");
        }
    }

    public void Weapon_OnMove(Vector2 _currentMovement)
    {
        foreach(WeaponGroup element in WeaponGroups)
        {
            element._animator.SetFloat("XMovement", _currentMovement.x);
            element._animator.SetFloat("YMovement", _currentMovement.y);
        }
    }

    public void Weapon_Check()
    {
        foreach (WeaponGroup element in WeaponGroups)
        {
            if (element._data == null)
                element._renderer.gameObject.SetActive(false);
        }
    }

    public bool Check_MainWeapon()
    {
        return WeaponGroups[0]._data;
    }

    public void Weapon_Flip(bool flipValue)
    {
        foreach (WeaponGroup element in WeaponGroups)
        {
            element._renderer.flipX = flipValue;
        }
    }

    public void On_LightAttackInitialize()
    {
        foreach(WeaponGroup element in WeaponGroups)
        {
            element._animator.Play("Light_Attack_Weapon0", 0, 0);
        }
    }

    public void On_LightAttackFinished()
    {
        //Check if there is a Queue --- Use Current Player State to Drive?
        if (Attack_Queue)
        {
            //Call Another Attack
            PlayerController.Player_Controller.Get_PlayerStateMachine.changeState(new Player_LightAttack());
            Attack_Queue = false;
        }
        else
        {
            //Change Back to Player Free Movement ---
            PlayerController.Player_Controller.Get_PlayerStateMachine.changeState(new Player_Movement());
        }
    }

    //Cheeky Lil Animation for Inspecting / Managing Inventory
    public void On_InventoryWeaponInspect()
    {
        foreach (WeaponGroup element in WeaponGroups)
        {
            element._animator.Play("Inventory_WeaponInspect");
        }
    }
}

[System.Serializable]
public class WeaponGroup
{
    [HideInInspector]
    public Weapon _data;

    public SpriteRenderer _renderer;
    public Animator _animator;

    public Combat_AnimationCaller Trigger;
}
