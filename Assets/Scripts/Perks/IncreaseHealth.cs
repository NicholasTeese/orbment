using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealth : Perk
{
    public int m_healthIncrease = 10;

    private Player m_player;

    public void Start()
    {
        m_player = GameObject.FindObjectOfType<Player>();



    }

    public override void Activate()
    {
        if(m_player != null)
        {
            m_player.m_currHealth += m_healthIncrease;
        }
    }

}
