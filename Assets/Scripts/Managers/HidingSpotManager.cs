using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotManager : MonoBehaviour
{
    public static HidingSpotManager m_hidingSpotManager;

    private void Awake()
    {
        if (m_hidingSpotManager == null)
        {
            m_hidingSpotManager = this;
        }
        else if (m_hidingSpotManager != this)
        {
            Destroy(gameObject);
        }
    }
}
