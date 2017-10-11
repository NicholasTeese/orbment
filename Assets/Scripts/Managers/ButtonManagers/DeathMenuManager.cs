using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuManager : MonoBehaviour
{
    private int m_iSelectedButtonIndex = 0;
    public int SelectedButtonIndex { get { return m_iSelectedButtonIndex; } set { m_iSelectedButtonIndex = value; } }

    private float m_fInputBuffer = 0.2f;

    private bool m_bInputRecieved = false;

    private GameObject m_mainPanel = null;
    private GameObject m_quitToMainMenuPanel = null;
    private GameObject m_quitToDesktopPanel = null;

    private BaseButton m_selectedButton;

    private List<BaseButton> m_lMainPanelButtons = new List<BaseButton>();
    private List<BaseButton> m_lQuitToMainMenuPanelButtons = new List<BaseButton>();
    private List<BaseButton> m_lQuitToDesktopPanelButtons = new List<BaseButton>();
    private List<BaseButton> m_lActivePanelButtons = new List<BaseButton>();

    // Variable getters and setters.
    public GameObject MainPanel { get { return m_mainPanel; } set { m_mainPanel = value; } }
    public GameObject QuitToMainMenuPanel { get { return m_quitToMainMenuPanel; } set { m_quitToMainMenuPanel = value; } }
    public GameObject QuitToDesktopPanel { get { return m_quitToDesktopPanel; } set { m_quitToDesktopPanel = value; } }

    public BaseButton SelectedButton { get { return m_selectedButton; } set { m_selectedButton = value; } }

    public List<BaseButton> MainPanelButtons { get { return m_lMainPanelButtons; } }
    public List<BaseButton> QuitToMainMenuPanelButtons { get { return m_lQuitToMainMenuPanelButtons; } }
    public List<BaseButton> QuitToDesktopPanelButtons { get { return m_lQuitToDesktopPanelButtons; } }
    public List<BaseButton> ActivePanelButtons { get { return m_lActivePanelButtons; } set { m_lActivePanelButtons = value; } }

    public static DeathMenuManager m_deathMenuManager;

    private void Awake()
    {
        if (m_deathMenuManager == null)
        {
            m_deathMenuManager = this;
        }
        else if (m_deathMenuManager != this)
        {
            Destroy(transform.parent.gameObject);
        }

        m_mainPanel = transform.Find("MainPanel").gameObject;
        m_quitToMainMenuPanel = transform.Find("QuitToMainMenuPanel").gameObject;
        m_quitToDesktopPanel = transform.Find("QuitToDesktopPanel").gameObject;

        InitialiseButtons();

        m_mainPanel.SetActive(true);
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
        NaviageButtons(v3PrimaryInputDirection, m_lActivePanelButtons);
    }

    private void InitialiseButtons()
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

    public void ResetSelectedButtonIndex()
    {
        m_iSelectedButtonIndex = 0;
    }
}
