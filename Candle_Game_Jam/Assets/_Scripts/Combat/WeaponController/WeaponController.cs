using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //Get in Awake
    SpriteRenderer Weapon_Sprite;
    public SpriteRenderer Renderer => Weapon_Sprite;

    Animator Weapon_Animator;
    public Animator GetWeaponAnimator => Weapon_Animator;


    public void Awake()
    {
        Weapon_Sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Weapon_Animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void Set_WeaponProperties(Material mat, RuntimeAnimatorController anim)
    {
        Weapon_Sprite.material = mat;
        Weapon_Animator.runtimeAnimatorController = anim;
    }
}
