using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : Perk
{
    public int m_damageIncrease = 1;
    private Player m_player;
    // Use this for initialization
    void Start()
    {
        this.m_id = PerkID.IncreaseDamage;
        
        m_player = GameObject.FindObjectOfType<Player>();
    }


    public override void Activate()
    {
        if(m_player != null)
        {
            m_player.m_currDamage += m_damageIncrease;
        }
        
    }
}
