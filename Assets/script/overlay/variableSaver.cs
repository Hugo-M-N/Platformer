using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class variableSaver : MonoBehaviour
{
    public bool firstTimeSceneChanger = true;
    public bool firstTimeMenuController = true;
	public static variableSaver Instance;
    public GameObject playerPrefab;
	// Start is called before the first frame update
	void Start()
    {
		if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playerSpawner(Vector3 position)
    {
        Instantiate(playerPrefab, position, Quaternion.identity);
	}
}
