using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTop : MonoBehaviour
{
    public static bool lockX = false;
    public static float lockedX;
    public Transform cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        while (cameraPosition == null)
        {
            cameraPosition = FindObjectOfType<CameraMovement>().transform;
		}
	}

    // Update is called once per frame
    void LateUpdate()
    {
        if (cameraPosition == null) cameraPosition = FindObjectOfType<CameraMovement>().transform;
        if (lockX)
        {
            cameraPosition.position = new Vector3(
                lockedX,
                cameraPosition.position.y,
                cameraPosition.position.z
            );
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lockX = true;
            lockedX = cameraPosition.position.x;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lockX = false;
        }
    }
}
