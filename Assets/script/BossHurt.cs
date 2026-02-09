using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurt : MonoBehaviour
{
    private Boss boss;

    private float damageCooldown = 0.2f;
    private float lastDamageTime;

    private BoxCollider2D hitbox;

    private void Awake()
    {
        boss = GetComponentInParent<Boss>();
        hitbox = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlayerDmg") && Time.time > lastDamageTime + damageCooldown)
        {
            boss.TakeDamage();
            if (boss.hitpoints <= 0) hitbox.enabled = false;
            lastDamageTime = Time.time;
        }
    }
}
