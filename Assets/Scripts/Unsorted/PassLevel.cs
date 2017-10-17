using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassLevel : MonoBehaviour
{

	void OnTriggerEnter(Collider a_collider)
    {
		if (a_collider.tag == "Player")
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case LevelManager.m_strTutorialSceneName:
                    {
                        LevelManager.m_levelManager.InitialiseDontDestroyOnLoad();
                        LevelManager.m_levelManager.LoadNextLevelAsyncOperation.allowSceneActivation = true;
                        break;
                    }

                case LevelManager.m_strLevelOneSceneName:
                    {
                        LevelManager.m_levelManager.InitialiseDontDestroyOnLoad();
                        LevelManager.m_levelManager.LoadNextLevelAsyncOperation.allowSceneActivation = true;
                        break;
                    }

                case LevelManager.m_strLevelTwoSceneName:
                    {
                        LevelManager.m_levelManager.DestroyAllDontDestroyOnLoad();
                        LevelManager.m_levelManager.LoadNextLevelAsyncOperation.allowSceneActivation = true;
                        break;
                    }
            }
		}
	}
}
