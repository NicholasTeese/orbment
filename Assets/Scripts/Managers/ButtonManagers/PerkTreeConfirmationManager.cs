using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkTreeConfirmationManager : MonoBehaviour
{
    private int m_iSelectedButtonIndex = 0;
    public int SelectedButtonIndex { get { return m_iSelectedButtonIndex; } set { m_iSelectedButtonIndex = value; } }

    private float m_fInputBuffer = 0.2f;

    private bool m_bInputRecieved = false;

    private BaseButton m_selectedButton;
    public BaseButton SelectedButton { get { return m_selectedButton; } set { m_selectedButton = value; } }

    private List<BaseButton> m_lMainPanelButtons = new List<BaseButton>();
    public List<BaseButton> MainPanelButtons { get { return m_lMainPanelButtons; } }

    public static PerkTreeConfirmationManager m_perkTreeConfirmationManager;

    private void Awake()
    {
        if (m_perkTreeConfirmationManager == null)
        {
            m_perkTreeConfirmationManager = this;
        }
        else if (m_perkTreeConfirmationManager != this)
        {
            Destroy(gameObject);
        }

        InitialiseButtons();

        m_selectedButton = m_lMainPanelButtons[0];
        m_selectedButton.IsMousedOver = true;
    }

    private void Update()
    {
        if (InputManager.AButton())
        {
            m_selectedButton.OnClick(m_selectedButton.m_strOnClickParameter);
        }

        Vector3 v3PrimaryInputDirection = InputManager.PrimaryInput();
        NavigateButtons(v3PrimaryInputDirection, m_lMainPanelButtons);
    }

    private void InitialiseButtons()
    {
        int iParentListIndex = 0;

        foreach (Transform button in transform)
        {
            if (button.CompareTag("Button"))
            {
                m_lMainPanelButtons.Add(button.GetComponent<BaseButton>());
                button.GetComponent<BaseButton>().ParentListIndex = iParentListIndex;
                ++iParentListIndex;
            }
        }
    }

    private void NavigateButtons(Vector3 a_v3PrimaryInputDirection, List<BaseButton> a_lButtons)
    {
        if (a_v3PrimaryInputDirection.x >= m_fInputBuffer)
        {
            if (!m_bInputRecieved)
            {
                m_bInputRecieved = true;

                if (m_selectedButton == a_lButtons[1])
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
        else if (a_v3PrimaryInputDirection.x <= -m_fInputBuffer)
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
