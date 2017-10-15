using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private bool m_bGameIsPaused = false;
    public bool GameIsPaused { get { return m_bGameIsPaused; } set { m_bGameIsPaused = value; } }

    public static GameManager m_gameManager = null;
    private Texture2D m_crosshair;
    public Vector2 offset;

    private void Awake()
    {
        Time.timeScale = 1;

        if (m_gameManager == null)
        {
            m_gameManager = this;
        }
        else if (m_gameManager != this)
        {
            Destroy(gameObject);
        }

        m_crosshair = Resources.Load("Textures/Beta/UI/Cursor_White") as Texture2D;

        offset = new Vector2(m_crosshair.width / 2.0f, m_crosshair.height / 2.0f);

        if (SceneManager.GetActiveScene().name == "Beta_Main_Menu")
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(m_crosshair, offset, CursorMode.Auto);
    }

    private void Start()
    {
        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().name != "Beta_Main_Menu")
        {
            PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
        }
    }

    void Update ()
    {
        if (SceneManager.GetActiveScene().name == LevelManager.m_strMainMenuSceneName)
        {
            return;
        }

        if (!Player.m_Player.IsAlive && !DeathMenuManager.m_deathMenuManager.gameObject.activeInHierarchy)
        {
            Time.timeScale = 0.0f;
            DeathMenuManager.m_deathMenuManager.gameObject.SetActive(true);
            PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(false);
            Cursor.visible = true;
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
                    Cursor.visible = true;
                }
                else
                {
                    m_bGameIsPaused = false;
                    Time.timeScale = 1;
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);
                    Cursor.visible = false;
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
        SceneManager.LoadScene(LevelManager.m_strMainMenuSceneName);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(LevelManager.m_strLevelOneSceneName);
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene(LevelManager.m_strLevelTwoSceneName);
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
