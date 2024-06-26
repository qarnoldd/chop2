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
        anim.SetBool("attacking", false);
        cm.enabled = true;
    }

    void isRolling()
    {
        direction = transform.forward;
        rb.AddForce(direction * rollSpeed * 10f, ForceMode.Impulse);
        cm.enabled = false;
    }

    void notRolling()
    {
        rolling = false;
        anim.SetBool("rolling", false);
        cm.enabled = true;
    }
}
