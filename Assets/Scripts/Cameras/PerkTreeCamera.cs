using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkTreeCamera : MonoBehaviour
{
    public static PerkTreeCamera m_perkTreeCamera;

    private void Awake()
    {
        if (m_perkTreeCamera == null)
        {
            m_perkTreeCamera = this;
        }
        else if (m_perkTreeCamera != this)
        {
            Destroy(gameObject);
        }
    }
}
