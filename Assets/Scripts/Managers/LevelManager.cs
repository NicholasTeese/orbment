using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Scene names.
    public const string m_strMainMenuSceneName = "Beta_Main_Menu";
    public const string m_strTutorialSceneName = "Beta_Tutorial";
    public const string m_strLevelOneSceneName = "Beta_Level_1";
    public const string m_strLevelTwoSceneName = "Beta_Level_2";

    // Player level start positions.
    private Vector3 m_v3PlayerTutorialStartPosition = new Vector3(-3.33f, 0.0f, -34.77f);
    private Vector3 m_v3PlayerLevelOneStartPosition = new Vector3(-3.33f, 0.0f, -34.77f);
    private Vector3 m_v3PlayerLevelTwoStartPosition = new Vector3(-3.33f, 0.0f, -34.77f);
    // Cameras level start positions.
    private Vector3 m_v3PlayerCameraTutorialStartPosition = new Vector3(-3.33f, 13.7f, -38.26f);
    private Vector3 m_v3PlayerCameraLevelOneStartPosition = new Vector3(-3.33f, 13.7f, -38.26f);
    private Vector3 m_v3PlayerCameraLevelTwoStartPosition = new Vector3(-3.33f, 13.7f, -38.26f);

    // Static reference to the LevelManager.
    public static LevelManager m_levelManager = null;

    // Variable getters and setters.
    // Player level start positions.
    public Vector3 PlayerTutorialStartPosition { get { return m_v3PlayerTutorialStartPosition; } }
    public Vector3 PlayerLevelOneStartPosition { get { return m_v3PlayerLevelOneStartPosition; } }
    public Vector3 PlayerLevelTwoStartPosition { get { return m_v3PlayerLevelTwoStartPosition; } }
    // Cameras level start positions.
    public Vector3 PlayerCameraTutorialStartPosition { get { return m_v3PlayerCameraTutorialStartPosition; } }
    public Vector3 PlayerCameraLevelOneStartPosition { get { return m_v3PlayerCameraLevelOneStartPosition; } }
    public Vector3 PlayerCameraLevelTwoStartPosition { get { return m_v3PlayerCameraLevelTwoStartPosition; } }

    private void Awake()
    {
        if (m_levelManager == null)
        {
            m_levelManager = this;
        }
        else if (m_levelManager != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoadComplete;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != m_strMainMenuSceneName)
        {
            InitialiseDontDestroyOnLoad();
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoadComplete;
    }

    private void OnSceneLoadComplete(Scene a_scene, LoadSceneMode a_loadSceneMode)
    {
        // Deactivate cameras from previous scene.
        //!? Is required to stop black screen bug when loaded between scenes.
        if (SceneManager.GetActiveScene().name != m_strMainMenuSceneName)
        {
            PerkTreeCamera.m_perkTreeCamera.gameObject.SetActive(false);
            IsoCam.m_playerCamera.gameObject.SetActive(false);
        }

        switch (a_scene.name)
        {
            case m_strMainMenuSceneName:
                {
                    // Initialise Canvasses.
                    //PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    //DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    //PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(false);
                    break;
                }

            case m_strTutorialSceneName:
                {
                    // Initialise canvasses.
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);

                    // Set player's new position.
                    Player.m_Player.transform.position = m_v3PlayerTutorialStartPosition;
                    // Set player camera's new position.
                    IsoCam.m_playerCamera.transform.position = m_v3PlayerCameraTutorialStartPosition;

                    // Initialise cameras.
                    IsoCam.m_playerCamera.gameObject.SetActive(true);

                    // Initialise pooling.
                    EnemyLootManager.m_enemyLootManager.DisableOrbs();
                    break;
                }

            case m_strLevelOneSceneName:
                {
                    // Initialise canvasses.
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);

                    // Set player's new position.
                    Player.m_Player.transform.position = m_v3PlayerLevelOneStartPosition;
                    // Set player camera's new position.
                    IsoCam.m_playerCamera.transform.position = m_v3PlayerCameraLevelOneStartPosition;

                    // Initialise cameras.
                    IsoCam.m_playerCamera.gameObject.SetActive(true);

                    // Initialise pooling.
                    EnemyLootManager.m_enemyLootManager.DisableOrbs();
                    break;
                }

            case m_strLevelTwoSceneName:
                {
                    // Initialise canvasses.
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);

                    // Set player's new position.
                    Player.m_Player.transform.position = m_v3PlayerLevelTwoStartPosition;
                    // Set player camera's new position.
                    IsoCam.m_playerCamera.transform.position = m_v3PlayerCameraLevelTwoStartPosition;

                    // Initialise cameras.
                    IsoCam.m_playerCamera.gameObject.SetActive(true);

                    // Initialise pooling.
                    EnemyLootManager.m_enemyLootManager.DisableOrbs();
                    break;
                }

            default:
                {
                    Debug.Log("Scene name: " + a_scene.name + " not recognised.");
                    break;
                }
        }
    }

    private void InitialiseDontDestroyOnLoad()
    {
        // Debuggers.
        DontDestroyOnLoad(DebugLevelSwitcher.m_debugLevelSwitcher.transform.parent);

        // Managers.
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameManager.m_gameManager.gameObject);

        // Actors.
        DontDestroyOnLoad(Player.m_Player.gameObject);

        // Canvasses.
        DontDestroyOnLoad(PlayerHUDManager.m_playerHUDManager.transform.parent);
        DontDestroyOnLoad(PerkTreeManager.m_perkTreeManager.gameObject);
        DontDestroyOnLoad(PauseMenuManager.m_pauseMenuManager.transform.parent); // This also handles the DeathMenuManager.

        // Cameras.
        DontDestroyOnLoad(IsoCam.m_playerCamera.gameObject);
        DontDestroyOnLoad(PerkTreeCamera.m_perkTreeCamera.gameObject);
    }

    public void LoadNextLevel()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case m_strMainMenuSceneName:
                {
                    SceneManager.LoadScene(m_strTutorialSceneName);
                    break;
                }

            case m_strTutorialSceneName:
                {
                    SceneManager.LoadScene(m_strLevelOneSceneName);
                    break;
                }

            case m_strLevelOneSceneName:
                {
                    SceneManager.LoadScene(m_strLevelTwoSceneName);
                    break;
                }

            case m_strLevelTwoSceneName:
                {
                    SceneManager.LoadScene(m_strMainMenuSceneName);
                    break;
                }

            default:
                {
                    Debug.Log("Scene name: " + SceneManager.GetActiveScene().name + " not recognised.");
                    break;
                }
        }
    }
}
