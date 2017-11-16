using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

//[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mana))]

//-------------------------------------------------------------------------------------------------------------------------------------------------------------

// Creators: Vince & John
// Additions: Taliesin Millhouse & Nicholas Teese
// Description: Handles player aspects i.e. Health/Mana/Shooting/Aiming/Damage Dealt/Damage Taken/Dash/Orbs/Movement

//-------------------------------------------------------------------------------------------------------------------------------------------------------------
public class Player : Entity
{
    //!? Unorganised variables.
    [Header("Damage Deviation Range")]
    public int m_damageDeviation = 3;

    //critical hits
    [Header("Criticals")]
    public int m_critPercentChance = 10;
    public float m_critDmgMult = 2.0f;
    [HideInInspector]
    public bool m_hasCrit = false;

    [Header("Firing interval")]
    public float m_playerFiringInterval = 0.1f;

    [Header("Point of projectile spawn")]
    public Transform m_shootPoint;

    [Header("Mana")]
    [Tooltip("Mana regen rate per second")]
    public float m_manaRegenAcceleration = 0.6f;
    private float m_currRegenRate = 0.0f;
    [Tooltip("Mana cost per projectile")]
    public float m_shootManaCost = 10.0f;

    [Header("Dash")]
    public float m_dashSpeed = 10.0f;
    public float m_dashTime = 0.2f;
    public float m_dashManaCost = 75.0f;

    [Header("Current Weapon")]
    public BaseWeapon m_currWeapon;

    [Header("Current Projectile")]
    //projectiles
    public GameObject m_currentProjectile;


    //orb count
    [Header("Orbs Collected")]
    public int m_orbsCollected = 0;


    private CharacterController m_charCont;
    private Vector3 m_movement;
    private float mouseX;
    private float mouseY;
    private Vector3 worldpos;
    private float cameraDif;

    private float m_playerFireTimer = 0.0f;
    private int m_shootPlane;

    private TrailRenderer m_dashTrail;
    private Mana m_manaPool;

    //dash
    [HideInInspector]
    public bool m_dashing = false;
    private float m_dashTimer = 0.0f;
    [HideInInspector]
    public Vector3 m_dashDirection;

    public GameObject m_spentOrbPrefab;
    public int m_poolAmountSpentOrbs = 15;
    private List<GameObject> m_spentOrbs = new List<GameObject>();

    public bool m_bImpacted = false;
    public float m_iImpactTimer = 0;
    //!? End of unorganised variables.

    private int m_iAdditionalBurnDPS = 0;
    private int m_iChanceToSetEnemiesOnFire = 0;
    private int m_iEnemiesOnFire = 0;

    private bool m_bCanSetEnemiesOnFire = false;
    private bool m_bBurningSpeedBoost = false;
    private bool m_bAdditionalBurningSpeedBoost = false;
    private bool m_bFreezeUnlocked = false;
    private bool m_bIceSplatterUnlocked = false;
    private bool m_bIceShield = false;
    private bool m_bIceArmor = false;
    private bool m_bGodModeAvailable = false;
    private bool m_bGodModeEnabled = false;

    private Animator m_animatior;

    private Vector3 m_v3LastMousePosition = Vector3.zero;

    public static Player m_player;

    // Variable getters and setters.
    public int ChanceToSetEnemiesOnFire { get { return m_iChanceToSetEnemiesOnFire; } set { m_iChanceToSetEnemiesOnFire = value; } }
    public int EnemiesOnFire { get { return m_iEnemiesOnFire; } set { m_iEnemiesOnFire = value;  m_iEnemiesOnFire = Mathf.Clamp(m_iEnemiesOnFire, 0, 50); } }
    public int AdditionalBurnDPS { get { return m_iAdditionalBurnDPS; } set { m_iAdditionalBurnDPS = value; } }

    public bool CanSetEnemiesOnFire { get { return m_bCanSetEnemiesOnFire; } set { m_bCanSetEnemiesOnFire = value; } }
    public bool IceSplatterUnlocked { get { return m_bIceSplatterUnlocked; } set { m_bIceSplatterUnlocked = value; } }
    public bool AdditionalBurningSpeedBoost { get { return m_bAdditionalBurningSpeedBoost; } set { m_bAdditionalBurningSpeedBoost = value; } }
    public bool BurningSpeedBoost { get { return m_bBurningSpeedBoost; } set { m_bBurningSpeedBoost = value; } }
    public bool FreezeUnlocked { get { return m_bFreezeUnlocked; } set { m_bFreezeUnlocked = value; } }
    public bool IceShield { get { return m_bIceShield; } set { m_bIceShield = value; } }
    public bool IceArmor { get { return m_bIceArmor; } set { m_bIceArmor = value; } }
    public bool GodModeAvailable { get { return m_bGodModeAvailable; } set { m_bGodModeAvailable = value; } }
    public bool GodModeEnabled { get { return m_bGodModeEnabled; } set { m_bGodModeEnabled = value; } }

