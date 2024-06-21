using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 4;
    public float rotateSpeed = 2;
    public float jumpStrength = 10;
    public bool onFloor = true;

    private Rigidbody rb;
    private Animator anim;

    Vector3 lookPos;
    Vector3 keyboardRotate;

    // RUNTIME -------------------------------------------------------------------------------------

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        keyboardRotate = new Vector3(0,0,0);
    }

    void Update()
    {
        print("VELOCITY: " + rb.velocity);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 200))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;
        if (onFloor)
        {
            anim.SetBool("jumping", false);
            lookAttack(lookDir);
            lookRoll();
        }
        else
        {
            anim.SetBool("jumping", true);
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if(onFloor)
            movement(horizontal, vertical, speed);
        else
            movement(horizontal, vertical, speed/2);
    }

    // FUNCTIONS -----------------------------------------------------------------------------------

    void movement(float horizontal, float vertical, float moveSpeed) //movement determined by keyboard
    {
        keyboardRotate.x = Mathf.Clamp(horizontal * rotateSpeed, -1, 1);
        keyboardRotate.z = Mathf.Clamp(vertical * rotateSpeed, -1, 1);


        if (horizontal != 0 || vertical != 0)
            anim.SetBool("moving", true);
        else
            anim.SetBool("moving", false);

        Vector3 movement = new Vector3(horizontal,0, vertical).normalized;

        rb.AddForce(movement * moveSpeed / Time.deltaTime);

        if (Input.GetKeyDown("space") && onFloor)
        {
            rb.AddForce(new Vector3(0, jumpStrength, 0) / Time.deltaTime, ForceMode.Impulse);
            onFloor = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            onFloor = true;
        }
    }

    void lookAttack(Vector3 lookDir) //set attack animation state
    { 
        if (Input.GetMouseButtonDown(0))
        {
            transform.LookAt(transform.position + lookDir, Vector3.up);
            anim.SetBool("attacking", true);
            this.enabled = false;
        }

        else
        {
            transform.LookAt(transform.position + keyboardRotate, Vector3.up);
        }
    }

    void lookRoll() //set roll animation state
    {
        transform.LookAt(transform.position + keyboardRotate, Vector3.up);
        if (Input.GetKeyDown("left shift"))
        {
            anim.SetBool("rolling", true);
            this.enabled = false;
        }
    }
}
