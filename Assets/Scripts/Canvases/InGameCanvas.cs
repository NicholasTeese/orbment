using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameCanvas : MonoBehaviour
{
    private float m_fFadeSpeed = 0.4f;

    private bool m_bFadeIn = true;
    private bool m_bFadeInComplete = false;
    private bool m_bFadeOutComplete = false;

    private Image m_fadeImage;

    public static InGameCanvas m_inGameCanvas;

    public bool FadeIn { get { return m_bFadeIn; } set { m_bFadeIn = value; } }
    public bool FadeInComplete { get { return m_bFadeInComplete; } set { m_bFadeInComplete = value; } }
    public bool FadeOutComplete { get { return m_bFadeOutComplete; } set { m_bFadeOutComplete = value; } }

    private void Awake()
    {
        if (m_inGameCanvas == null)
        {
            m_inGameCanvas = this;
        }
        else if (m_inGameCanvas != this)
        {
            Destroy(gameObject);
        }

        m_fadeImage = transform.Find("Fade_Image").GetComponent<Image>();
    }

    private void Update()
    {
        if (m_bFadeIn && !m_bFadeInComplete)
        {
            if (ImageFadeIn(m_fadeImage, m_fFadeSpeed))
            {
                m_bFadeInComplete = true;
                m_fadeImage.gameObject.SetActive(false);
            }
        }
        else if (!m_bFadeIn && !m_bFadeOutComplete)
        {
            m_fadeImage.gameObject.SetActive(true);

            if (ImageFadeOut(m_fadeImage, m_fFadeSpeed))
            {
                m_bFadeOutComplete = true;
                if (SceneManager.GetActiveScene().name != LevelManager.m_strLevelTwoSceneName)
                {
                    LevelManager.m_levelManager.InitialiseDontDestroyOnLoad();
                    LevelManager.m_levelManager.LoadNextLevelAsyncOperation.allowSceneActivation = true;
                }
                else
                {
                    LevelManager.m_levelManager.DestroyAllDontDestroyOnLoad();
                    LevelManager.m_levelManager.LoadNextLevelAsyncOperation.allowSceneActivation = true;
                }
            }
        }
    }

    private bool ImageFadeOut(Image a_fadeImage, float a_fFadeSpeed)
    {
        Color imageColour = a_fadeImage.color;

        if (imageColour.a < 1.0f)
        {
            imageColour.a += a_fFadeSpeed * Time.deltaTime;
            a_fadeImage.color = imageColour;
            return false;
        }

        return true;
    }

    private bool ImageFadeIn(Image a_fadeImage, float a_fFadeSpeed)
    {
        Color imageColour = a_fadeImage.color;

        if (imageColour.a > 0.0f)
        {
            imageColour.a -= a_fFadeSpeed * Time.deltaTime;
            a_fadeImage.color = imageColour;
            return false;
        }

        return true;
    }
}
