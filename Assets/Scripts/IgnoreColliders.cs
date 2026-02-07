using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColliders : MonoBehaviour
{
    public Collider2D colliderA;
    public Collider2D colliderB;
    

    void Start()
    {
        // Hace que colliderA y colliderB se ignoren entre sí
        Physics2D.IgnoreCollision(colliderA, colliderB);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
