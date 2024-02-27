using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Weapon", menuName = "Weapon Types/Melee Weapon")]
public class Melee : WeaponType
{
    #region Not Implemented Yet
    public override void onBlock()
    {

    }

    public override void onChargeAttack()
    {

    }

    public override void onHeavyAttack()
    {

    }
    #endregion

    //Better Here Since Only Melee Will Need This ---
    public Combat_Channel CombatChannel;

    public override void OnLightAttack_Initialized()
    {
        //Nothing Here ---
        Debug.Log("Attack Initialized");
    }

    public override void OnLightAttack_AnimationEvent()
    {
        CombatChannel.RaiseEvent(PlayerController.Player_Controller.Get_StoredDirection, Stat_Group.Get_BaseStats.Damage);
    }

    public override void OnLightAttack_Finished()
    {
        if (Get_OnAttackFinish != null)
            Get_OnAttackFinish.Invoke();
    }
}