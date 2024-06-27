using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private Health health;
    private Attack player;
    public string targetTag;

    void Start()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == targetTag)
        {
            health.takeDamage(other.gameObject.GetComponent<Hurtbox>().getDamage());
        }
    }
}
