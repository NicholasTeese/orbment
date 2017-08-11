using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : Perk
{

    // Use this for initialization
    private GameObject m_currProjectile;
    private Player m_player;
    public BaseWeapon m_weapon;
    // Use this for initialization
    void Start()
    {
        m_player = GameObject.FindObjectOfType<Player>();



    }

    public override void Activate()
    {

        if (m_player != null)
        {
            m_currProjectile = m_player.m_currentProjectile;
        }
        if (m_currProjectile != null )
        {
            m_player.m_currWeapon = m_weapon;
            m_player.m_currWeapon.SetProjectile(m_currProjectile);
        }
    }
}
