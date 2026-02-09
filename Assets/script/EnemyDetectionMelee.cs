using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionMelee : MonoBehaviour
{
    private EnemyMovementMelee enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyMovementMelee>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            enemy.playerdetection = true;
        }
    }
    /*private void OnTriggerExit2D(Collider2D other)
    {
        {
            if (other.CompareTag("Player"))
            {
                enemy.playerdetection = false;
            }
        }
    }*/
}
