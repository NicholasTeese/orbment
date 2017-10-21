using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    private int m_iSelectedButtonIndex = 0;

    private float m_fInputBuffer = 0.2f;

    private bool m_bInputRecieved = false;

    private GameObject m_optionsMainPanel;
    private GameObject m_optionsAudioPanel;
    private GameObject m_optionsGeneralPanel;

    private AudioSource m_audioSource;

    private BaseButton m_selectedButton;

    // Main panel buttons.
    private List<BaseButton> m_lMainPanelButtons = new List<BaseButton>();
    
    private List<BaseButton> m_lOptionsMainPanelButtons = new List<BaseButton>();
    private List<BaseButton> m_lOptionsAudioPanelButtons = new List<BaseButton>();
    private List<BaseButton> m_lOptionsGeneralPanelButtons = new List<BaseButton>();
    // Quit panel buttons.
    private List<BaseButton> m_lQuitToMainMenuPanelButtons = new List<BaseButton>();
    private List<BaseButton> m_lQuitToDesktopPanelButtons = new List<BaseButton>();

    private List<BaseButton> m_lActivePanelButtons = new List<BaseButton>();

    // Variable getters and setters.
    public int SelectedButtonIndex { get { return m_iSelectedButtonIndex; } set { m_iSelectedButtonIndex = value; } }

    public GameObject OptionsMainPanel { get { return m_optionsMainPanel; } }
    public GameObject OptionsAudioPanel { get { return m_optionsAudioPanel; } }
    public GameObject OptionsGeneralPanel { get { return m_optionsGeneralPanel; } }

    public AudioSource PauseMenuAudioSource { get { return m_audioSource; } }

    public BaseButton SelectedButton { get { return m_selectedButton; } set { m_selectedButton = value; } }

    // Main panel buttons.
    public List<BaseButton> MainPanelButtons { get { return m_lMainPanelButtons; } }
    // Options panel buttons.
    public List<BaseButton> OptionsMainPanelButtons { get { return m_lOptionsMainPanelButtons; } }
    public List<BaseButton> OptionsAudioPanelButtons { get { return m_lOptionsAudioPanelButtons; } }
    public List<BaseButton> OptionsGeneralPanelButtons { get { return m_lOptionsGeneralPanelButtons; } }
    // Quit panel buttons.
    public List<BaseButton> QuitToMainMenuPanelButtons { get { return m_lQuitToMainMenuPanelButtons; } }
    public List<BaseButton> QuitToDesktopPanelButtons { get { return m_lQuitToDesktopPanelButtons; } }
    // Active panel buttons.
    public List<BaseButton> ActivePanelButtons { get { return m_lActivePanelButtons; } set { m_lActivePanelButtons = value; } }

    [Header("Pause Menu Panels")]
    public GameObject m_mainPanel;
    public GameObject m_optionsPanel;
    public GameObject m_quitToMainMenuPanel;
    public GameObject m_quitToDesktopPanel;

    public static PauseMenuManager m_pauseMenuManager;

    private void Awake()
    {
        if (m_pauseMenuManager == null)
        {
            m_pauseMenuManager = this;
        }
        else if (m_pauseMenuManager != this)
        {
            Destroy(gameObject);
        }

        m_optionsMainPanel = m_optionsPanel.transform.Find("Main_Options_Panel").gameObject;
        m_optionsAudioPanel = m_optionsPanel.transform.Find("Audio_Options_Panel").gameObject;
        m_optionsGeneralPanel = m_optionsPanel.transform.Find("General_Options_Panel").gameObject;

        m_audioSource = transform.GetComponentInParent<AudioSource>();

        InitialiseButtons();

        m_mainPanel.SetActive(true);
        m_optionsPanel.SetActive(false);
        m_quitToMainMenuPanel.SetActive(false);
        m_quitToDesktopPanel.SetActive(false);

        m_lActivePanelButtons = m_lMainPanelButtons;
        m_selectedButton = m_lActivePanelButtons[0];
        m_selectedButton.IsMousedOver = true;
    }

    private void Update()
    {
        if (InputManager.AButton())
        {
            m_selectedButton.OnClick(m_selectedButton.m_strOnClickParameter);
        }

        Vector3 v3PrimaryInputDirection = InputManager.PrimaryInput();
        NavigateButtons(v3PrimaryInputDirection, m_lActivePanelButtons);
    }

    private void InitialiseButtons()
    {
        int iParentListIndex = 0;

        // Main panel.
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

        // Options panel.
        // Options main panel.
        foreach (Transform button in m_optionsMainPanel.transform)
        {
            if (button.CompareTag("Button"))
            {
                m_lOptionsMainPanelButtons.Add(button.GetComponent<BaseButton>());
                button.GetComponent<BaseButton>().ParentListIndex = iParentListIndex;
                ++iParentListIndex;
            }
        }
        iParentListIndex = 0;

        // Options audio panel.
        foreach (Transform button in m_optionsAudioPanel.transform)
        {
            if (button.CompareTag("Button"))
            {
                m_lOptionsAudioPanelButtons.Add(button.GetComponent<BaseButton>());
                m_lOptionsAudioPanelButtons[iParentListIndex].ParentListIndex = iParentListIndex;
                ++iParentListIndex;
            }
        }
        iParentListIndex = 0;

        // Options general panel.
        foreach (Transform button in m_optionsGeneralPanel.transform)
        {
            if (button.CompareTag("Button"))
            {
                m_lOptionsGeneralPanelButtons.Add(button.GetComponent<BaseButton>());
                m_lOptionsGeneralPanelButtons[iParentListIndex].ParentListIndex = iParentListIndex;
                ++iParentListIndex;
            }
        }
        iParentListIndex = 0;

        // Quit to main menu panel.
        foreach (Transform button in m_quitToMainMenuPanel.transform)
        {
            if (button.CompareTag("Button"))
            {
                m_lQuitToMainMenuPanelButtons.Add(button.GetComponent<BaseButton>());
                button.GetComponent<BaseButton>().ParentListIndex = iParentListIndex;
                ++iParentListIndex;
            }
        }
        iParentListIndex = 0;

        // Quit to desktop panel.
        foreach (Transform button in m_quitToDesktopPanel.transform)
        {
            if (button.CompareTag("Button"))
            {
                m_lQuitToDesktopPanelButtons.Add(button.GetComponent<BaseButton>());
                button.GetComponent<BaseButton>().ParentListIndex = iParentListIndex;
                ++iParentListIndex;
            }
        }
        iParentListIndex = 0;
    }

    private void NavigateButtons(Vector3 a_v3PrimaryInputDirection, List<BaseButton> a_lButtons)
    {
        if (a_v3PrimaryInputDirection.x >= m_fInputBuffer)
        {
            if (!m_bInputRecieved)
            {
                m_bInputRecieved = true;

                if (m_selectedButton.VolumeSlider != null)
                {
                    m_selectedButton.VolumeSlider.value += 0.1f;
                }
            }
        }
        else if (a_v3PrimaryInputDirection.x <= -m_fInputBuffer)
        {
            if (!m_bInputRecieved)
            {
                m_bInputRecieved = true;

                if (m_selectedButton.VolumeSlider != null)
                {
                    m_selectedButton.VolumeSlider.value -= 0.1f;
                }
            }
        }
        else if (a_v3PrimaryInputDirection.z >= m_fInputBuffer)
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
