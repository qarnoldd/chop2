using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject player;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        agent.SetDestination(player.transform.position);
    }
}
