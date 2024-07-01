using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
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
        damage = user.GetComponent<Attack>().damage;
        return damage;
    }
}
