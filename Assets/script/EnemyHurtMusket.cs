using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtMusket : MonoBehaviour
{
    private EnemyMovementMusket enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyMovementMusket>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("PlayerDmg"))
        {
            enemy.TakeDamage();
        }
    }
}
