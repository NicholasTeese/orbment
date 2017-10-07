using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugLevelSwitcher : MonoBehaviour
{
    public static DebugLevelSwitcher m_debugLevelSwitcher = null;

    private void Awake()
    {
        if (m_debugLevelSwitcher == null)
        {
            m_debugLevelSwitcher = this;
        }
        else if (m_debugLevelSwitcher != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(LevelManager.m_strMainMenuSceneName);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene(LevelManager.m_strTutorialSceneName);
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene(LevelManager.m_strLevelOneSceneName);
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene(LevelManager.m_strLevelTwoSceneName);
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {

        }
    }
}