    public Mana ManaPool { get { return m_manaPool; } }

    void Awake()
    {
        if (m_player == null)
        {
            m_player = this;
        }
        else if (m_player != this)
        {
            Destroy(gameObject);
        }

        m_animatior = transform.Find("Vince_Model_Beta").GetComponent<Animator>();

        m_manaPool = GetComponent<Mana>();
    }

    new void Start()
    {
        base.Start();
        PoolSpentOrbs();

        m_charCont = this.GetComponent<CharacterController>();
        m_camera = GameObject.FindObjectOfType<IsoCam>();


        m_shootPlane = LayerMask.GetMask("ShootPlane");
        //Cursor.visible = false;

        m_dashTrail = this.GetComponent<TrailRenderer>();

        if (m_dashTrail != null)
        {
            m_dashTrail.enabled = false;
        }

    }

    protected new void Update()
    {
		if (m_currHealth <= 0.0f && m_bIsAlive)
        {
            Time.timeScale = 0.5f;
            m_bIsAlive = false;
            m_animatior.SetBool("bAlive", m_bIsAlive);
            m_animatior.speed = 1.3f;
		}

        //#if UNITY_EDITOR
        if (DebugLevelSwitcher.m_bCheatsEnabled)
        {
            // Debug shortcut for Heal
            if (Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.KeypadPlus))
            {
                m_currHealth = m_maxHealth;
            }
            // Debug shortcut for Keys
            if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.KeypadPlus))
            {
                m_orbsCollected = 20;
            }
        }
