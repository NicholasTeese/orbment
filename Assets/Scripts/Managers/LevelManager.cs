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

    private bool m_bSceneLoadComplete = false;

    private string m_strCurrentSceneName;

    // Player level start positions.
    private Vector3 m_v3PlayerTutorialStartPosition = new Vector3(-3.33f, 0.0f, -34.77f);
    private Vector3 m_v3PlayerLevelOneStartPosition = new Vector3(-3.33f, 0.0f, -34.77f);
    private Vector3 m_v3PlayerLevelTwoStartPosition = new Vector3(-3.33f, 0.0f, -34.77f);
    // Cameras level start positions.
    private Vector3 m_v3PlayerCameraTutorialStartPosition = new Vector3(-3.33f, 13.7f, -38.26f);
    private Vector3 m_v3PlayerCameraLevelOneStartPosition = new Vector3(-3.33f, 13.7f, -38.26f);
    private Vector3 m_v3PlayerCameraLevelTwoStartPosition = new Vector3(-3.33f, 13.7f, -38.26f);

    private AsyncOperation m_loadNextLevelAsyncOperation;

    // Static reference to the LevelManager.
    public static LevelManager m_levelManager = null;

    // Variable getters and setters.
    public string CurrentSceneName { get { return m_strCurrentSceneName; } }

    // Player level start positions.
    public Vector3 PlayerTutorialStartPosition { get { return m_v3PlayerTutorialStartPosition; } }
    public Vector3 PlayerLevelOneStartPosition { get { return m_v3PlayerLevelOneStartPosition; } }
    public Vector3 PlayerLevelTwoStartPosition { get { return m_v3PlayerLevelTwoStartPosition; } }
    // Cameras level start positions.
    public Vector3 PlayerCameraTutorialStartPosition { get { return m_v3PlayerCameraTutorialStartPosition; } }
    public Vector3 PlayerCameraLevelOneStartPosition { get { return m_v3PlayerCameraLevelOneStartPosition; } }
    public Vector3 PlayerCameraLevelTwoStartPosition { get { return m_v3PlayerCameraLevelTwoStartPosition; } }

    public AsyncOperation LoadNextLevelAsyncOperation { get { return m_loadNextLevelAsyncOperation; } }

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
                    m_strCurrentSceneName = m_strMainMenuSceneName;

                    DontDestroyOnLoad(gameObject);
                    DontDestroyOnLoad(AudioManager.m_audioManager.gameObject);

                    AudioManager.m_audioManager.PlaySceneMusic(m_strCurrentSceneName);
                    break;
                }

            case m_strTutorialSceneName:
                {
                    m_strCurrentSceneName = m_strTutorialSceneName;

                    // Initialise canvasses.
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);

                    InGameCanvas.m_inGameCanvas.FadeIn = true;
                    InGameCanvas.m_inGameCanvas.FadeInComplete = false;
                    InGameCanvas.m_inGameCanvas.FadeOutComplete = false;

                    AudioManager.m_audioManager.PlaySceneMusic(m_strCurrentSceneName);
                    AudioManager.m_audioManager.FadeIn = true;

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
                    m_strCurrentSceneName = m_strLevelOneSceneName;

                    // Initialise canvasses.
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);

                    InGameCanvas.m_inGameCanvas.FadeIn = true;
                    InGameCanvas.m_inGameCanvas.FadeInComplete = false;
                    InGameCanvas.m_inGameCanvas.FadeOutComplete = false;

                    AudioManager.m_audioManager.PlaySceneMusic(m_strCurrentSceneName);
                    AudioManager.m_audioManager.FadeIn = true;

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
                    m_strCurrentSceneName = m_strLevelTwoSceneName;

                    // Initialise canvasses.
                    PauseMenuManager.m_pauseMenuManager.gameObject.SetActive(false);
                    DeathMenuManager.m_deathMenuManager.gameObject.SetActive(false);
                    PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);

                    InGameCanvas.m_inGameCanvas.FadeIn = true;
                    InGameCanvas.m_inGameCanvas.FadeInComplete = false;
                    InGameCanvas.m_inGameCanvas.FadeOutComplete = false;

                    AudioManager.m_audioManager.PlaySceneMusic(m_strCurrentSceneName);
                    AudioManager.m_audioManager.FadeIn = true;

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

        m_bSceneLoadComplete = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            m_loadNextLevelAsyncOperation.allowSceneActivation = true;
            
            if (SceneManager.GetActiveScene().name != m_strLevelTwoSceneName || SceneManager.GetActiveScene().name != m_strMainMenuSceneName)
            {
                InitialiseDontDestroyOnLoad();
            }
            else if (SceneManager.GetActiveScene().name == m_strLevelTwoSceneName)
            {
                DestroyAllDontDestroyOnLoad();
            }
        }

        if (m_bSceneLoadComplete)
        {
            StartCoroutine(LoadNextLevelAsync(false));
            m_bSceneLoadComplete = false;
        }
    }

    public void InitialiseDontDestroyOnLoad()
    {
        // Debuggers.
        DontDestroyOnLoad(DebugLevelSwitcher.m_debugLevelSwitcher.transform.parent);

        // Managers.
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameManager.m_gameManager.gameObject);
        DontDestroyOnLoad(AudioManager.m_audioManager.gameObject);

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

    public void DestroyAllDontDestroyOnLoad()
    {
        if (DebugLevelSwitcher.m_debugLevelSwitcher != null)
        {
            Destroy(DebugLevelSwitcher.m_debugLevelSwitcher.transform.parent.gameObject);
        }

        if (GameManager.m_gameManager != null)
        {
            Destroy(GameManager.m_gameManager.gameObject);
        }

        if (AudioManager.m_audioManager != null)
        {
            Destroy(AudioManager.m_audioManager.gameObject);
        }

        if (Player.m_Player != null)
        {
            Destroy(Player.m_Player.gameObject);
        }

        if (PlayerHUDManager.m_playerHUDManager != null)
        {
            Destroy(PlayerHUDManager.m_playerHUDManager.transform.parent.gameObject);
        }

        if (PerkTreeManager.m_perkTreeManager != null)
        {
            Destroy(PerkTreeManager.m_perkTreeManager.gameObject);
        }

        if (PauseMenuManager.m_pauseMenuManager != null)
        {
            Destroy(PauseMenuManager.m_pauseMenuManager.transform.parent.gameObject); // This also handles the DeathMenuManager.
        }

        if (IsoCam.m_playerCamera != null)
        {
            Destroy(IsoCam.m_playerCamera.gameObject);
        }

        if (PerkTreeCamera.m_perkTreeCamera != null)
        {
            Destroy(PerkTreeCamera.m_perkTreeCamera.gameObject);
        }
    }

    public IEnumerator LoadNextLevelAsync(bool a_bActivateScene)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case m_strMainMenuSceneName:
                {
                    m_loadNextLevelAsyncOperation = SceneManager.LoadSceneAsync(m_strTutorialSceneName);
                    break;
                }

            case m_strTutorialSceneName:
                {
                    m_loadNextLevelAsyncOperation = SceneManager.LoadSceneAsync(m_strLevelOneSceneName);
                    break;
                }

            case m_strLevelOneSceneName:
                {
                    m_loadNextLevelAsyncOperation = SceneManager.LoadSceneAsync(m_strLevelTwoSceneName);
                    break;
                }

            case m_strLevelTwoSceneName:
                {
                    m_loadNextLevelAsyncOperation = SceneManager.LoadSceneAsync(m_strMainMenuSceneName);
                    break;
                }

            default:
                {
                    Debug.Log("Scene name: " + SceneManager.GetActiveScene().name + " not recognised.");
                    break;
                }
        }

        m_loadNextLevelAsyncOperation.allowSceneActivation = a_bActivateScene;

        if (!a_bActivateScene)
        {
            yield return null;
        }
        else
        {
            yield return m_loadNextLevelAsyncOperation;
        }
    }
}
