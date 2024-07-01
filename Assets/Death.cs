using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public Animator anim;
    public AudioSource audio;
    public float deathTimer;
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        AudioManager.Instance.PlaySFX("death", audio);
        anim.SetBool("dead", true);
        Invoke(nameof(kill), deathTimer);
    }

    void kill()
    {
        Destroy(gameObject);
    }
}