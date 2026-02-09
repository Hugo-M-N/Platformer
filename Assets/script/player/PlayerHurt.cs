using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    private Player player;

    private float damageCooldown = 0.5f;
    private float lastDamageTime;

    private BoxCollider2D hitbox;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        hitbox = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("EnemyAttack") && Time.time > lastDamageTime + damageCooldown)
        {
            player.TakeDamage();
            if (player.hitpoints <= 0) hitbox.enabled = false;
            lastDamageTime = Time.time;
        }
    }
}
