using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtMelee : MonoBehaviour
{
    private EnemyMovementMelee enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyMovementMelee>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("PlayerAttack"))
        {
            enemy.TakeDamage();
        }
    }
}
