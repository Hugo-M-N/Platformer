using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveSprint = 10f;
    public float jumpForce = 7f;
    public float playerMovement;
    private Animator anim;
    private Rigidbody2D rb;
    public bool sprint = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    private void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        if (sprint) playerMovement = moveSprint;
        if (!sprint) playerMovement = moveSpeed;
        rb.velocity = new Vector2(move * playerMovement, rb.velocity.y);
        
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (move > 0)
        {
            transform.localScale = new Vector3(3, transform.localScale.y, transform.localScale.z);
        }

        if (move < 0)
        {
            transform.localScale = new Vector3(-3, transform.localScale.y, transform.localScale.z);
        }
        anim.SetBool("Moving", move != 0);

        if (move != 0 && Input.GetKey(KeyCode.LeftShift)) sprint = true;
        else sprint = false;
         
    }
    
    
}
