using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject player;
    private Animator anim;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        checkMoving();
    }

    void Movement()
    {
        agent.SetDestination(player.transform.position);
    }

    void checkMoving()
    {
        if (agent.velocity.magnitude > 0)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }
}
