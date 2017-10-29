using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDManager : MonoBehaviour
{
    private GameObject m_healthBar;

    public Image m_currentKillStreakImage;

    public static PlayerHUDManager m_playerHUDManager;

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
        m_healthBar = transform.Find("Health_Bar").Find("Health_Bar_Full").gameObject;
    }
}
