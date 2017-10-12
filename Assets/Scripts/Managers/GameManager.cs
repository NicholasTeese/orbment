using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private bool m_bGameIsPaused = false;
    public bool GameIsPaused { get { return m_bGameIsPaused; } set { m_bGameIsPaused = value; } }

    public static GameManager m_gameManager = null;
    //private GameObject m_cursor = null;
    public Texture2D m_crosshair = null;
    public Vector2 offset;

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

        Cursor.lockState = CursorLockMode.Confined;
        offset = new Vector2(1.0f, 1.0f);
        Cursor.SetCursor(m_crosshair, offset, CursorMode.Auto);
    }

    private void Start()
    {
        Time.timeScale = 1;

        PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
        DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
    }

    void Update ()
    {
        //Debug.Log(offset);
        if (SceneManager.GetActiveScene().name == LevelManager.m_strMainMenuSceneName)
        {
            return;
        }

        if (!Player.m_Player.IsAlive && !DeathMenuManager.m_deathMenuManager.gameObject.activeInHierarchy)
        {
            Time.timeScale = 0.0f;
            DeathMenuManager.m_deathMenuManager.gameObject.SetActive(true);
            PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(false);
        }

        if(!ExpManager.m_experiencePointsManager.PerkTreeOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || InputManager.StartButton())
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
	}

    public void Continue()
    {
        m_bGameIsPaused = false;
        Time.timeScale = 1;
        PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
        DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
        PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);
    }

	public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitToDesktop()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
