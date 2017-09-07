using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkTreeManager : MonoBehaviour
{
    private uint m_uiAvailiablePerks = 0;
    public uint AvailiablePerks { get { return m_uiAvailiablePerks; } }

    private float m_fInputBuffer = 0.2f;

    private bool m_bIsPressed = false;

    public PerkButton m_selectedPerk;

    [HideInInspector]
    public Player m_player;

    public static PerkTreeManager m_perkTreeManager;

    private void Awake()
    {
        m_selectedPerk.IsHighLighted = true;

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
            m_selectedPerk.OnClick();
        }

        Vector3 v3PrimaryInputDirection = InputManager.PrimaryInputHold();

        // Forward.
        if (v3PrimaryInputDirection.z >= m_fInputBuffer)
        {
            // If there is only one child perk, make it selected.
            if (m_selectedPerk.m_childPerks.Count == 0 && !m_bIsPressed)
            {
                m_bIsPressed = true;
                m_selectedPerk.IsHighLighted = false;
                m_selectedPerk = m_selectedPerk.m_childPerks[0].GetComponent<PerkButton>();
                m_selectedPerk.IsHighLighted = true;
            }

            // Forward & Left.
            if (v3PrimaryInputDirection.x <= -m_fInputBuffer)
            {
                if (!m_bIsPressed)
                {
                    m_bIsPressed = true;
                    if (m_selectedPerk.m_childPerks[0].transform.position.x < m_selectedPerk.m_childPerks[1].transform.position.x)
                    {

                    }
                }
            }
            // Forward & Right.
            else if (v3PrimaryInputDirection.x >= m_fInputBuffer)
            {
                if (!m_bIsPressed)
                {
                    m_bIsPressed = true;
                }
            }
        }
        // Backward.
        else if (v3PrimaryInputDirection.z <= -m_fInputBuffer)
        {
            if (m_selectedPerk.m_parentPerk != null)
            {
                m_selectedPerk.IsHighLighted = false;
                m_selectedPerk = m_selectedPerk.m_parentPerk.GetComponent<PerkButton>();
                m_selectedPerk.IsHighLighted = true;
            }
        }
        else
        {
            m_bIsPressed = false;
        }

        //x if (InputManager.PrimaryInputDown() != Vector3.zero)
        //x {
        //x     if (InputManager.PrimaryInputHold().z > 0.5f)
        //x     {
        //x         if (m_selectedPerk.m_childPerks.Count == 0)
        //x         {
        //x             return;
        //x         }
        //x 
        //x         if (m_selectedPerk.m_childPerks.Count == 1)
        //x         {
        //x             m_selectedPerk.IsHighLighted = false;
        //x             m_selectedPerk = m_selectedPerk.m_childPerks[0].GetComponent<PerkButton>();
        //x             m_selectedPerk.IsHighLighted = true;
        //x         }
        //x         else if (InputManager.PrimaryHorizontal() < 0.0f)
        //x         {
        //x             if (m_selectedPerk.m_childPerks[0].transform.position.x < m_selectedPerk.m_childPerks[1].transform.position.x)
        //x             {
        //x                 m_selectedPerk.IsHighLighted = false;
        //x                 m_selectedPerk = m_selectedPerk.m_childPerks[0].GetComponent<PerkButton>();
        //x                 m_selectedPerk.IsHighLighted = true;
        //x             }
        //x             else
        //x             {
        //x                 m_selectedPerk.IsHighLighted = false;
        //x                 m_selectedPerk = m_selectedPerk.m_childPerks[1].GetComponent<PerkButton>();
        //x                 m_selectedPerk.IsHighLighted = true;
        //x             }
        //x         }
        //x         else
        //x         {
        //x             if (m_selectedPerk.m_childPerks[0].transform.position.x > m_selectedPerk.m_childPerks[1].transform.position.x)
        //x             {
        //x                 m_selectedPerk.IsHighLighted = false;
        //x                 m_selectedPerk = m_selectedPerk.m_childPerks[0].GetComponent<PerkButton>();
        //x                 m_selectedPerk.IsHighLighted = true;
        //x             }
        //x             else
        //x             {
        //x                 m_selectedPerk.IsHighLighted = false;
        //x                 m_selectedPerk = m_selectedPerk.m_childPerks[1].GetComponent<PerkButton>();
        //x                 m_selectedPerk.IsHighLighted = true;
        //x             }
        //x         }
        //x     }
        //x     else if (InputManager.PrimaryInputHold().z < 0.0f && m_selectedPerk.m_parentPerk != null)
        //x     {
        //x         m_selectedPerk.IsHighLighted = false;
        //x         m_selectedPerk = m_selectedPerk.m_parentPerk.GetComponent<PerkButton>();
        //x         m_selectedPerk.IsHighLighted = true;
        //x     }
        //x }

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
