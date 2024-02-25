using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public static WeaponController Controller;
    [SerializeField] private List<WeaponGroup> WeaponGroups;
  
    public void Awake()
    {
        Controller = this;
        Weapon_Check();
    }

    public void Set_WeaponProperties(int index, Weapon data)
    {
        WeaponGroups[index]._renderer.gameObject.SetActive(true);

        WeaponGroups[index]._data = data;
        WeaponGroups[index]._renderer.material = data.Get_Material;

        if (data.Animation_Override != null)
            WeaponGroups[index]._animator.runtimeAnimatorController = data.Animation_Override;

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

    public void On_LightAttack()
    {
        foreach(WeaponGroup element in WeaponGroups)
        {
            element._animator.Play("Light_Attack_Weapon0");
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

    public AnimationTriggerCall Trigger;
}
