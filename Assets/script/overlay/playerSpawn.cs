using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawn : MonoBehaviour
{
    public SpawnDirection entryDirection;
}

public enum SpawnDirection
{
    Left,
    Right,
    Up,
    Down
}
