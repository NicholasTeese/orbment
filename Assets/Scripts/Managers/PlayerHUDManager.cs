using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDManager : MonoBehaviour
{
    private GameObject m_healthBar = null;

    public static PlayerHUDManager m_playerHUDManager = null;

    public GameObject HealthBar { get { return m_healthBar; } set { m_healthBar = value; } }

    private void Awake()
    {
        if (m_playerHUDManager == null)
        {
            m_playerHUDManager = this;
        }
        else if (m_playerHUDManager != this)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void Start()
    {
        m_healthBar = transform.Find("Health_Bar").gameObject;
    }
}
