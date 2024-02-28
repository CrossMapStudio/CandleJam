using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponType : ScriptableObject
{
    //Start of the Animation --- Call of the Animator --- Animation Finished

    //Weapon Statistics for Melee ---
    public AnimatorOverrideController Animation_Override;
    public int Attack_CombinationLimit;

    public StatisticGroup Stat_Group;

    public abstract void OnLightAttack_Initialized();
    public abstract void OnLightAttack_AnimationEvent();
    public abstract void OnLightAttack_Finished();
    private UnityEvent On_AttackFinish;
    public UnityEvent Get_OnAttackFinish => On_AttackFinish;


    public abstract void onHeavyAttack();
    public abstract void onBlock();
    public abstract void onChargeAttack();
}

[Serializable]
public class StatisticGroup
{
    public enum W_SpecialTrait
    {
        Burning,
        Corrosion,
        Plague,
        Bleed,
        Faith,
        Arcane,
        Luck,
        Leach,
        Curse,
        Illusion
    }

    public enum W_Requirement
    {
        Strength,
        Stamina,
        Arcane,
        Agility,
        Faith,
        SingleWeapon,
        DuelWeapon,
        TwoHandedWeapon,
        DefensiveWeapon
    }

    [SerializeField] private Weapon_BaseStats Stats;
    public Weapon_BaseStats Get_BaseStats => Stats;

    //Special Trait Values ---
    [SerializeField] private List<Weapon_SpecialTrait> Traits;
    public List<Weapon_SpecialTrait> GetSpecialTraits => Traits;


    [SerializeField] private List<Weapon_Requirements> Requirements;
    public List<Weapon_Requirements> Get_Requirements => Requirements;
}

[Serializable]
public class Weapon_BaseStats
{
    [Range(0f, 100f)]
    public float Damage, Stamina, Arcane, Speed, Critical_Damage, Critical_Percentage;

    [Range(0f, 100f)]
    public float H_Damage, H_Stamina, H_Arcane, H_Speed, H_Critical_Damage, H_Critical_Percentage;
}

[Serializable]
public class Weapon_SpecialTrait
{
    public StatisticGroup.W_SpecialTrait Trait_Type;

    [Range(0f, 100f)]
    public float Percentage;

    [Range(0f, 100f)]
    public float Level;

    [Range(0f, 100f)]
    public float CursePercentage;

    [Range(0f, 100f)]
    public float CurseLevel;
}

[Serializable]
public class Weapon_Requirements
{
    public StatisticGroup.W_Requirement Stat_Type;
    [Range(0, 100)]
    public int Requirement_Level;
}
