using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager m_pauseMenuCanvasManager;

    private void Awake()
    {
        if (m_pauseMenuCanvasManager == null)
        {
            m_pauseMenuCanvasManager = this;
        }
        else if (m_pauseMenuCanvasManager != this)
        {
            Destroy(gameObject);
        }
    }
}
