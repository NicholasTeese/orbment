using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxMana : Perk
{

    public int m_maxManaIncrease = 100;

    private Player m_player;
    private Mana m_playerMana;

    public void Start()
    {
        m_player = GameObject.FindObjectOfType<Player>();

        if(m_player != null)
        {
            m_playerMana = m_player.GetComponent<Mana>();
        }


    }

    public override void Activate()
    {
        if (m_playerMana != null)
        {
            m_playerMana.m_maxMana += m_maxManaIncrease;
        }
    }
}
