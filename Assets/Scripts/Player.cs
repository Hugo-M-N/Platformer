using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float playerMovement;
    public float jumpForce = 7f;
    private float scaleX;
    public int hitpoints = 100;
    public int damage;
    public CapsuleCollider2D DmgZone1;
    public CapsuleCollider2D DmgZone2;
    public BoxCollider2D DmgZone3;
    private int combo;
    private float lastAttack;
    private float comboWindow = 0.25f;
    private float nextComboTime = 0f;
    Animator animator;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        DmgZone1.enabled = false;
        DmgZone2.enabled = false;
        DmgZone3.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        scaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();        
    }

    private void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("xVelocity", Math.Abs(move));

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Movement")) rb.velocity = new Vector2(move * playerMovement, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f) rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetFloat("yVelocity", rb.velocity.y);
        if (Mathf.Abs(rb.velocity.y) > 0.01f) animator.SetBool("IsGrounded", false);
        else animator.SetBool("IsGrounded", false);

        if (move < 0) transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
        else if (move > 0) transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);

    }

    private void Attack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("BaseAttack"))
        {
            DmgZone1.enabled = false;
            animator.SetBool("BaseAttack", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Combo1"))
        {
            DmgZone2.enabled = false;
            animator.SetBool("Combo1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Combo2"))
        {
            DmgZone3.enabled = false;
            animator.SetBool("Combo2", false);
            rb.velocity = (transform.localScale.x>1) ? new Vector2(playerMovement, rb.velocity.y) : new Vector2(-playerMovement, rb.velocity.y);
                combo = 0;
        }

        if(Time.time - lastAttack > comboWindow)
        {
            combo = 0;
        }
        
        if(Time.time > nextComboTime)
        {
            if (Input.GetButtonDown("Fire1")) Combo();
        }
    }

    void Combo()
    {
        lastAttack = Time.time;
        combo++;
        if (combo == 1)
        {
            DmgZone1.enabled = true;
            animator.SetBool("BaseAttack", true);
        }

        combo = Math.Clamp(combo, 0, 3);

        if(combo >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("BaseAttack"))
        {
            DmgZone1.enabled = false;
            animator.SetBool("BaseAttack", false);
            DmgZone2.enabled = true;
            animator.SetBool("Combo1", true);
        }

        if(combo >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Combo1"))
        {
            DmgZone2.enabled = false;
            animator.SetBool("Combo1", false);
            DmgZone3.enabled = true;
            animator.SetBool("Combo2", true);
        }
    }

    public void TakeDamage()
    {

        //hitpoints -= 50;
        //Debug.Log(hitpoints);
        Debug.Log("Player Hit");
        if (hitpoints <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }
}