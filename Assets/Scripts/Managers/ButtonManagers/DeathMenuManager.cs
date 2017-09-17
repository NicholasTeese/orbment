using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuManager : MonoBehaviour
{
    private BaseButton m_selectedButton;
    public BaseButton SelectedButton { get { return m_selectedButton; } set { m_selectedButton = value; } }

    public static DeathMenuManager m_deathMenuManager;

    private void Awake()
    {
        if (m_deathMenuManager == null)
        {
            m_deathMenuManager = this;
        }
        else if (m_deathMenuManager != this)
        {
            Destroy(gameObject);
        }

        List<BaseButton> lButtons = new List<BaseButton>();

        foreach (Transform child in transform)
        {
            lButtons.Add(child.GetComponent<BaseButton>());
        }

        m_selectedButton = lButtons[0];
        m_selectedButton.IsMousedOver = true;
    }
}
