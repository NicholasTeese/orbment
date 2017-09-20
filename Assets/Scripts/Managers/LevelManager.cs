using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private bool m_bLoadInitiated = false;

    private void Update()
    {
        if (!m_bLoadInitiated)
        {
            //SceneManager.LoadSceneAsync()
        }
    }
}
