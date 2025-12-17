using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;                 
    public float minWaitTime = 1f;           
    public float maxWaitTime = 2.5f;          
    private bool movingRight = true;     
    private bool isMoving = true;        

    void Start()
    {
        StartCoroutine(ChangeDirectionRoutine());
    }

    void Update()
    {
        if (isMoving)
        {
            float dir = movingRight ? 1f : -1f;
            transform.Translate(new Vector2(dir, 0f) * speed * Time.deltaTime);
        }
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            isMoving = true;
            yield return new WaitForSeconds(waitTime);

            isMoving = false;
            yield return new WaitForSeconds(0.5f);

            movingRight = !movingRight;
            transform.localScale = movingRight ? new Vector2(3, 3) : new Vector2(-3, 3);
        }
    }
}