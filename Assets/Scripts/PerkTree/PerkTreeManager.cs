using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkTreeManager : MonoBehaviour
{
    private uint m_uiAvailiablePerks = 0;
    public uint AvailiablePerks { get { return m_uiAvailiablePerks; } }

    private int m_iPerkTreeIndex = 1;

    private float m_fInputBuffer = 0.2f;

    private bool m_bInputRecieved = false;
    private bool m_bPerkTreeIsSelected = false;

    private Image m_backgroundImage = null;

    [Header("Perk Tree Button Max & Min Indexes")]
    public int m_iMinimumIndex;
    public int m_iMaximumIndex;

    [Header("Selected Perk Tree")]
    public PerkTreeButton m_selectedPerkTreeButton;

    [Header("Selected Perk Button")]
    public PerkButton m_selectedPerkButton;

    [HideInInspector]
    public Player m_player;

    public static PerkTreeManager m_perkTreeManager;

    private void Awake()
    {
        m_selectedPerkButton.IsHighlighted = true;

        m_backgroundImage = transform.Find("Background_Panel").GetComponent<Image>();
        Color newColor = m_backgroundImage.color;
        newColor.a = 1.0f;
        m_backgroundImage.color = newColor;

        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (m_perkTreeManager == null)
        {
            m_perkTreeManager = this;
        }
        else if (m_perkTreeManager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (InputManager.AButton())
        {
            if (m_selectedPerkButton.IsHighlighted)
            {
                m_selectedPerkButton.OnClick();
            }
            else
            {
                m_bPerkTreeIsSelected = false;
                m_selectedPerkButton.m_backButton.GetComponent<PerkTreeBackButton>().IsHightlighted = false;
                m_selectedPerkButton.m_backButton.GetComponent<PerkTreeBackButton>().OnClick();
            }
        }

        NavigatePerkTree();
    }

    private void NavigatePerkTree()
    {
        Vector3 v3PrimaryInputDirection = InputManager.PrimaryInput();

        // Forward.
        if (v3PrimaryInputDirection.z >= m_fInputBuffer)
        {
            if (!m_selectedPerkButton.IsHighlighted)
            {
                m_bInputRecieved = true;
                m_selectedPerkButton.IsHighlighted = true;
                m_selectedPerkButton.m_backButton.GetComponent<PerkTreeBackButton>().IsHightlighted = false;
            }

            // If there is only one child perk, make it selected.
            if (m_selectedPerkButton.m_childPerks.Count == 1 && !m_bInputRecieved)
            {
                m_bInputRecieved = true;
                m_selectedPerkButton.IsHighlighted = false;
                m_selectedPerkButton = m_selectedPerkButton.m_childPerks[0].GetComponent<PerkButton>();
                m_selectedPerkButton.IsHighlighted = true;
            }

            // Forward & Left.
            if (v3PrimaryInputDirection.x <= -m_fInputBuffer)
            {
                if (!m_bInputRecieved)
                {
                    if (m_selectedPerkButton.m_childPerks[0].transform.position.x < m_selectedPerkButton.m_childPerks[1].transform.position.x)
                    {
                        m_bInputRecieved = true;
                        m_selectedPerkButton.IsHighlighted = false;
                        m_selectedPerkButton = m_selectedPerkButton.m_childPerks[0].GetComponent<PerkButton>();
                        m_selectedPerkButton.IsHighlighted = true;
                    }
                    else
                    {
                        m_bInputRecieved = true;
                        m_selectedPerkButton.IsHighlighted = false;
                        m_selectedPerkButton = m_selectedPerkButton.m_childPerks[1].GetComponent<PerkButton>();
                        m_selectedPerkButton.IsHighlighted = true;
                    }
                }
            }
            // Forward & Right.
            else if (v3PrimaryInputDirection.x >= m_fInputBuffer)
            {
                if (!m_bInputRecieved)
                {
                    if (m_selectedPerkButton.m_childPerks[0].transform.position.x > m_selectedPerkButton.m_childPerks[1].transform.position.x)
                    {
                        m_bInputRecieved = true;
                        m_selectedPerkButton.IsHighlighted = false;
                        m_selectedPerkButton = m_selectedPerkButton.m_childPerks[0].GetComponent<PerkButton>();
                        m_selectedPerkButton.IsHighlighted = true;
                    }
                    else
                    {
                        m_bInputRecieved = true;
                        m_selectedPerkButton.IsHighlighted = false;
                        m_selectedPerkButton = m_selectedPerkButton.m_childPerks[1].GetComponent<PerkButton>();
                        m_selectedPerkButton.IsHighlighted = true;
                    }
                }
            }
        }
        // Backward.
        else if (v3PrimaryInputDirection.z <= -m_fInputBuffer)
        {
            if (m_selectedPerkButton.m_parentPerk != null && !m_bInputRecieved)
            {
                m_bInputRecieved = true;
                m_selectedPerkButton.IsHighlighted = false;
                m_selectedPerkButton = m_selectedPerkButton.m_parentPerk.GetComponent<PerkButton>();
                m_selectedPerkButton.IsHighlighted = true;
            }
            else if (m_selectedPerkButton.m_parentPerk == null && !m_bInputRecieved)
            {
                m_bInputRecieved = true;
                m_selectedPerkButton.IsHighlighted = false;
                m_selectedPerkButton.m_backButton.GetComponent<PerkTreeBackButton>().IsHightlighted = true;
            }
        }
        else
        {
            m_bInputRecieved = false;
        }
    }

    public void IncrementAvailiablePerks()
    {
        ++m_uiAvailiablePerks;
    }

    public void DecrementAvailiablePerks()
    {
        --m_uiAvailiablePerks;
    }
}
