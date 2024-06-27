using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    private float damage;
    public GameObject player;
    void Start()
    {
    }

    void Update()
    {
        damage = player.GetComponent<Attack>().damage;
    }

    public float getDamage()
    {
        return damage;
    }
}