//#endif


            PlayerHUDManager.m_playerHUDManager.HealthBar.GetComponent<Image>().fillAmount = m_currHealth / m_maxHealth;
        if (m_camera != null && m_currHealth < m_oldHealth)
        {
            //shake cam if player hurt
            m_camera.FlashRed(0.5f);
            m_camera.Shake(10.0f, 0.1f);

        }

        base.Update();

        m_currLevel = ExpManager.m_experiencePointsManager.m_playerLevel;


        m_damage = m_currDamage + Random.Range(-m_damageDeviation, m_damageDeviation);

        //mouse hold fire
        if ((Input.GetMouseButton(0) || InputManager.RightTriggerHold()) && Time.timeScale != 0.0f)
        {
            if (m_playerFireTimer >= m_playerFiringInterval)
            {
                m_playerFireTimer = 0.0f;
            }

            if (m_playerFireTimer == 0.0f)
            {
                if (m_manaPool.m_currentMana - m_shootManaCost >= 0.0f)
                {
                    if (m_currWeapon != null)
                    {
                        m_currWeapon.m_playerRef = this;
                        m_hasCrit = false;
                        if (Random.Range(0, 100) <= m_critPercentChance)
                        {
                            //crit
                            m_hasCrit = true;
                        }
                        //fire
                        m_animatior.SetBool("bShooting", true);
                        m_currWeapon.Fire(this.transform.forward, m_damage * m_currDamageMult, m_hasCrit, m_critDmgMult);

                        if (m_camera != null)
                        {
                            m_camera.Shake(2.5f, m_playerFiringInterval);
                        }
                    }
                    m_manaPool.m_currentMana -= m_shootManaCost;
                    Debug.Log(m_manaPool.m_currentMana);
                }
            }
            m_playerFireTimer += Time.deltaTime;
        }
        else
        {
            m_animatior.SetBool("bShooting", false);
        }

        //regen mana when mouse up
        if ((!Input.GetMouseButton(0) || m_manaPool.m_currentMana <= m_shootManaCost) && Time.timeScale != 0.0f)
        {
            RegenMana();
        }
        else
        {
            m_currRegenRate = 0.0f;
        }

        //Debug.Log("Movement: " + m_movement);
        //Debug.Log("Velocity: " + m_charCont.velocity);

        //dash
        if ((Input.GetKeyDown(KeyCode.Space) || InputManager.LeftTriggerDown()) && m_manaPool.m_currentMana >= m_dashManaCost && m_charCont.velocity != Vector3.zero)
        {
            if (m_dashTrail != null)
            {
                AudioManager.m_audioManager.PlayOneShotPlayerDash();
                m_dashTrail.enabled = true;
            }
            m_dashDirection = m_movement.normalized;
            m_manaPool.m_currentMana -= m_dashManaCost;
            m_dashing = true;
        }

        if (!GameManager.m_gameManager.GameIsPaused && m_bIsAlive)
        {
            //get movement input
            m_movement = Vector3.forward * InputManager.PrimaryVertical() + Vector3.right * InputManager.PrimaryHorizontal();
            Vector3 v3AnimationMovement = v3AnimationMovement = transform.InverseTransformDirection(InputManager.PrimaryInput());

            m_animatior.SetFloat("fXAxis", v3AnimationMovement.x);
            m_animatior.SetFloat("fYAxis", v3AnimationMovement.z);
        }
    }


    void FixedUpdate()
    {
        if (!m_bIsAlive)
        {
            return;
        }

        //mouse aiming
        if (InputManager.SecondaryInput() == Vector3.zero && m_v3LastMousePosition != Input.mousePosition)
        {
            RaycastHit hit;
            float rayLength = 1000;
            Ray ray = IsoCam.m_playerCamera.IsometricCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, rayLength, m_shootPlane))
            {
                Vector3 hitPoint = hit.point;
                hitPoint.y = 0;
                Vector3 playerToMouse = hitPoint - transform.position;
                playerToMouse = hitPoint - transform.position;
                playerToMouse.y = 0f;
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                transform.rotation = newRotation;
            }
        }

        if (InputManager.SecondaryInput() != Vector3.zero)
        {
            m_v3LastMousePosition = Input.mousePosition;
            //Debug.Log("Joy");
            Vector3 playerTojoy = Vector3.zero;
            playerTojoy = InputManager.SecondaryInput();
            Quaternion newRotation = Quaternion.LookRotation(playerTojoy);
            transform.rotation = newRotation;
        }

        //dashing
        if (m_dashing)
        {
            Dash(m_dashDirection);
        }
        else if (m_bIsAlive)
        {
            //movement
            m_charCont.Move(m_movement * (m_currSpeed + m_iEnemiesOnFire) * m_currSpeedMult * Time.deltaTime);
        }
    }


    void Dash(Vector3 dir)
    {
        m_currRegenRate = 0.0f;
        m_charCont.Move(dir * m_dashSpeed);

        m_dashTimer += Time.deltaTime;
        
        if (m_dashTimer >= m_dashTime)
        {
            m_dashTimer = 0.0f;
            m_dashing = false;

            if (m_dashTrail != null)
            {
                m_dashTrail.Clear();
                m_dashTrail.enabled = false;
            }
        }
    }

    void RegenMana()
    {
        if (m_manaPool.m_currentMana >= m_manaPool.m_maxMana)
        {
            m_manaPool.m_currentMana = m_manaPool.m_maxMana;
            m_currRegenRate = 0.0f;
        }

        if (m_manaPool.m_currentMana < m_manaPool.m_maxMana)
        {
            m_currRegenRate += m_manaRegenAcceleration * Time.deltaTime;
            m_manaPool.m_currentMana += m_currRegenRate * Time.deltaTime;
        }

    }

   
    void PoolSpentOrbs()
    {
        for (int i = 0; i < m_poolAmountSpentOrbs; ++i)
        {
            GameObject obj = GameObject.Instantiate(m_spentOrbPrefab);
            obj.tag = "spentOrb";
            obj.SetActive(false);
            m_spentOrbs.Add(obj);
        }
    }

    public void EmitSpentOrb(int a_num)
    {
        int count = 0;
        for (int i = 0; i < m_spentOrbs.Count; ++i)
        {
            if(count == a_num - 1)
            {
                return;
            }

            if (!m_spentOrbs[i].activeInHierarchy)
            {
                m_spentOrbs[i].transform.position = this.transform.position + Random.onUnitSphere;

                m_spentOrbs[i].SetActive(true);
                count++;                
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if(body == null)
        {
            return;
        }
    }

    public void OrbPickedUp()
    {
        AudioManager.m_audioManager.PlayOneShotOrbPickup();
    }
}
