using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public GameObject menuUI;
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        if (menuUI == null)
        {
            StartCoroutine(menuFinder());
		}
		variableSaver VariableSaver = FindObjectOfType<variableSaver>();
			if (VariableSaver.firstTimeMenuController && SceneManager.GetSceneByName("firstScenario").Equals(SceneManager.GetActiveScene()) && menuUI != null)
        {
            menuUI.SetActive(true);
            Time.timeScale = 0f;
            VariableSaver.firstTimeMenuController = false;
		} 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }
    public IEnumerator menuFinder()
    {
               while (menuUI == null)
        {
            menuUI = GameObject.Find("menu");
            yield return null;
        }
	}

	public void ToggleMenu()
    {
        isPaused = !isPaused;
        menuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void StartGame()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("firstScenario");
    }
}
