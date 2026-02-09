using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3((float)(player.hitpoints * 0.03), transform.localScale.y, transform.localScale.y);
        transform.localPosition = new Vector3((float)(-7 - ((100 - player.hitpoints) * 0.015)), transform.localPosition.y, transform.localPosition.z);
    }
}
