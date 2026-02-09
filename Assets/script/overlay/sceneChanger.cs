using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{
    public string spawnPointName;
    public string SceneName;
    public static int dir;
    public CanvasGroup fadePanel;
    public float alpha;
    public float fadeSpeed = 1;
    public float start;
    public int spawnInactivityTime = 4;
    public BoxCollider2D spawnCollider;
    public Player Player;
    public GameObject top;
    public playerSpawn playerSpawn;
    public Camera mainCamera;
	private Rigidbody2D playerRb;
	// Start is called before the first frame update
	void Start()
    {
		variableSaver VariableSaver = FindObjectOfType<variableSaver>();
        StartCoroutine(spawnInactivity());
		if (VariableSaver.firstTimeSceneChanger && SceneManager.GetSceneByName("firstScenario").Equals(SceneManager.GetActiveScene()))
        {
			VariableSaver.playerSpawner(GameObject.Find("respawnPoint").transform.position);
			VariableSaver.firstTimeSceneChanger = false;
		}
        else 
        {
            if(Player== null)
            {
                playerSpawn = GameObject.Find(spawnPointName).GetComponent<playerSpawn>();
                VariableSaver.playerSpawner(playerSpawn.transform.position);
			}
			StartCoroutine(playerFinder());
            fadePanel.alpha = 1f;
			StartCoroutine(screenFadeOut());
            StartCoroutine(playerExitMovement(Player));
			Player.canMove = true;
		} 
    }

	public IEnumerator playerFinder()
	{
		while (Player == null)
		{
			GameObject playerObj = GameObject.FindWithTag("Player");
			if (playerObj != null)
			{
				Player = playerObj.GetComponent<Player>();
                playerRb = Player.getRB();
			}
			yield return null;
		}

	}

	// Update is called once per frame
	void Update()
    {
        
    }

    public IEnumerator spawnInactivity()
    {
		while (Player == null)
		{
			GameObject playerObj = GameObject.FindWithTag("Player");
			if (playerObj != null)
			{
				Player = playerObj.GetComponent<Player>();
				playerRb = Player.GetComponent<Rigidbody2D>();
			}
			yield return null;
		}
		float timer = 0f;
        spawnCollider.enabled = false;
        Debug.Log(spawnCollider.enabled);
		while (timer < spawnInactivityTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        spawnCollider.enabled = true;
	}

	public IEnumerator playerExitMovement(Player Player)
    {
        Player.transform.position = playerSpawn.transform.position;
        Player.canMove = false;
        float move = Input.GetAxisRaw("Horizontal");
        if (playerSpawn.entryDirection == SpawnDirection.Left) dir = 1;
		if (playerSpawn.entryDirection == SpawnDirection.Right) dir = -1;
        playerRb = Player.getRB();
        mainCamera.transform.position = new Vector3(playerSpawn.transform.position.x + dir * 10, mainCamera.transform.position.y, mainCamera.transform.position.z);
        top.transform.position = new Vector3(playerSpawn.transform.position.x + dir * 10, playerSpawn.transform.position.y, playerSpawn.transform.position.z);


        while (true)
        {
            playerRb.velocity = new Vector2(dir * Player.playerMovement, Player.getRB().velocity.y);
            Player.setRB(playerRb);
            Player.transform.localScale = new Vector3(Player.transform.localScale.x * dir, Player.transform.localScale.y, Player.transform.localScale.z);

            yield return null;
            if (dir > 0 && Player.transform.position.x >= top.transform.position.x) break;
            if (dir < 0 && Player.transform.position.x <= top.transform.position.x) break;
        }
    }
	public IEnumerator playerEntryMovement(Player Player)
	{
		Player.canMove = false;
        float move = Input.GetAxisRaw("Horizontal");

        if (playerSpawn.entryDirection == SpawnDirection.Left) dir = -1;
		if (playerSpawn.entryDirection == SpawnDirection.Right) dir = 1;
		Rigidbody2D rb = Player.getRB();

		while (true)
		{
            rb.velocity = new Vector2(move * Player.playerMovement, rb.velocity.y);
            yield return null;
			if (dir > 0 && Player.transform.position.x >= playerSpawn.transform.position.x) break;
			if (dir < 0 && Player.transform.position.x <= playerSpawn.transform.position.x) break;
		}
	}

	public IEnumerator screenFadeIn()
    {
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadePanel.alpha = alpha;
            yield return null;
        }
    }

    public IEnumerator screenFadeOut()
    {
        StopCoroutine(fadeSequence());
        alpha = 1f;
		while (alpha > 0f)
        {
            alpha -= Time.unscaledDeltaTime * fadeSpeed;
            fadePanel.alpha = alpha;
            yield return null;
        }
        alpha = 0f;
    }

    public IEnumerator fadeSequence()
    {
        yield return StartCoroutine(screenFadeIn());
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);
        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone) yield return null;

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player Player = collision.GetComponent<Player>();
            Player.canMove = false;
            StartCoroutine(playerEntryMovement(Player));
            StartCoroutine(fadeSequence());
        }
    }
}

