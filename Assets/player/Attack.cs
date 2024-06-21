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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cm = GetComponent<CharacterMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = transform.forward;
        if (rolling)
        {
            rb.AddForce(direction * rollSpeed / Time.deltaTime);
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
        rolling = true;
        cm.enabled = false;
    }

    void notRolling()
    {
        rolling = false;
        anim.SetBool("rolling", false);
        cm.enabled = true;
    }
}
