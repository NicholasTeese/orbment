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
            Time.timeScale = 0.0f;

            switch (SceneManager.GetActiveScene().name)
            {
                case LevelManager.m_strTutorialSceneName:
                    {
                        InGameCanvas.m_inGameCanvas.FadeIn = false;
                        break;
                    }

                case LevelManager.m_strLevelOneSceneName:
                    {
                        InGameCanvas.m_inGameCanvas.FadeIn = false;
                        break;
                    }

                case LevelManager.m_strLevelTwoSceneName:
                    {
                        InGameCanvas.m_inGameCanvas.FadeIn = false;
                        break;
                    }
            }
		}
	}
}
