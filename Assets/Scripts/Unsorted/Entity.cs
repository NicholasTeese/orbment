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
    public bool m_ringOfFireActive = false;


    [Header("Perks")]
    public bool m_hasRingOfFire = false;
    public List<PerkID> m_perks = new List<PerkID>();

    // Use this for initialization
    protected void Start()
    {
        m_agent = this.GetComponent<NavMeshAgent>();
        
        if (m_agent != null)
        {
            m_originalMoveSpeed = m_agent.speed;
        }

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

        //Check for ring of fire
        if (!m_hasRingOfFire && m_perks.Contains(PerkID.RingOfFire))
        {
            m_hasRingOfFire = true;
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
            m_bIsAlive = false;
            m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.BigBlood, 0.0f);
            m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.Gibs, 0.0f);
            this.gameObject.SetActive(false);
        }

        ///STATUS EFFECTS
        //if set on fire and previously not on fire. to avoid effect stacking
        if (m_setOnFire && !m_onFire)
        {
            m_setOnFire = false;

            m_statusEffectManager.RequestEffect(this.transform, StatusEffect.Status.OnFire);

            if (!gameObject.CompareTag("Player") && Player.m_player.BurningSpeedBoost)
            {
                if (Player.m_player.BurningSpeedBoost)
                {
                    ++Player.m_player.EnemiesOnFire;
                }
                //x ++Player.m_player.EnemiesOnFire;
            }
        }

        if (!m_ringOfFireActive && HealthBelowPercentCheck(25) && m_hasRingOfFire)
        {
            m_statusEffectManager.RequestEffect(this.transform, StatusEffect.Status.FireRing);
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
