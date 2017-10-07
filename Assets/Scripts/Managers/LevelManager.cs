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

    public static LevelManager m_levelManager = null;

    // Variable getters and setters.
    public Vector3 PlayerTutorialStartPosition { get { return m_v3PlayerTutorialStartPosition; } }
    public Vector3 PlayerLevelOneStartPosition { get { return m_v3PlayerLevelOneStartPosition; } }
    public Vector3 PlayerLevelTwoStartPosition { get { return m_v3PlayerLevelTwoStartPosition; } }

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
        InitialiseDontDestroyOnLoad();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoadComplete;
    }

    private void OnSceneLoadComplete(Scene a_scene, LoadSceneMode a_loadSceneMode)
    {
        switch (a_scene.name)
        {
            case m_strMainMenuSceneName:
                {
                    // InitialiseCanvasses.
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(false);
                    break;
                }

            case m_strTutorialSceneName:
                {
                    // InitialiseCanvasses.
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);

                    // Set Player's new position.
                    Player.m_Player.transform.position = m_v3PlayerTutorialStartPosition;
                    break;
                }

            case m_strLevelOneSceneName:
                {
                    // InitialiseCanvasses.
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);

                    // Set Player's new position.
                    Player.m_Player.transform.position = m_v3PlayerLevelOneStartPosition;
                    break;
                }

            case m_strLevelTwoSceneName:
                {
                    // InitialiseCanvasses.
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);

                    // Set Player's new position.
                    Player.m_Player.transform.position = m_v3PlayerLevelTwoStartPosition;
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
        DontDestroyOnLoad(ExpManager.m_experiencePointsManager.gameObject);

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
}
