using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float playerMovement;
    public int  hitpoints = 20;
    private float scaleX;
    private Rigidbody2D rb;

    public bool Hited;
    public CapsuleCollider2D DmgZone;
    Animator animator;

    public GameObject player;
    private Player PLAYER;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DmgZone.enabled = false;
        scaleX = transform.localScale.x;
        PLAYER = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PLAYER.hitpoints>0 && hitpoints>0) Movement();
    }

    private void Movement()
    {

        int dir = (transform.position.x < player.transform.position.x) ? 1 : -1;
        if((Math.Abs(transform.position.x - player.transform.position.x) > 0.2) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) rb.velocity = new Vector2(dir * playerMovement, rb.velocity.y);

        if (dir < 0) transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
        else if (dir > 0) transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (collision.CompareTag("Player") && PLAYER.hitpoints>0)
        {
            animator.SetBool("InRange", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("InRange", false);
    }

    private void EnableDmgZone()
    {
        DmgZone.enabled =true;
    }

    private void DisableDmgZone()
    {
        DmgZone.enabled=false;
    }

    // Getter's & Setter's
    public Animator getAnimator()
    {
        return animator;
    }

    public void TakeDamage()
    {
        hitpoints -= PLAYER.damage;
        rb.velocity = new Vector2(player.transform.localScale.x * 0.5f, 0.5f);
        Debug.Log(hitpoints);
        if (hitpoints > 0) animator.Play("Hit");
        if (hitpoints <= 0){
            animator.Play("Death");  
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
