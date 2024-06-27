using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    public Camera cam;

    public Animator anim;
    Vector3 rayStart;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ResetJump();
    }

    private void Update()
    {
        myInput();
        SpeedControl();
        groundCheck();
        Animator();
        rayStart = transform.position + Vector3.up * 0.1f;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Animator()
    {
        anim.SetBool("jumping", !grounded);
    }

    private void myInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("Jump") && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void mouseLookAt()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            Vector3 look = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(look);
        }
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3 (horizontalInput, 0, verticalInput);
        movement.Normalize();

        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);
            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        if(grounded)
            rb.AddForce(movement.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(movement.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        if (movement != Vector3.zero)
        {
            if (!Input.GetMouseButton(0))
            {
                float angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
            anim.SetBool("moving", true);
        }
       
        else
            anim.SetBool("moving", false);
        
        if (Input.GetMouseButton(0))
        {
            mouseLookAt();
        }
    }

    private void groundCheck()
    {
        grounded = Physics.Raycast(rayStart, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        Debug.DrawRay(rayStart, Vector3.down * (playerHeight * 0.5f + 0.2f),Color.red);

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        } 

    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(rayStart, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
