using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusketBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    private Vector2 direction;

    // Called by the shooter
    public void SetDirection(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        // Rotate bullet to face movement direction
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Start()
    {
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            
        }
    }
}
