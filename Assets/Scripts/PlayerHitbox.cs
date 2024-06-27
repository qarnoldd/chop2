using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private Health health;
    private Attack attack;

    void Start()
    {
        health = GetComponent<Health>();
        attack= GetComponent<Attack>();
    }

    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EnemyHurtbox")
        {
            health.takeDamage(other.gameObject.GetComponent<EnemyHurtbox>().getDamage());
        }
    }
}
