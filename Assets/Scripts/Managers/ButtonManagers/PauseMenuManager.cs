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

    List<BaseButton> m_lButtons = new List<BaseButton>();

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

        foreach (Transform child in transform)
        {
            m_lButtons.Add(child.GetComponent<BaseButton>());
        }

        m_selectedButton = m_lButtons[0];
        m_selectedButton.IsMousedOver = true;
    }

    private void Start()
    {
        ValidateInitialisation();
    }

    private void Update()
    {
        if (InputManager.AButton())
        {
            m_selectedButton.OnClick();
        }

        Vector3 v3PrimaryInputDirection = InputManager.PrimaryInput();

        if (v3PrimaryInputDirection.z >= m_fInputBuffer)
        {
            if (!m_bInputRecieved)
            {
                m_bInputRecieved = true;

                if (m_selectedButton == m_lButtons[0])
                {
                    m_selectedButton.IsMousedOver = false;
                    m_selectedButton = m_lButtons[m_lButtons.Count - 1];
                    m_selectedButton.IsMousedOver = true;
                    m_iSelectedButtonIndex = m_lButtons.Count - 1;
                }
                else
                {
                    m_selectedButton.IsMousedOver = false;
                    m_selectedButton = m_lButtons[m_iSelectedButtonIndex - 1];
                    m_selectedButton.IsMousedOver = true;
                    --m_iSelectedButtonIndex;
                }
            }
        }
        else if (v3PrimaryInputDirection.z <= -m_fInputBuffer)
        {
            if (!m_bInputRecieved)
            {
                m_bInputRecieved = true;

                if (m_selectedButton == m_lButtons[m_lButtons.Count - 1])
                {
                    m_selectedButton.IsMousedOver = false;
                    m_selectedButton = m_lButtons[0];
                    m_selectedButton.IsMousedOver = true;
                    m_iSelectedButtonIndex = 0;
                }
                else
                {
                    m_selectedButton.IsMousedOver = false;
                    m_selectedButton = m_lButtons[m_iSelectedButtonIndex + 1];
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

    private void ValidateInitialisation()
    {
        if (m_selectedButton == null)
        {
            Debug.Log("SelectedButton was unable to be initialised.");
        }
    }
}
