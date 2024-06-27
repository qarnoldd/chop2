using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Movement")]
    public float rollSpeed = 3;
    
    [Header("Player Stats")]
    public float damage;
    public bool parrying = false;
    private Boolean rolling = false;

    [Header("Boxes")]
    public GameObject hurtbox;
    public GameObject parrybox;
    Rigidbody rb;
    private Animator anim;
    private CharacterMovement cm;
    

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
        
        //PARRY
        if (Input.GetButtonDown("Fire3"))
        {
            anim.SetBool("parrying", true);
        }
    }

    void parryFrame()
    {
        parrybox.active = true;
        parrying = true;
    }

    void endParryFrame()
    {
        parrybox.active = false; 
        parrying = false;
    }

    void isParrying()
    {
        cm.enabled = false;
    }
    void notParrying()
    {
        anim.SetBool("parrying", false);
        cm.enabled = true;
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
