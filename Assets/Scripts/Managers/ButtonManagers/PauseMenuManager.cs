using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    private int m_iSelectedButtonIndex = 0;

    private float m_fInputBuffer = 0.2f;

    private bool m_bInputRecieved = false;

    private BaseButton m_selectedButton;
    public BaseButton SelectedButton { get { return m_selectedButton; } set { m_selectedButton = value; } }

    private List<BaseButton> m_lMainPanelButtons = new List<BaseButton>();
    public List<BaseButton> MainPanelButtons { get { return m_lMainPanelButtons; } }
    private List<BaseButton> m_lOptionsPanelButtons = new List<BaseButton>();
    public List<BaseButton> OptionsPanelbuttons { get { return m_lOptionsPanelButtons; } }
    private List<BaseButton> m_lQuitToMainMenuPanelButtons = new List<BaseButton>();
    public List<BaseButton> QuitToMainMenuPanelButtons { get { return m_lQuitToMainMenuPanelButtons; } }
    private List<BaseButton> m_lQuitToDesktopPanelButtons = new List<BaseButton>();
    public List<BaseButton> QuitToDesktopPanelButtons { get { return m_lQuitToDesktopPanelButtons; } }
    private List<BaseButton> m_lActivePanelButtons = new List<BaseButton>();
    public List<BaseButton> ActivePanelButtons { get { return m_lActivePanelButtons; } set { m_lActivePanelButtons = value; } }

    [Header("Pause Menu Panels")]
    public GameObject m_mainPanel;
    public GameObject m_optionsPanel;
    public GameObject m_quitToMainMenuPanel;
    public GameObject m_quitToDesktopPanel;

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
            m_selectedButton.OnClick();
        }

        Vector3 v3PrimaryInputDirection = InputManager.PrimaryInput();
        NaviageButtons(v3PrimaryInputDirection, m_lMainPanelButtons);
    }

    private void InitialiseButtons()
    {
        foreach (GameObject button in m_mainPanel.transform)
        {
            m_lMainPanelButtons.Add(button.GetComponent<BaseButton>());
        }

        foreach (GameObject button in m_optionsPanel.transform)
        {
            m_lOptionsPanelButtons.Add(button.GetComponent<BaseButton>());
        }

        foreach (GameObject button in m_quitToMainMenuPanel.transform)
        {
            m_lQuitToMainMenuPanelButtons.Add(button.GetComponent<BaseButton>());
        }

        foreach (GameObject button in m_quitToDesktopPanel.transform)
        {
            m_lQuitToDesktopPanelButtons.Add(button.GetComponent<BaseButton>());
        }
    }

    private void NaviageButtons(Vector3 a_v3PrimaryInputDirection, List<BaseButton> a_lButtons)
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
}
