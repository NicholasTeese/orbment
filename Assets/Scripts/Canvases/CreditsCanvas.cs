using UnityEngine;
using UnityEngine.UI;

public class CreditsCanvas : MonoBehaviour
{
    private float m_fFadeSpeed = 0.01f;

    private bool m_bShowPageOne = true;
    private bool m_bShowPageTwo = false;
    private bool m_bPageOneShown = false;
    private bool m_bPageTwoShown = false;
    private bool m_bPagesSwitched = false;

    public GameObject m_pageOne;
    public GameObject m_pageTwo;

    public Image m_fadeImage;

    private void Awake()
    {
        if (m_pageOne == null)
        {
            m_pageOne = transform.Find("Page_One").gameObject;
        }

        if (m_pageTwo == null)
        {
            m_pageTwo = transform.Find("Page_Two").gameObject;
        }

        if (m_fadeImage == null)
        {
            m_fadeImage = transform.Find("Fade_Image").GetComponent<Image>();
        }

        m_pageTwo.SetActive(false);
    }

    private void Update()
    {
        if (m_bShowPageOne && !m_bPageOneShown)
        {
            if (FadeIn(m_fadeImage, m_fFadeSpeed))
            {
                m_bPageOneShown = true;
            }
        }

        if (m_bShowPageOne && Input.anyKeyDown)
        {
            m_bShowPageOne = false;
            m_bPageOneShown = true;
        }

        if (!m_bShowPageOne && m_bPageOneShown)
        {
            if (FadeOut(m_fadeImage, m_fFadeSpeed))
            {
                m_bPageOneShown = false;
                m_bShowPageTwo = true;
            }
        }

        if (m_bShowPageTwo && !m_bPageTwoShown)
        {
            if (!m_bPagesSwitched)
            {
                m_bPagesSwitched = true;
                m_pageOne.SetActive(false);
                m_pageTwo.SetActive(true);
            }

            if (FadeIn(m_fadeImage, m_fFadeSpeed))
            {
                m_bPageTwoShown = true;
            }
        }

        if (m_bShowPageTwo && Input.anyKeyDown)
        {
            m_bShowPageTwo = false;
            m_bPageTwoShown = true;
        }

        if (!m_bShowPageTwo && m_bPageTwoShown)
        {
            if (FadeOut(m_fadeImage, m_fFadeSpeed))
            {
                LevelManager.m_levelManager.LoadNextLevelAsyncOperation.allowSceneActivation = true;
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
}
