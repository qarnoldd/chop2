using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Parrybox : MonoBehaviour
{

    void Start()
    {
    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyHurtbox")
        {
            other.gameObject.GetComponent<EnemyHurtbox>().stun();
        }
    }
}
