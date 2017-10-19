using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private int m_iSelectedButtonIndex = 0;
    public int SelectedButtonIndex { get { return m_iSelectedButtonIndex; } set { m_iSelectedButtonIndex = value; } }

    private float m_fInputBuffer = 0.2f;

    private bool m_bFadeIn = true;
    private bool m_bInputRecieved = false;

    private AudioSource m_audioSource;
    public AudioSource MainMenuAudioSource { get { return m_audioSource; } }

    private Image m_fadeImage;

    private BaseButton m_selectedButton;
    public BaseButton SelectedButton { get { return m_selectedButton; } set { m_selectedButton = value; } }

    private List<BaseButton> m_lMainPanelButtons = new List<BaseButton>();
    private List<BaseButton> m_lOptionsPanelButtons = new List<BaseButton>();
    private List<BaseButton> m_lQuitToDesktopPanelButtons = new List<BaseButton>();
    private List<BaseButton> m_lActivePanelButtons = new List<BaseButton>();
   
    public List<BaseButton> MainPanelButtons { get { return m_lMainPanelButtons; } }
    public List<BaseButton> OptionsPanelButtons { get { return m_lOptionsPanelButtons; } set { m_lOptionsPanelButtons = value; } }
    public List<BaseButton> QuitToDesktopPanelButtons { get { return m_lQuitToDesktopPanelButtons; } }
    public List<BaseButton> ActivePanelButtons { get { return m_lActivePanelButtons; } set { m_lActivePanelButtons = value; } }

    [Header("Pause Menu Panels")]
    public GameObject m_mainPanel;
    public GameObject m_optionsPanel;
    public GameObject m_quitToDesktopPanel;

    public static MainMenuManager m_mainMenuManager;

    private void Awake()
    {
        if (m_mainMenuManager == null)
        {
            m_mainMenuManager = this;
        }
        else if (m_mainMenuManager != this)
        {
            Destroy(gameObject);
        }

        m_audioSource = GetComponent<AudioSource>();

        //m_fadeImage = transform.Find("Fade_Image").GetComponent<Image>();

        IntitialiseButtons();

        m_mainPanel.SetActive(true);
        m_optionsPanel.SetActive(false);
        m_quitToDesktopPanel.SetActive(false);

        m_lActivePanelButtons = m_lMainPanelButtons;
        m_selectedButton = m_lActivePanelButtons[0];
        m_selectedButton.IsMousedOver = true;
    }

    private void Start()
    {
        if (GameManager.m_gameManager.ShowCursor)
        {
            m_optionsPanel.transform.Find("Show_Hide_Cursor_Button").GetComponentInChildren<Text>().text = "Hide Cursor";
        }
        else
        {
            m_optionsPanel.transform.Find("Show_Hide_Cursor_Button").GetComponentInChildren<Text>().text = "Show Cursor";
        }
    }

    private void Update()
    {
        if (m_bFadeIn)
        {
            //ImageFadeOut();
        }
        else
        {
            //ImageFadeIn();
        }

        if (InputManager.AButton())
        {
            m_selectedButton.OnClick(m_selectedButton.m_strOnClickParameter);
        }

        Vector3 v3PrimaryInputDirection = InputManager.PrimaryInput();
        NavigateButtons(v3PrimaryInputDirection, m_lActivePanelButtons);
    }

    private void ImageFadeOut(Image a_fadeImage, float a_fFadeSpeed)
    {

    }

    private void ImageFadeIn(Image a_fadeImage, float a_fFadeSpeed)
    {
        //if (a_fadeImage.color.a > 0.0f)
        //{
        //    a_fadeImage.GetComponent<Color>.a -= a_fFadeSpeed * Time.deltaTime;
        //}
    }

    private void IntitialiseButtons()
    {
        int iParentListIndex = 0;

        foreach (Transform button in m_mainPanel.transform)
        {
            if (button.CompareTag("Button"))
            {
                m_lMainPanelButtons.Add(button.GetComponent<BaseButton>());
                button.GetComponent<BaseButton>().ParentListIndex = iParentListIndex;
                ++iParentListIndex;
            }
        }
        iParentListIndex = 0;

        foreach (Transform button in m_optionsPanel.transform)
        {
            if (button.CompareTag("Button"))
            {
                m_lOptionsPanelButtons.Add(button.GetComponent<BaseButton>());
                button.GetComponent<BaseButton>().ParentListIndex = iParentListIndex;
                ++iParentListIndex;
            }
        }
        iParentListIndex = 0;

        foreach (Transform button in m_quitToDesktopPanel.transform)
        {
            if (button.CompareTag("Button"))
            {
                m_lQuitToDesktopPanelButtons.Add(button.GetComponent<BaseButton>());
                button.GetComponent<BaseButton>().ParentListIndex = iParentListIndex;
                ++iParentListIndex;
            }
        }
    }

    private void NavigateButtons(Vector3 a_v3PrimaryInputDirection, List<BaseButton> a_lButtons)
    {
        if (a_v3PrimaryInputDirection.z >= m_fInputBuffer)
        {
            if (!m_bInputRecieved)
            {
                m_bInputRecieved = true;

                if (m_selectedButton == a_lButtons[0])
                {
                    m_selectedButton.IsMousedOver = false;
                    m_selectedButton = a_lButtons[a_lButtons.Count - 1];
                    m_selectedButton.IsMousedOver = true;
                    m_iSelectedButtonIndex = a_lButtons.Count - 1;
                }
                else
                {
                    m_selectedButton.IsMousedOver = false;
                    m_selectedButton = a_lButtons[m_iSelectedButtonIndex - 1];
                    m_selectedButton.IsMousedOver = true;
                    --m_iSelectedButtonIndex;
                }
            }
        }
        else if (a_v3PrimaryInputDirection.z <= -m_fInputBuffer)
        {
            if (!m_bInputRecieved)
            {
                m_bInputRecieved = true;

                if (m_selectedButton == a_lButtons[a_lButtons.Count - 1])
                {
                    m_selectedButton.IsMousedOver = false;
                    m_selectedButton = a_lButtons[0];
                    m_selectedButton.IsMousedOver = true;
                    m_iSelectedButtonIndex = 0;
                }
                else
                {
                    m_selectedButton.IsMousedOver = false;
                    m_selectedButton = a_lButtons[m_iSelectedButtonIndex + 1];
                    m_selectedButton.IsMousedOver = true;
                    ++m_iSelectedButtonIndex;
                }
            }
        }
        else
        {
            m_bInputRecieved = false;
        }
    }

    public void ResetSelectedButtonIndex()
    {
        m_iSelectedButtonIndex = 0;
    }
}
