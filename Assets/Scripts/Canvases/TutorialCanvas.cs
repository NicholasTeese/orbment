using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvas : MonoBehaviour
{
    public static TutorialCanvas m_tutorialCanvas;

    private void Awake()
    {
        if (m_tutorialCanvas == null)
        {
            m_tutorialCanvas = this;
        }
        else if (m_tutorialCanvas != this)
        {
            Destroy(gameObject);
        }
    }
}
