using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private Health health;
    AudioSource audio;

    void Start()
    {
        health = GetComponent<Health>();
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyHurtbox")
        {
            AudioManager.Instance.PlaySFX("hit", audio);
            health.takeDamage(other.gameObject.GetComponent<EnemyHurtbox>().getDamage());
        }
    }
}
