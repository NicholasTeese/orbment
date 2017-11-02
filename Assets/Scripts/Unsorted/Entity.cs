using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//-------------------------------------------------------------------------------------------------------------------------------------------------------------

// Creators: Vince & John
// Additions: Taliesin Millhouse & Nicholas Teese
// Description: Agents in game inherit from this script - assigns variables and functions for agent management

//-------------------------------------------------------------------------------------------------------------------------------------------------------------
[RequireComponent(typeof(NavMeshAgent))]
public class Entity : MonoBehaviour
{
    protected bool m_bIsAlive = true;
    public bool IsAlive { get { return m_bIsAlive; } }

    //TODO: Move perk related variables to the player class as other entities wont have perks.
    protected float m_fGodModeTimer = 5.0f;
    public float GodModeTimer { get; set; }

    protected bool m_bGodModeIsAvailable = false;
    public bool GodModeIsAvailable { get; set; }

    public bool m_bGodModeIsActive = false;
    public bool GodModeIsActive { get; set; }

    [Header("Level")]
    public int m_currLevel = 1;
    public int m_healthPerLevel = 50;
    public int m_damagePerLevel = 5;
    public int m_xpPerLevel = 20;

    [Header("Health")]
    public int m_healthBarWidth = 100;
    public float m_maxHealth = 100;
    public float m_currHealth = 100;
    public float m_experienceValue = 10;
    public Texture m_healthBarTexture;
    public Texture m_emptyBarTexture;
    public int m_recentDamageTaken = 0;

    [Header("Current Damage and Speed")]
    public int m_currDamage = 10;
    public float m_currSpeed = 10;
    protected int m_damage = 10;


    [Header("Current Damage and Speed Multipliers")]
    public int m_currDamageMult = 1;
    public int m_currSpeedMult = 1;

    //Agent
    protected NavMeshAgent m_agent;
    //original move speed;
    [HideInInspector]
    public float m_originalMoveSpeed = 0.0f;

    //old health;
    protected float m_oldHealth = 0.0f;


    //protected ExpManager m_expManager;
    //protected DamageNumberManager m_damageNumbersManager;
    protected StatusEffectManager m_statusEffectManager;
    protected ExplosionManager m_explosionManager;
    protected KillStreakManager m_killStreakManager;

    protected IsoCam m_camera;

    //public Transform m_RecentAttacker;

    [HideInInspector]
    public bool m_setOnFire = false, m_causeStun = false, m_causeSlow = false, m_giveBuff = false;

    [HideInInspector]
    public bool m_beenCrit = false;

    //status effects
    [Header("Status Effects")]
    public bool m_onFire = false;
    public bool m_isSlowed = false;
    public bool m_isStunned = false;
    public bool m_isBuffed = false;
    public bool m_ringOfFireActive = false;
    public bool m_lightningFieldActive = false;


    [Header("Perks")]
    public bool m_hasRamboPerk = false;
    public bool m_hasRingOfFire = false;
    public bool m_hasLightningField = false;
    public List<PerkID> m_perks = new List<PerkID>();

    // Use this for initialization
    protected void Start()
    {
        //gameManager = GameManager.m_gameManager;/
        m_agent = this.GetComponent<NavMeshAgent>();
        
        if (m_agent != null)
        {
            m_originalMoveSpeed = m_agent.speed;
        }
        //m_expManager = GameObject.FindObjectOfType<ExpManager>();
        //m_damageNumbersManager = GetComponent<DamageNumberManager>();
        m_statusEffectManager = GameObject.FindObjectOfType<StatusEffectManager>();
        m_explosionManager = GameObject.FindObjectOfType<ExplosionManager>();
        m_killStreakManager = GameObject.FindObjectOfType<KillStreakManager>();

        LevelUpdate();

        m_oldHealth = m_currHealth;
    }

    protected void OnEnable()
    {
        LevelUpdate();
    }

