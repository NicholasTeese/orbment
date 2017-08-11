using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPerk : Perk
{
    private Player m_player;

    public void Start()
    {
        m_player = GameObject.FindObjectOfType<Player>();

    }

    public override void Activate()
    {
        if (m_player != null)
        {
            m_player.m_perks.Add(PerkID.StunChance);
        }

    }
}
