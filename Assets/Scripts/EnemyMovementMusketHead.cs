using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementMusketHead : MonoBehaviour
{
    public Transform player;      
    public float maxAngle = 30f;
    public SpriteRenderer Head;
    public Transform upperBody;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Animator anim;
    public bool isAttacked = false;
    void Start()
    {
        //Head = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Head.enabled = false;
    }
    void Update()
    {
        bool movingRight = transform.root.localScale.x > 0;
        transform.localScale = movingRight ? new Vector2(1, 1) : new Vector2(-1, -1);
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        if (!movingRight)
        {
            
            angle = Mathf.Repeat(angle + 180f, 360f) - 180f;

            if (angle > -180f+maxAngle && angle < 180f-maxAngle)
            {
                angle = angle < 0 ? -150f : 150f;
            }
            transform.localRotation = Quaternion.Euler(0, 0, -angle);
        }
        else
        {
            angle = Mathf.Clamp(angle, -maxAngle, maxAngle);
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        if (isAttacked == true) 
        {
            Head.enabled = false;
            anim.Play("IdleHead");
            isAttacked = false;
        }
       
        
    }
    

    public void ShowHead()
    {
        Head.enabled = true;
    }

    public void HideHead()
    {
        Head.enabled = false;
    }
    public void Shoot()
    {
        bool movingRight = transform.root.localScale.x > 0;
        Vector2 direction = player.position - firePoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (!movingRight)
        {
            
            angle = Mathf.Repeat(angle + 180f, 360f) - 180f;

            if (angle > -180f+maxAngle && angle < 180f-maxAngle)
            {
                angle = angle < 0 ? -150f : 150f;
            }
            transform.localRotation = Quaternion.Euler(0, 0, -angle);
        }
        else
        {
            angle = Mathf.Clamp(angle, -maxAngle, maxAngle);
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<MusketBullet>().SetDirection(angle);
    }
}
