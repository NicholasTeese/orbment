using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfFire : StatusEffect
{
    public int m_ringOfFireDPS = 5;
    public float m_tickInterval = 0.5f;
    private float m_elapsed = 0.0f;
    public float m_healthPercentThreshold = 25.0f;
    //private StatusEffectManager m_statusEffectManager;
    // Use this for initialization

    private Player m_player;

    // Use this for initialization
    void Start()
    {
        this.m_type = Status.FireRing;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (m_entity != null)
        {
            m_entity.m_ringOfFireActive = true;
        }


    }

    void OnDisable()
    {
        if (m_entity != null)
        {
            m_entity.m_ringOfFireActive = false;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {

        base.Update();

        if (m_entity != null)
        {
            if (!m_entity.HealthBelowPercentCheck(m_healthPercentThreshold))
            {
                m_entity.m_ringOfFireActive = false;
                ReturnToSender();
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Enemy"))
        {

            Entity entity = col.GetComponent<Entity>();

            if (entity != null)
            {
                entity.m_setOnFire = true;
            }

        }
    }
}
