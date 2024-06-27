using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private Health health;
    public string targetTag;

    void Start()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == targetTag)
        {
            Debug.Log(this.tag  + "HIT");
            health.takeDamage(other.gameObject.GetComponent<Hurtbox>().getDamage());
        }
    }
}
