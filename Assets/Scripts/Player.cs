using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerMovement;
    public float jumpForce = 7f;
    private float scaleX;
    public int hitpoints = 100;
    public float stamina = 100;
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
    private SpriteRenderer sr;
    public Sprite death;

    // Start is called before the first frame update
    void Start()
    {
        
        DmgZone1.enabled = false;
        DmgZone2.enabled = false;
        DmgZone3.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitpoints > 0)
        {
            Movement();
            Attack();
            if(stamina<100) stamina += 0.05f;
        }

    }

    private void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("xVelocity", Math.Abs(move));

        rb.velocity = new Vector2(move * playerMovement, rb.velocity.y);

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
        if (combo == 1 && stamina>=20)
        {
            animator.SetBool("BaseAttack", true);
            stamina -= 20;

        }

        combo = Math.Clamp(combo, 0, 3);

        if(combo >= 2 && stamina>=30 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("BaseAttack"))
        {
            animator.SetBool("BaseAttack", false);
            animator.SetBool("Combo1", true);
            stamina -= 30;
        }

        if (combo >= 3 && stamina >= 50 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Combo1"))
        {
            animator.SetBool("Combo1", false);
            animator.SetBool("Combo2", true);
            stamina -= 50;

        }
    }
    public void TakeDamage()
    {
        hitpoints -= 20;
        Debug.Log(hitpoints);
        if (hitpoints>0)animator.Play("PlayerHit");
        if (hitpoints <= 0)
        {
            animator.Play("PlayerDeath");
            sr.sprite = death;

        }
    }

    public void enableDMG1()
    {
        DmgZone1.enabled=true;
    }
    public void enableDMG2()
    {
        DmgZone2.enabled=true;
    }
    public void enableDMG3()
    {
        DmgZone3.enabled=true;
    }

    public void disableAnimator()
    {
        animator.enabled=false;
    }
}