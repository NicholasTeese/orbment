using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFiringInterval : Perk {

    public float m_firingInterval = 0.1f;
    private Player m_player;
    
    // Use this for initialization
    void Start()
    {
        m_player = GameObject.FindObjectOfType<Player>();


    }

    public override void Activate()
    {
        m_player.m_playerFiringInterval = m_firingInterval;
    }
}
