using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 4;
    public float rotateSpeed = 2;

    Rigidbody rb;

    Vector3 lookPos;
    Vector3 keyboardRotate;
    
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        keyboardRotate = new Vector3(0,0,0);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;
        lookAttack(lookDir);
        lookRoll(lookDir);
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //axis
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        keyboardRotate.x = Mathf.Clamp(horizontal * rotateSpeed, -1, 1);
        keyboardRotate.z = Mathf.Clamp(vertical * rotateSpeed, -1, 1);

        if (horizontal != 0 || vertical != 0)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }

        Vector3 movement = new Vector3(horizontal, 0,vertical);
        rb.AddForce(movement * speed /Time.deltaTime);
    }

    void lookAttack(Vector3 lookDir)
    { 
        if (Input.GetMouseButtonDown(0))
        {
            transform.LookAt(transform.position + lookDir, Vector3.up);
            anim.SetBool("moving", false);
            anim.SetBool("attacking", true);
            this.enabled = false;
        }

        else
        {
            transform.LookAt(transform.position + keyboardRotate, Vector3.up);
            anim.SetBool("attacking", false);
        }
    }

    void lookRoll(Vector3 lookDir)
    {
        transform.LookAt(transform.position + keyboardRotate, Vector3.up);
        if (Input.GetKeyDown("space"))
        {
            anim.SetBool("rolling", true);
            this.enabled = false;
        }

        else
        {
            anim.SetBool("rolling", false);
        }
    }
}
