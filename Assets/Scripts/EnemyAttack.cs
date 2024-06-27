using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameObject range;
    public GameObject player;
    public GameObject hurtbox;
    private Animator anim;
    private Enemy enemy;
    public float damage;
    public float attackRange;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        attack();
    }

    private void attack()
    {
        if(inRange())
        {
            hurtbox.GetComponent<Hurtbox>().damage = damage;
            anim.SetBool("attacking", true);
            enemy.enabled = false;
        }
        else
        {
            anim.SetBool("attacking", false);
            enemy.enabled = true;
        }
    }

    public void enableHurtbox()
    {
        hurtbox.active = true;
    }
    public void disableHurtbox()
    {
        hurtbox.active = false;
    }

    private bool inRange()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= attackRange)
            return true;
        else
            return false;
    }

}
