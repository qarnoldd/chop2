using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private Health health;
    private EnemyAttack attack;
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
        if (other.gameObject.tag == "PlayerHurtbox")
        {
            health.takeDamage(other.gameObject.GetComponent<Hurtbox>().getDamage());
        }
    }
}
