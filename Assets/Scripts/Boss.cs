using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float playerMovement;
    private float scaleX;

    public int damage;
    public CapsuleCollider2D DmgZone;
    Animator animator;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();
    }

    private void Movement()
    {
        int dir = (transform.position.x < player.transform.position.x) ? 1 : -1;
        Vector2 move = new Vector2 (transform.position.x * dir * moveSpeed, transform.position.y);
        transform.Translate(move, null);
        
    }

    private void Attack()
    {

    }
}
