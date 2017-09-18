using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private bool m_bGameIsPaused = false;
    public bool GameIsPaused { get { return m_bGameIsPaused; } set { m_bGameIsPaused = value; } }

    public GameObject m_healthBar;
    public GameObject deathMenu;

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

    private void Start()
    {
        PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
        DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
        PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);
    }

    void Update ()
    {
        if (!Player.m_Player.IsAlive && !DeathMenuManager.m_deathMenuManager.gameObject.activeInHierarchy)
        {
            Time.timeScale = 0.0f;
            DeathMenuManager.m_deathMenuManager.gameObject.SetActive(true);
            PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.Escape) || InputManager.StartButton())
        {
            if (!m_bGameIsPaused)
            {
                m_bGameIsPaused = true;
                Time.timeScale = 0.0f;
                PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(true);
                PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(false);
            }
            else
            {
                m_bGameIsPaused = false;
                Time.timeScale = 1;
                PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);
            }
        }
	}
	public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
	}

	public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
