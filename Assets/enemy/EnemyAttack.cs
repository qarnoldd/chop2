using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    private GameObject range;
    public GameObject player;
    public GameObject hurtbox;
    
    public GameObject corpse;

    private NavMeshAgent agent;
    private Animator anim;
    private EnemyAttack enemy;
    private EnemyMovement em;
    private Health health;
    private Collider col;
    AudioSource audio;

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
        em = GetComponent<EnemyMovement>();
        audio = GetComponent<AudioSource>();
        health = GetComponent<Health>();
        col = GetComponent<Collider>();
    }
    void Update()
    {
        attack();
        checkDead();
    }

    private void checkDead()
    {
        if (health.dead == true)
        {
            print("DEAD");
            Instantiate(corpse, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    private void attack()
    {
        if(inRange())
        {
            em.enabled = false;
            anim.SetBool("attacking", true);
        }
        else
        {
            em.enabled = true;
            anim.SetBool("attacking", false);
        }
    }
    public void enableHurtbox()
    {
        hurtbox.GetComponent<EnemyHurtbox>().damage = damage;
        hurtbox.GetComponent<CapsuleCollider>().enabled = true;
    }
    public void disableHurtbox()
    {
        hurtbox.GetComponent<CapsuleCollider>().enabled = false;
    }
    private bool inRange()
    {
        if (player.transform.position != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= attackRange)
                return true;
        }
        return false;
    }
    public void stunned()
    {
        col.isTrigger = true;
        damage = 0;
        anim.SetBool("attacking", false);
        anim.SetBool("moving", false);
        anim.SetBool("stunned", true);
        enemy.enabled = false;
        em.enabled = false;
        
    }
    public void notStunned()
    {
        col.isTrigger = false;
        damage = finalDamage;
        anim.SetBool("stunned", false);
        enemy.enabled = true;
        em.enabled = true;
    }
    public void onParry()
    {
        parryable = true;
    }
    public void offParry()
    {
        parryable = false;
    }

    void playSlash()
    {
        if(anim.GetBool("stunned"))
            AudioManager.Instance.PlaySFX("slash", audio);
    }
}
