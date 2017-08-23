using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkTreeManager : MonoBehaviour
{
    private uint m_uiAvailiablePerks = 0;
    public uint AvailiablePerks { get { return m_uiAvailiablePerks; } }

    public static PerkTreeManager m_perkTreeManager;

    private void Awake()
    {
        if (m_perkTreeManager == null)
        {
            m_perkTreeManager = this;
        }
        else if (m_perkTreeManager != this)
        {
            Destroy(gameObject);
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
