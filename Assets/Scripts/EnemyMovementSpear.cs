using System.Collections;
using UnityEngine;

public class EnemyMovementSpear : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    public float speed = 2f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 2.5f;
    private bool movingRight = true;
    private bool isMoving = true;
    private Animator anim;
    private bool playerdetection = false;
    public Transform player;
    public float detectionRadius = 10f;
    public float attackRange = 3f;
    public float attackCooldown = 1.5f;
    private bool isAttacking = false;
    public float attackDashDistance = 1f;
    public float attackDashDuration = 0.15f;
    private float scaleX;
    void Start()
    {
        anim = GetComponent<Animator>();
        scaleX = transform.localScale.x;
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
            StartCoroutine(MoveRoutine());
            
        }
    }

    void Update()
    {
        if (isMoving)
        {
            anim.SetBool("moving", true);
            float dir = movingRight ? 1f : -1f;
            transform.Translate(new Vector2(dir, 0f) * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("moving", false);
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            
            AttackPlayer();
        }
        if (distanceToPlayer <= detectionRadius && !isAttacking)
        {
            FollowPlayer();
        }
        else if (distanceToPlayer > detectionRadius)
        {
            Transform targetWaypoint = waypoints[currentWaypoint];
            movingRight = targetWaypoint.position.x > transform.position.x;
            transform.localScale = movingRight ? new Vector2(scaleX, scaleX) : new Vector2(-scaleX, scaleX);
        }

    }
    IEnumerator MoveRoutine()
    {
        while (true)
        {
            Transform targetWaypoint = waypoints[currentWaypoint];

            movingRight = targetWaypoint.position.x > transform.position.x;
            transform.localScale = movingRight ? new Vector2(scaleX, scaleX) : new Vector2(-scaleX, scaleX);


            while (Vector2.Distance(transform.position, targetWaypoint.position) > 1f)
            {
                yield return null;
            }

            isMoving = false;
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            isMoving = true;

            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }
    void FollowPlayer()
    {
        anim.SetBool("moving", true);


        float dir = player.position.x > transform.position.x ? 1f : -1f;
        movingRight = dir > 0;

        transform.localScale = movingRight ? new Vector2(scaleX, scaleX) : new Vector2(-scaleX, scaleX);

        Vector2 newPos = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
    }

    void AttackPlayer()
    {
        if (isAttacking) return;

        isAttacking = true;
        isMoving = false;
        anim.SetBool("moving", false);
        float dir = player.position.x > transform.position.x ? 1f : -1f;
        movingRight = dir > 0;
        transform.localScale = movingRight ? new Vector2(scaleX, scaleX) : new Vector2(-scaleX, scaleX);
        anim.SetTrigger("attack");

        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        isMoving = true;
    }
    IEnumerator AttackDash()
    {
        float timer = 0f;

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.right *
                            (movingRight ? attackDashDistance : -attackDashDistance);

        while (timer < attackDashDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, timer / attackDashDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }
}
