using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkTreeManager : MonoBehaviour
{
    private uint m_uiAvailiablePerks = 0;
    public uint AvailiablePerks { get { return m_uiAvailiablePerks; } }

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

        if (InputManager.PrimaryInputDown() != Vector3.zero)
        {
            if (InputManager.PrimaryInputHold().z > 0.5f)
            {
                if (m_selectedPerk.m_childPerks.Count == 0)
                {
                    return;
                }

                if (m_selectedPerk.m_childPerks.Count == 1)
                {
                    m_selectedPerk.IsHighLighted = false;
                    m_selectedPerk = m_selectedPerk.m_childPerks[0].GetComponent<PerkButton>();
                    m_selectedPerk.IsHighLighted = true;
                }
                else if (InputManager.PrimaryHorizontal() < 0.0f)
                {
                    if (m_selectedPerk.m_childPerks[0].transform.position.x < m_selectedPerk.m_childPerks[1].transform.position.x)
                    {
                        m_selectedPerk.IsHighLighted = false;
                        m_selectedPerk = m_selectedPerk.m_childPerks[0].GetComponent<PerkButton>();
                        m_selectedPerk.IsHighLighted = true;
                    }
                    else
                    {
                        m_selectedPerk.IsHighLighted = false;
                        m_selectedPerk = m_selectedPerk.m_childPerks[1].GetComponent<PerkButton>();
                        m_selectedPerk.IsHighLighted = true;
                    }
                }
                else
                {
                    if (m_selectedPerk.m_childPerks[0].transform.position.x > m_selectedPerk.m_childPerks[1].transform.position.x)
                    {
                        m_selectedPerk.IsHighLighted = false;
                        m_selectedPerk = m_selectedPerk.m_childPerks[0].GetComponent<PerkButton>();
                        m_selectedPerk.IsHighLighted = true;
                    }
                    else
                    {
                        m_selectedPerk.IsHighLighted = false;
                        m_selectedPerk = m_selectedPerk.m_childPerks[1].GetComponent<PerkButton>();
                        m_selectedPerk.IsHighLighted = true;
                    }
                }
            }
            else if (InputManager.PrimaryInputHold().z < 0.0f && m_selectedPerk.m_parentPerk != null)
            {
                m_selectedPerk.IsHighLighted = false;
                m_selectedPerk = m_selectedPerk.m_parentPerk.GetComponent<PerkButton>();
                m_selectedPerk.IsHighLighted = true;
            }
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
