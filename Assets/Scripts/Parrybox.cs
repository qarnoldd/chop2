using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Parrybox : MonoBehaviour
{
    public bool parrySuccess = false;
    public AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.tag == "EnemyHurtbox" && !parrySuccess)
        {
            parrySuccess = true;
            AudioManager.Instance.PlaySFX("parry", audio);
            print("PARRY SOUND PLAYED");
            other.gameObject.GetComponent<EnemyHurtbox>().stun();
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyAttack>().stunned();
            }
        }
    }
    public void resetParry()
    { parrySuccess = false; }
}
