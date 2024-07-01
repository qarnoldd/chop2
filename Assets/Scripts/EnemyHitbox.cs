using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private Health health;
    private EnemyAttack attack;
    public string targetTag;
    AudioSource audio;

    void Start()
    {
        health = GetComponent<Health>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerHurtbox")
        {
            AudioManager.Instance.PlaySFX("hit", audio);
            health.takeDamage(other.gameObject.GetComponent<Hurtbox>().getDamage());
        }
    }
}
