using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodLightningPerk : Perk
{
    private Player m_player;

    // Use this for initialization
    void Start()
    {
        m_player = GameObject.FindObjectOfType<Player>();
    }


    public override void Activate()
    {
        m_player.m_perks.Add(PerkID.GodBolt);
    }
}
