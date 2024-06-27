using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float rollSpeed = 3;

    Rigidbody rb;
    private Animator anim;
    private CharacterMovement cm;
    public GameObject hurtbox;
    public float damage;

    private Boolean rolling = false;
    private Vector3 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cm = GetComponent<CharacterMovement>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        UserInput();
    }
    void UserInput()
    {
        //ATTACK
        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("attacking", true);
        }

        //ROLL
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("rolling", true);
        }
    }

    void isAttacking()
    {
        cm.enabled = false;
    }

    void notAttacking()
    {
        hurtbox.SetActive(false);
        anim.SetBool("attacking", false);
        cm.enabled = true;
    }
    void enableHurtbox()
    {
        hurtbox.GetComponent<Hurtbox>().damage = damage;
        hurtbox.SetActive(true);
    }

    void disableHurtbox()
    {
        damage = 0;
        hurtbox.SetActive(false);
    }

    void isRolling()
    {
        direction = transform.forward;
        rb.AddForce(direction * rollSpeed * 10f, ForceMode.Impulse);
        cm.enabled = false;
    }

    void notRolling()
    {
        anim.SetBool("rolling", false);
        cm.enabled = true;
    }
}
