using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    private GameObject range;
    public GameObject player;
    public GameObject hurtbox;
    private NavMeshAgent agent;
    private Animator anim;
    private EnemyAttack enemy;
    public float finalDamage;
    public float damage;
    public float attackRange;
    public bool parryable;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        enemy = GetComponent<EnemyAttack>();
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
        hurtbox.GetComponent<EnemyHurtbox>().damage = damage;
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
    public void stunned()
    {
        if (parryable)
        {
            damage = 0;
            anim.SetBool("attacking", false);
            anim.SetBool("moving", false);
            anim.SetBool("stunned", true);
            enemy.enabled = false;
            agent.enabled = false;
        }
    }
    public void notStunned()
    {
        damage = finalDamage;
        anim.SetBool("stunned", false);
        enemy.enabled = true;
        agent.enabled = true;
    }
    public void onParry()
    {
        parryable = true;
    }
    public void offParry()
    {
        parryable = false;
    }
}
