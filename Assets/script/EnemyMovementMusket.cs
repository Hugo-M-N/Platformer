using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementMusket : MonoBehaviour
{
    public Animator headAnimator;
    private bool isAttacking = false;
    //private bool isMoving = true;
    public bool isAttacked = false;
    public int hitpoints = 100;
    private Animator anim;
    public Transform player;
    public bool playerdetection = false;
    private bool movingRight = true;
    public float attackCooldown = 1.5f;
    public float attackRange = 3f;
    private float scaleX;
    private EnemyMovementMusketHead head;
    public EnemySoundController soundController;
    void Start()
    {
        soundController = GetComponent<EnemySoundController>();
        anim = GetComponent<Animator>();
        scaleX = transform.localScale.x;
        head = GetComponentInChildren<EnemyMovementMusketHead>();
    }

    // Update is called once per frame
    void Update()
    {
        float dir = player.position.x > transform.position.x ? 1f : -1f;
        movingRight = dir > 0;
        transform.localScale = movingRight ? new Vector2(scaleX, scaleX) : new Vector2(-scaleX, scaleX);
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (playerdetection)
        {
            AttackPlayer();
        }
    }
    public void PlayHeadAnim()
    {
        
         headAnimator.Play("AttackHead");
    }
    void AttackPlayer()
    {
        if (isAttacking) return;

        isAttacking = true;
        //isMoving = false;
        anim.SetBool("moving", false);
        /*float dir = player.position.x > transform.position.x ? 1f : -1f;
        movingRight = dir > 0;
        transform.localScale = movingRight ? new Vector2(3, 3) : new Vector2(-3, 3);*/
        anim.SetTrigger("attack");

        StartCoroutine(AttackCooldown());
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        //isMoving = true;
    }
    public void TakeDamage()
    {

        hitpoints -= 10;
        anim.Play("Hurt");
        anim.SetTrigger("isHurt");
        head.isAttacked = true;
        Debug.Log("Enemy Hit");
        soundController.playhitSound();
        if (hitpoints <= 0)
        {
            soundController.playouchSound();
            anim.SetBool("isDead", true);
            //isMoving = false;
            isAttacking = false;
            this.enabled = false;
            Collider2D col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.simulated = false;
            }
        }
    }
}
