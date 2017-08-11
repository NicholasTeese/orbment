using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slowed : StatusEffect
{

    public float m_lifetime = 2.0f;
    public float m_slowMult = 0.2f;
    private float m_timer = 0.0f;
    private NavMeshAgent m_agent;
    // Use this for initialization
    void Start()
    {
        this.m_type = Status.Slowed;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (m_entity != null)
        {
            m_entity.m_isSlowed = true;
        }

        m_agent = this.GetComponentInParent<NavMeshAgent>();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        m_timer += Time.deltaTime;

        if (m_agent != null)
        {
            m_agent.speed = m_entity.m_originalMoveSpeed * m_slowMult;
        }

        if (m_timer >= m_lifetime)
        {
            if (m_entity != null)
            {
                m_entity.m_isSlowed = false;
            }

            if (m_agent != null && m_entity != null)
            {
                m_agent.speed = m_entity.m_originalMoveSpeed;
            }

            ReturnToSender();

            m_timer = 0.0f;
        }
    }
}
