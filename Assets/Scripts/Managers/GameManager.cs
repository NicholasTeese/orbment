using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private bool m_bGameIsPaused = false;
    private bool m_bShowCursor = true;
    private bool m_bForceHideCursor = false;

    private Vector2 offset = Vector2.zero;

    private Vector3 m_v3LastMousePosition = Vector3.zero;

    private Texture2D m_crosshair;

    public static GameManager m_gameManager;

    public bool GameIsPaused { get { return m_bGameIsPaused; } set { m_bGameIsPaused = value; } }
    public bool ShowCursor { get { return m_bShowCursor; } set { m_bShowCursor = value; } }
    public bool ForceHideCursor { get { return m_bForceHideCursor; } set { m_bForceHideCursor = value; } }

    private void Awake()
    {
        m_v3LastMousePosition = Input.mousePosition;

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
        if (SceneManager.GetActiveScene().name != "Beta_Main_Menu")
        {
            PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
        }
    }

    void Update ()
    {
        CursorToggle();

        if (m_bShowCursor)
        {
            Cursor.visible = true;
        }
        else if (!m_bShowCursor)
        {
            Cursor.visible = false;
        }

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
                    SerializationManager.m_serializationManager.Load();
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

    private void CursorToggle()
    {
        if (m_bForceHideCursor)
        {
            m_bShowCursor = false;
            return;
        }

        if (InputManager.SecondaryInput() != Vector3.zero)
        {
            m_v3LastMousePosition = Input.mousePosition;
            m_bShowCursor = false;
        }
        else if (m_v3LastMousePosition != Input.mousePosition)
        {
            m_bShowCursor = true;
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
