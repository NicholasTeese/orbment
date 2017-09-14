using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public static BombManager m_bombManager;

    private void Awake()
    {
        if (m_bombManager == null)
        {
            m_bombManager = this;
        }
        else if (m_bombManager != this)
        {
            Destroy(gameObject);
        }
    }
}
