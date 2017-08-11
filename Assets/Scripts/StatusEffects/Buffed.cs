using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffed : StatusEffect
{
    public int m_damageMultplier = 2;
    public float m_firingIntervalDivider = 2.0f;
    public float m_healthPercentThreshold = 10.0f;
    private int m_originalMult = 1;
    private float m_origFireRate;
    private Player m_player;

    // Use this for initialization
    void Start()
    {
        this.m_type = Status.Buffed;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (m_entity != null)
        {
            m_entity.m_isBuffed = true;

            m_player = m_entity.GetComponent<Player>();

            if (m_player != null)
            {
                m_originalMult = m_player.m_currDamageMult;
                m_origFireRate = m_player.m_playerFiringInterval;

                m_player.m_playerFiringInterval = m_player.m_playerFiringInterval / m_firingIntervalDivider;
                m_player.m_currDamageMult = m_damageMultplier;
            }
        }


    }

    void OnDisable()
    {
        if(m_entity != null)
        {
            m_entity.m_isBuffed = false;
        }
        
        if (m_player != null)
        {
            m_player.m_playerFiringInterval = m_origFireRate;
            m_player.m_currDamageMult = m_originalMult;
        }
    }
    // Update is called once per frame
    protected override void Update()
    {

        base.Update();

        if(m_entity != null)
        {
            if(!m_entity.HealthBelowPercentCheck(m_healthPercentThreshold))
            {
                m_entity.m_isBuffed = false;
                ReturnToSender();
            }
        }
    }
}
