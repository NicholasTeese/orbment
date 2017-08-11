using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stunned : StatusEffect
{
    public float m_lifetime = 2.0f;
 
    private float m_timer = 0.0f;
    private NavMeshAgent m_agent;
    private EnemyAttack m_enemyAttack;
    private EnemyShoot m_enemyShoot;

    // Use this for initialization
    void Start()
    {
        this.m_type = Status.Stunned;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (m_entity != null)
        {
            m_entity.m_isStunned = true;
        }

        m_agent = this.GetComponentInParent<NavMeshAgent>();

        m_enemyAttack = this.GetComponentInParent<EnemyAttack>();
        m_enemyShoot = this.GetComponentInParent<EnemyShoot>();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        m_timer += Time.deltaTime;

        if (m_agent != null)
        {
            m_agent.speed = 0.0f;
            if (m_enemyAttack != null)
            {
                m_enemyAttack.m_CanAttack = false;
            }
            if(m_enemyShoot != null)
            {
                m_enemyShoot.m_canAttack = false;
            }
        }

        if (m_timer >= m_lifetime)
        {
            if (m_entity != null)
            {
                m_entity.m_isStunned = false;
            }

            if (m_agent != null && m_entity != null)
            {
                m_agent.speed = m_entity.m_originalMoveSpeed;
            }

            if (m_enemyAttack != null)
            {
                m_enemyAttack.m_CanAttack = true;
            }

            if (m_enemyShoot != null)
            {
                m_enemyShoot.m_canAttack = true;
            }



            ReturnToSender();

            m_timer = 0.0f;
        }
    }
}
