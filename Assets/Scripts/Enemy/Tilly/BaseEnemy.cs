using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemy : MonoBehaviour
{
    protected enum EnemyType
    {
        GREMLIN,
        GOBLIN,
        ORC
    }

    // Level info.
    protected float m_fCurrentLevel = 1.0f;
    protected float m_fHealthPerLevel = 50.0f;
    protected float m_fDamagePerLevel = 5.0f;
    protected float m_fExperiencePerLevel = 20.0f;
    protected float m_fExperienceGivenOnDeath = 10.0f;
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
    // Debuff triggers.
    protected bool m_bBurn = false;
    protected bool m_bSlow = false;
    protected bool m_bStun = false;
    // Debuffs.
    protected bool m_bIsBuring = false;
    protected bool m_bIsSlowed = false;
    protected bool m_bIsStunned = false;
    protected bool m_bIsCritted = false;

    protected Vector3 m_v3TravelDirection = Vector3.zero;

    protected EnemyType m_eEnemyType;

    protected Collider m_collider;

    protected NavMeshAgent m_navMeshAgent;

    public Animator m_animator;

    public Texture m_fullHealthBarTexture;
    public Texture m_emptyHealthBarTexutre;

    /// <summary>
    /// Initialise BaseEnemy.
    /// </summary>
    protected virtual void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();

        m_fOriginalMoveSpeed = m_navMeshAgent.speed;

        CheckLevel();
    }

    /// <summary>
    /// Update BaseEnemy logic, call once per frame.
    /// </summary>
    protected virtual void Update()
    {
        // If can't be seen by the camera don't update.
        if (!CanBeSeen(IsoCam.m_playerCamera.FrustrumPlanes, m_collider))
        {
            return;
        }

        CheckHealth();
    }

    protected virtual void OnGUI()
    {
        if (ExpManager.m_experiencePointsManager.PerkTreeOpen || GameManager.m_gameManager.GameIsPaused)
        {
            return;
        }

        Vector2 v2ScreenPoint = IsoCam.m_playerCamera.MainPlaterCamera.WorldToScreenPoint(transform.position);

        if (m_fCurrentHealth != m_fMaxHealth)
        {
            GUI.DrawTexture(new Rect(v2ScreenPoint.x - 0.5f * m_fHealthBarWidth, Screen.height - v2ScreenPoint.y - 40.0f, m_fHealthBarWidth, 10.0f), m_emptyHealthBarTexutre);
            GUI.DrawTexture(new Rect(v2ScreenPoint.x - 0.5f * m_fHealthBarWidth, Screen.height - v2ScreenPoint.y - 40.0f, m_fHealthBarWidth * m_fCurrentHealth / m_fMaxHealth, 10.0f), m_fullHealthBarTexture);
        }
        else
        {
            GUI.Label(new Rect(v2ScreenPoint.x - 0.5f * m_fHealthBarWidth, Screen.height - v2ScreenPoint.y - 60.0f, m_fHealthBarWidth, 50.0f), "Lvl " + m_fCurrentLevel);
        }
    }

    protected virtual bool CanBeSeen(Plane[] a_frustrumPlanes, Collider a_collider)
    {
        if (GeometryUtility.TestPlanesAABB(a_frustrumPlanes, a_collider.bounds))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Check values based on level.
    /// </summary>
    protected virtual void CheckLevel()
    {
        // Health scaling.
        m_fCurrentHealth = m_fCurrentLevel * m_fHealthPerLevel;
        m_fMaxHealth = m_fCurrentHealth;
        // Damage scaling.
        m_fCurrentDamage = m_fCurrentLevel * m_fDamagePerLevel;
        // Experience scaling.
        m_fExperienceGivenOnDeath = m_fCurrentLevel * m_fExperiencePerLevel;
    }

    /// <summary>
    /// Check health values.
    /// </summary>
    protected virtual void CheckHealth()
    {
        // If there was a health change.
        if (m_fOldHealth != m_fCurrentHealth)
        {
            Color textureColor = Color.white;

            // If the health change was from critical damage.
            if (m_bIsCritted)
            {
                textureColor = Color.red;
                m_bIsCritted = false;
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
            OnDeath();
        }
    }

    /// <summary>
    /// Checks for debuffs to be applied.
    /// </summary>
    protected virtual void CheckDebuffs()
    {
        // Check fire.
        if (m_bBurn && m_bIsBuring)
        {
            StatusEffectManager.m_statusEffectManager.RequestEffect(transform, StatusEffect.Status.OnFire);
            m_bBurn = false;

            if (Player.m_Player.BurningSpeedBoost)
            {
                if (Player.m_Player.AdditionalBurningSpeedBoost)
                {
                    ++Player.m_Player.EnemiesOnFire;
                }

                ++Player.m_Player.EnemiesOnFire;
            }
        }

        // Check slow.
        if (m_bSlow && !m_bIsSlowed)
        {
            StatusEffectManager.m_statusEffectManager.RequestEffect(transform, StatusEffect.Status.Slowed);
            m_bSlow = false;
        }

        // Check stun.
        if (m_bStun && !m_bIsStunned)
        {
            StatusEffectManager.m_statusEffectManager.RequestEffect(transform, StatusEffect.Status.Stunned);
            m_bStun = false;
        }
    }

    /// <summary>
    /// Handles player death.
    /// </summary>
    protected virtual void OnDeath()
    {
        m_bIsAlive = false;
        ExpManager.m_experiencePointsManager.m_playerExperience += m_fExperienceGivenOnDeath;
        KillStreakManager.m_killStreakManager.AddKill();
        AudioManager.m_audioManager.EffectsAudioSource.PlayOneShot(AudioManager.m_audioManager.EnemyDeathClips[Random.Range(0, AudioManager.m_audioManager.EnemyDeathClips.Length)]);
        ExplosionManager.m_explosionManager.RequestExplosion(transform.position, transform.forward, Explosion.ExplosionType.BigBlood, 0.0f);
        ExplosionManager.m_explosionManager.RequestExplosion(transform.position, transform.forward, Explosion.ExplosionType.Gibs, 0.0f);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Gets a position to wander towards.
    /// </summary>
    /// <param name="a_v3CurrentPosition"></param>
    /// <returns></returns>
    protected virtual Vector3 GetWanderPosition(Vector3 a_v3CurrentPosition)
    {
        float fXOffset = Random.Range(-5, 5);
        float fZOffset = Random.Range(-5, 5);

        return new Vector3(a_v3CurrentPosition.x + fXOffset, a_v3CurrentPosition.y, a_v3CurrentPosition.z + fZOffset);
    }
}
