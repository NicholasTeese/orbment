using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public bool paused = false;
    public bool dead = false;

	public GameObject m_healthBar;
    public GameObject deathMenu;
    public GameObject hud;

    public static GameManager m_gameManager;

    private void Awake()
    {
        if (m_gameManager == null)
        {
            m_gameManager = this;
        }
        else if (m_gameManager != this)
        {
            Destroy(gameObject);
        }
    }
	
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || InputManager.StartButton())
        {
            if (!paused)
            {
                paused = true;
            }
            else
            {
                paused = false;
            }
        }
        if (paused)
        {
            Time.timeScale = 0.0f;
            PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(true);
            hud.SetActive(false);
        }
        else if (dead == true)
        {
            Time.timeScale = 0.0f;
            DeathMenuManager.m_deathMenuManager.gameObject.SetActive(true);
            hud.SetActive(false);
        }
        else
        {
            PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
            DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
            hud.SetActive(true);
            Time.timeScale = 1;
        }
	}

	public void ContinueGame()
    {
		paused = false;
	}
	public void Options()
    {

	}
	public void LoadMainMenu()
    {

	}
	public void QuitToDesktop()
    {

	}
	public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level1");
    }
}
