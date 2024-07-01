using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Attack : MonoBehaviour
{
    [Header("Movement")]
    public float rollSpeed = 3;
    
    [Header("Player Stats")]
    public float damage;
    public bool parrying = false;
    public float attackCooldown;
    public bool readyToAttack = true;
    public float parryCooldown;
    public bool readyToParry = true;
    public float rollCooldown;
    public bool readyToRoll = true;
    public float stunsize;

    [Header("Boxes")]
    public GameObject hurtbox;
    public GameObject parrybox;
    Rigidbody rb;
    private Animator anim;
    private CharacterMovement cm;
    private Health health;
    private Collider col;
    AudioSource audio;

    public GameObject corpse;

    private Vector3 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cm = GetComponent<CharacterMovement>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        health = GetComponent<Health>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        checkDead();
    }

    void FixedUpdate()
    {
        UserInput();
    }
    void UserInput()
    {
        //ATTACK
        if(Input.GetButtonDown("Fire1") && readyToAttack)
        {
            readyToAttack = false;
            anim.SetBool("attacking", true);
            Invoke(nameof(resetAttack), attackCooldown);
        }
        //ROLL
        else if (Input.GetButtonDown("Fire2") && readyToRoll)
        {
            readyToRoll = false;
            anim.SetBool("rolling", true);
            Invoke(nameof(resetRoll),rollCooldown);
        }
        //PARRY
        else if (Input.GetButtonDown("Fire3") && readyToParry)
        {
            readyToParry = false;   
            anim.SetBool("parrying", true);
            Invoke(nameof(resetParrying), parryCooldown);
        }
    }
    private void checkDead()
    {
        if (health.dead == true)
        {
            print("DEAD");
            Instantiate(corpse, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void checkParrySuccess()
    {
        if (parrybox.GetComponent<Parrybox>().parrySuccess)
        {
            parrybox.transform.localScale = parrybox.transform.localScale * stunsize;
            Invoke(nameof(resetParrySize), 1);
        }
    }

    private void resetParrySize()
    {
        parrybox.transform.localScale = parrybox.transform.localScale / stunsize;
        parrybox.GetComponent<Parrybox>().parrySuccess = false;
    }


    void resetAttack()
    {
        readyToAttack = true;
    }

    void resetRoll()
    { readyToRoll = true; }

    void resetParrying()
    {
        readyToParry = true;
    }

    void resetParry()
    {
        parrybox.GetComponent<Parrybox>().resetParry();
    }

    void parryFrame()
    {
        parrybox.SetActive(true);
        parrying = true;
    }

    void endParryFrame()
    {
        parrybox.SetActive(false); 
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
        anim.SetBool("attacking", false);
        cm.enabled = true;
    }
    void enableHurtbox()
    {
        hurtbox.GetComponent<Hurtbox>().damage = damage;
        hurtbox.GetComponent<CapsuleCollider>().enabled = true;
    }

    void disableHurtbox()
    {
        hurtbox.GetComponent<CapsuleCollider>().enabled = false;
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

    void playSlashSFX()
    {
        AudioManager.Instance.PlaySFX("slash", audio);
    }
}
