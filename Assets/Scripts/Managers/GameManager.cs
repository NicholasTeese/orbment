using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float m_fDeathFadeOutSpeed = 0.008f;
    private float m_fTimeScaleSlowDownSpeed = 0.008f;

    private bool m_bGameIsPaused = false;
    private bool m_bShowCursor = true;
    private bool m_bForceHideCursor = false;
    private bool m_bDeathFadeOutStarted = false;

    private Vector2 offset = Vector2.zero;

    private Vector3 m_v3LastMousePosition = Vector3.zero;

    public Texture2D m_crosshair;

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

        offset = new Vector2(m_crosshair.width / 2.0f, m_crosshair.height / 2.0f);

        if (SceneManager.GetActiveScene().name == LevelManager.m_strMainMenuSceneName)
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
        if (SceneManager.GetActiveScene().name != LevelManager.m_strMainMenuSceneName)
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

        if (!Player.m_player.IsAlive && !DeathMenuManager.m_deathMenuManager.gameObject.activeInHierarchy)
        {
            OnPlayerDeath();
        }

        if(!ExpManager.m_experiencePointsManager.PerkTreeOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || InputManager.StartButton()
                && !PauseMenuManager.m_pauseMenuManager.gameObject.activeInHierarchy && Player.m_player.IsAlive)
            {
                if (!m_bGameIsPaused)
                {
                    if (TutorialCanvas.m_tutorialCanvas != null)
                    {
                        TutorialCanvas.m_tutorialCanvas.gameObject.SetActive(false);
                    }

                    OrbCountDisplay.m_orbCountDisplay.gameObject.SetActive(false);

                    SerializationManager.m_serializationManager.Load();
                    m_bGameIsPaused = true;
                    Time.timeScale = 0.0f;
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(true);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(false);
                    Cursor.visible = true;
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

    private void OnPlayerDeath()
    {
        if (!m_bDeathFadeOutStarted)
        {
            InGameCanvas.m_inGameCanvas.FadeOutSpeed = m_fDeathFadeOutSpeed;
            InGameCanvas.m_inGameCanvas.FadeIn = false;
            InGameCanvas.m_inGameCanvas.FadeOutComplete = false;
            m_bDeathFadeOutStarted = true;
        }

        if (PlayerHUDManager.m_playerHUDManager.gameObject.activeInHierarchy)
        {
            PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(false);
        }

        if (OrbCountDisplay.m_orbCountDisplay.gameObject.activeInHierarchy)
        {
            OrbCountDisplay.m_orbCountDisplay.gameObject.SetActive(false);
        }

        if (Time.timeScale > m_fTimeScaleSlowDownSpeed)
        {
            Time.timeScale -= m_fTimeScaleSlowDownSpeed;

            if (Time.timeScale <= m_fTimeScaleSlowDownSpeed)
            {
                Time.timeScale = 0;
            }
        }
        else if (InGameCanvas.m_inGameCanvas.FadeImage.color.a >= 0.9f)
        {
            Time.timeScale = 0.0f;
            m_bGameIsPaused = true;
            Cursor.visible = true;
            DeathMenuManager.m_deathMenuManager.gameObject.SetActive(true);
        }
    }

    public void Continue()
    {
        if (TutorialCanvas.m_tutorialCanvas != null)
        {
            TutorialCanvas.m_tutorialCanvas.gameObject.SetActive(true);
        }

        m_bGameIsPaused = false;
        Time.timeScale = 1;
        PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
        DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
        PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);
        Cursor.visible = false;
    }

	public void LoadMainMenu()
    {
        LevelManager.m_levelManager.DestroyAllDontDestroyOnLoad();
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