    void LevelUpdate()
    {
        if (ExpManager.m_experiencePointsManager != null)
        {
            //health scaling
            //m_currLevel = Random.Range(m_expManager.m_playerLevel, m_expManager.m_playerLevel + 2);
            m_currHealth = m_currLevel * m_healthPerLevel;
            m_maxHealth = m_currHealth;

            //damage scaling
            m_currDamage = m_currLevel * m_damagePerLevel;

            //exp scaling
            m_experienceValue = m_currLevel * m_xpPerLevel;
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        HealthUpdate();

        if (!m_hasRamboPerk && m_perks.Contains(PerkID.RamboMode))
        {
            m_hasRamboPerk = true;
        }

        //Check for ring of fire
        if (!m_hasRingOfFire && m_perks.Contains(PerkID.RingOfFire))
        {
            m_hasRingOfFire = true;
        }

        if (!m_hasLightningField && m_perks.Contains(PerkID.LightningField))
        {
            m_hasLightningField = true;
        }
    }

    protected void HealthUpdate()
    {
        if (m_oldHealth != m_currHealth)
        {
            Color textColor = Color.white;

            if (m_beenCrit)
            {
                textColor = Color.red;
                m_beenCrit = false;
            }

            if (m_currHealth > m_oldHealth)
            {
                textColor = Color.green;
            }
            DamageNumberManager.m_damageNumbersManager.CreateDamageNumber(Mathf.Abs(m_oldHealth - m_currHealth).ToString(), this.transform, textColor);
        }

        //cant go above max
        if (m_currHealth > m_maxHealth)
        {
            m_currHealth = m_maxHealth;
        }
        
        if (m_currHealth <= 0)
        {
            if (!m_bGodModeIsActive)
            {
                if (gameObject.CompareTag("Player"))
                {
                    Time.timeScale = 0.5f;
                }

                m_bIsAlive = false;
                m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.BigBlood, 0.0f);
                m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.Gibs, 0.0f);
                this.gameObject.SetActive(false);
            }
            else
            {
                m_currHealth = 1;
            }
        }

        ///STATUS EFFECTS
        //if set on fire and previously not on fire. to avoid effect stacking
        if (m_setOnFire && !m_onFire)
        {
            m_statusEffectManager.RequestEffect(this.transform, StatusEffect.Status.OnFire);
            m_setOnFire = false;
            
            if (!gameObject.CompareTag("Player") && Player.m_player.BurningSpeedBoost)
            {
                if (Player.m_player.AdditionalBurningSpeedBoost)
                {
                    ++Player.m_player.EnemiesOnFire;
                }
                ++Player.m_player.EnemiesOnFire;
            }
        }

        if (m_causeSlow && !m_isSlowed)
        {
            m_statusEffectManager.RequestEffect(this.transform, StatusEffect.Status.Slowed);
            m_causeSlow = false;
        }

        if (m_causeStun && !m_isStunned)
        {
            m_statusEffectManager.RequestEffect(this.transform, StatusEffect.Status.Stunned);
            m_causeStun = false;
        }

        if (!m_isBuffed && HealthBelowPercentCheck(10) && m_hasRamboPerk)
        {
            m_statusEffectManager.RequestEffect(this.transform, StatusEffect.Status.Buffed);
        }

        if (!m_ringOfFireActive && HealthBelowPercentCheck(25) && m_hasRingOfFire)
        {
            m_statusEffectManager.RequestEffect(this.transform, StatusEffect.Status.FireRing);
        }

        if (!m_lightningFieldActive && HealthBelowPercentCheck(25) && m_hasLightningField)
        {
            m_statusEffectManager.RequestEffect(this.transform, StatusEffect.Status.LightningRing);
        }

        m_oldHealth = m_currHealth;
    }

    public bool HealthBelowPercentCheck(float m_threshold)
    {
        if (m_currHealth <= m_maxHealth * (m_threshold / 100.0f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
