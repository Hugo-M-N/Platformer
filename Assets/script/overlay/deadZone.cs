using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deadZone : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public Transform respawnPoint;
    public CanvasGroup fadePanel;
    public float fadeSpeed = 1f;

    public bool isFading = false;

	void Start()
	{
		StartCoroutine(playerFinder());
		if (fadePanel == null)
		{
			GameObject fadeObj = GameObject.Find("YouDied");
			fadePanel = fadeObj.GetComponent<CanvasGroup>();
		}
	}

	public IEnumerator playerFinder()
	{
		while (playerRb == null )
		{
			GameObject playerObj = GameObject.FindWithTag("Player");
			if (playerObj != null)
			{
				playerRb = playerObj.GetComponent<Rigidbody2D>();
			}
			yield return null;
		}
	}
	public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeAndRespawn());
        }
    }

    public IEnumerator FadeAndRespawn()
    {
        isFading = true;

        Player playerMovement = playerRb.GetComponent<Player>();
        if (playerMovement != null) playerMovement.enabled = false;

        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadePanel.alpha = alpha;
            yield return null;
        }

        playerRb.position = respawnPoint.position;
        playerRb.velocity = Vector2.zero;

        while (alpha > 0f)
        {
            alpha -= Time.unscaledDeltaTime * fadeSpeed;
            fadePanel.alpha = alpha;
            yield return null;
        }

        if (playerMovement != null) playerMovement.enabled = true;
        isFading = false;
    }
}
