using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMoveSpeed : Perk
{
    public int m_speedIncrease = 1;
    private Player m_player;

    // Use this for initialization
    void Start()
    {
        m_player = GameObject.FindObjectOfType<Player>();
    }

    public override void Activate()
    {
        if(m_player != null)
        {
            m_player.m_currSpeed += m_speedIncrease;
        }
        
    }

}
