using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeProjectile: Perk
{
    public GameObject m_projectile;
    public float m_firingInterval = 0.1f;
    public float m_shootManaCost = 10.0f;
    private Player m_player;
    private BaseWeapon m_playerWeapon;
    // Use this for initialization
    void Start()
    {
        m_player = GameObject.FindObjectOfType<Player>();

        if(m_player != null)
        {
            m_playerWeapon = m_player.m_currWeapon;
        }
        
    }

    public override void Activate()
    {
       if(m_playerWeapon != null && m_projectile != null)
        {
            m_player.m_currentProjectile = m_projectile;
            m_player.m_playerFiringInterval = m_firingInterval;
            m_player.m_shootManaCost = m_shootManaCost;
            m_playerWeapon.SetProjectile(m_projectile);
        }
    }
}
