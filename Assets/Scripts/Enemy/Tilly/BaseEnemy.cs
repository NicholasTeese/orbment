using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemy : MonoBehaviour
{
    // Level info.
    protected float m_fCurrentLevel = 1.0f;
    protected float m_fHealthPerLevel = 50.0f;
    protected float m_fDamagePerLevel = 5.0f;
    protected float m_fExperiencePerLevel = 20.0f;
    protected float m_fExperienceValue = 10.0f;
    // Health info.
    protected float m_fHealthBarWidth = 100.0f;
    protected float m_fMaxHealth = 100.0f;
    protected float m_fCurrentHealth = 100.0f;
    protected float m_fOldHealth = 0.0f;
    protected float m_fRecentDamageTaken = 0.0f;
    // Damage info.
    protected float m_fCurrentDamage = 10.0f;
    protected float m_fDamage = 10.0f;
    protected float m_fDamageMultiplier = 1.0f;
    // Speed info.
    protected float m_fCurrentSpeed = 10.0f;
    protected float m_fOriginalMoveSpeed = 0.0f;
    protected float m_fSpeedMultiplier = 1.0f;

    protected bool m_bIsAlive = false;
    // Debuffs.
    protected bool m_bIsOnFire = false;
    protected bool m_bIsSlowed = false;
    protected bool m_bIsStunned = false;
    protected bool m_bIsBuffed = false;
    protected bool m_bHasBeedCritted = false;

    protected NavMeshAgent m_navMeshAgent;

    public Texture m_fullHealthBarTexture;
    public Texture m_emptyHealthBarTexutre;

    private void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();

        m_fOriginalMoveSpeed = m_navMeshAgent.speed;
    }

    protected virtual void Update()
    {
        HealthUpdate();
    }

    private void LevelUpdate()
    {
        // Health scaling.
        m_fCurrentHealth = m_fCurrentLevel * m_fHealthPerLevel;
        m_fMaxHealth = m_fCurrentHealth;
        // Damage scaling.
        m_fCurrentDamage = m_fCurrentLevel * m_fDamagePerLevel;
        // Experience scaling.
        m_fExperienceValue = m_fCurrentLevel * m_fExperiencePerLevel;
    }

    private void HealthUpdate()
    {
        // If there was a health change.
        if (m_fOldHealth != m_fCurrentHealth)
        {
            Color textureColor = Color.white;

            // If the health change was from critical damage.
            if (m_bHasBeedCritted)
            {
                textureColor = Color.red;
                m_bHasBeedCritted = false;
            }

            // If the health change was gaining health.
            if (m_fCurrentHealth > m_fOldHealth)
            {
                textureColor = Color.green;
            }

            // Display health change.
            DamageNumberManager.m_damageNumbersManager.CreateDamageNumber(Mathf.Abs(m_fOldHealth - m_fCurrentHealth).ToString(), transform, textureColor);
        }

        // Clamp health to max health.
        if (m_fCurrentHealth > m_fMaxHealth)
        {
            m_fCurrentHealth = m_fMaxHealth;
        }

        // Handle death.
        if (m_fCurrentHealth <= 0.0f)
        {
            m_bIsAlive = false;
            ExplosionManager.m_explosionManager.RequestExplosion(transform.position, transform.forward, Explosion.ExplosionType.BigBlood, 0.0f);
            ExplosionManager.m_explosionManager.RequestExplosion(transform.position, transform.forward, Explosion.ExplosionType.Gibs, 0.0f);
            gameObject.SetActive(false);
        }
    }
}
