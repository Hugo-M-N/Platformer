using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public CameraMovement Instance;
    public Transform player;
    public float smoothSpeed = 5f;
    public float lookAheadDistance = 2f;
    public float lookAheadSmooth = 5f;
    private float currentLookAhead = 0f;
    private float lookAheadVelocity = 0f;
    private Rigidbody2D playerRb;

    void Start()
    {
		StartCoroutine(playerFinder());
    }

    public IEnumerator playerFinder()
    {
        while (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                playerRb = player.GetComponent<Rigidbody2D>();
            }
            yield return null;
		}

	}
    void LateUpdate()
    {
		if (!cameraTop.lockX)
        {
            totalMovement();
        }
        else
        {
            lockedX();
        }
    }

    public void totalMovement()
    {
        if (player == null) return;

        float targetLookAhead = 0f;

        float horizontalVelocity = playerRb.velocity.x;

        if (Mathf.Abs(horizontalVelocity) > 0.1f)
        {
            targetLookAhead = Mathf.Sign(horizontalVelocity) * lookAheadDistance;
        }

        currentLookAhead = Mathf.SmoothDamp(
            currentLookAhead,
            targetLookAhead,
            ref lookAheadVelocity,
            1f / lookAheadSmooth
        );

        Vector3 targetPos = new Vector3(
            player.position.x + currentLookAhead,
            player.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * smoothSpeed
        );
    }

    public void lockedX()
    {
        if (player == null) return;

        float targetLookAhead = 0f;

        float horizontalVelocity = playerRb.velocity.x;

        if (Mathf.Abs(horizontalVelocity) > 0.1f)
        {
            targetLookAhead = Mathf.Sign(horizontalVelocity) * lookAheadDistance;
        }

        currentLookAhead = Mathf.SmoothDamp(
            currentLookAhead,
            targetLookAhead,
            ref lookAheadVelocity,
            1f / lookAheadSmooth
        );

        Vector3 targetPos = new Vector3(
            cameraTop.lockedX,
            player.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * smoothSpeed
        );
    }
}