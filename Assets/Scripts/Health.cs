using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;

    void Start()
    {
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
            Destroy(this.gameObject);
        }
    }
}
