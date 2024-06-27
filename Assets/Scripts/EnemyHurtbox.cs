using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    public float damage;
    public GameObject user;
    void Start()
    {
    }

    void Update()
    {
    }

    public float getDamage()
    {
        this.damage = user.GetComponent<EnemyAttack>().damage;
        return damage;
    }

    public void stun()
    {
        user.GetComponent<EnemyAttack>().stunned();
    }
}
