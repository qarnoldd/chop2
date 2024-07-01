using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject player;
    AudioSource audio;
    private Animator anim;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
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

    void playFootstep()
    {
        AudioManager.Instance.PlaySFX("footstep2", audio);
    }
}
