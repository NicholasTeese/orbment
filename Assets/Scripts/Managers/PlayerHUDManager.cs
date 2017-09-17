using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDManager : MonoBehaviour
{
    public static PlayerHUDManager m_playerHUDManager;

    private void Awake()
    {
        if (m_playerHUDManager == null)
        {
            m_playerHUDManager = this;
        }
        else if (m_playerHUDManager != this)
        {
            Destroy(gameObject);
        }
    }
}
