using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    AudioSource audio;
    public bool dead = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        checkAlive();
    }

    public void takeDamage(float amount)
    {
        health -= amount;
    }

    private void checkAlive()
    {
        if(health <= 0)
        {
            dead = true;
        }
    }
}
