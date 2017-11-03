using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenCanvas : MonoBehaviour
{
    private float m_fFadeSpeed = 0.01f;

    private bool m_bShowAIELogo = true;
    private bool m_bAIELogoShown = false;
    private bool m_bVSOLogoShown = false;

    public GameObject m_mainMenuCanvas;
    public GameObject m_mainMenuCamera;
    public GameObject m_splashScreenCamera;

    public Image m_aieLogoImage;
    public Image m_vsoLogoImage;
    public Image m_fadeImage;

    private void Awake()
    {
        if (m_mainMenuCamera.gameObject.activeInHierarchy)
        {
            m_mainMenuCamera.gameObject.SetActive(false);
        }

        if (!m_splashScreenCamera.gameObject.activeInHierarchy)
        {
            m_splashScreenCamera.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (m_bShowAIELogo && !m_bAIELogoShown)
        {
            if (FadeIn(m_fadeImage, m_fFadeSpeed))
            {
                m_bAIELogoShown = true;
            }
        }
        else if (m_bShowAIELogo && m_bAIELogoShown)
        {
            if (FadeOut(m_fadeImage, m_fFadeSpeed))
            {
                m_bShowAIELogo = false;
                m_aieLogoImage.gameObject.SetActive(false);
                m_vsoLogoImage.gameObject.SetActive(true);
            }
        }
        else if (!m_bShowAIELogo && !m_bVSOLogoShown)
        {
            if (FadeIn(m_fadeImage, m_fFadeSpeed))
            {
                m_bVSOLogoShown = true;
            }
        }
        else if (!m_bShowAIELogo && m_bVSOLogoShown)
        {
            if (FadeOut(m_fadeImage, m_fFadeSpeed))
            {
                m_mainMenuCanvas.SetActive(true);
                m_mainMenuCamera.SetActive(true);
                gameObject.SetActive(false);
                m_splashScreenCamera.SetActive(false);
            }
        }
    }


    private bool FadeIn(Image a_fadeImage, float a_fFadeSpeed)
    {
        Color fadeImageColor = a_fadeImage.color;

        if (fadeImageColor.a > 0.0f)
        {
            fadeImageColor.a -= a_fFadeSpeed;
            a_fadeImage.color = fadeImageColor;
            return false;
        }

        return true;
    }

    private bool FadeOut(Image a_fadeImage, float a_fFadeSpeed)
    {
        Color fadeImageColor = a_fadeImage.color;

        if (fadeImageColor.a < 1.0f)
        {
            fadeImageColor.a += a_fFadeSpeed;
            a_fadeImage.color = fadeImageColor;
            return false;
        }

        return true;
    }

    private void PlaySplashScreen()
    {

    }
}
