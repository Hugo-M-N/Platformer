using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMelee : MonoBehaviour
{
    public Collider2D attackCollider;
    // Start is called before the first frame update
    void Start()
    {
       
        attackCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void ActivarTrigger()
    {
        attackCollider.enabled = true;
    }

    public void DesactivarTrigger()
    {
        attackCollider.enabled = false;
    }
}
