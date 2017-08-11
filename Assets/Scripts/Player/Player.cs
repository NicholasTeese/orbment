using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

//[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mana))]
public class Player : Entity
{
	//Johns Variables

	public AudioClip fire_shot;
	public AudioClip ice_shot;
	public AudioClip lightning_shot;
	public AudioSource shootingAudioSource;

	//End of Johns Variables


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
    public float m_dashManaCost = 50.0f;

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









    new void Start()
    {

        base.Start();
        PoolSpentOrbs();


        m_charCont = this.GetComponent<CharacterController>();
        m_manaPool = this.GetComponent<Mana>();
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
		if (m_currHealth <= 1) {
			
			GameObject.Find ("GameManager").GetComponent<GameManager> ().paused = true;
		}
		GameObject.Find ("GameManager").GetComponent<GameManager> ().healthBar.GetComponent<Slider> ().maxValue = m_maxHealth;
		GameObject.Find ("GameManager").GetComponent<GameManager> ().healthBar.GetComponent<Slider> ().value = m_currHealth;
        if (m_camera != null && m_currHealth < m_oldHealth)
        {

            //shake cam if player hurt
            m_camera.FlashRed(0.5f);
            m_camera.Shake(10.0f, 0.1f);

        }
        base.Update();

        m_currLevel = m_expManager.m_playerLevel;


        m_damage = m_currDamage + Random.Range(-m_damageDeviation, m_damageDeviation);

        //mouse hold fire
        if (Input.GetMouseButton(0) && Time.timeScale != 0.0f)
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
                        m_currWeapon.Fire(this.transform.forward, m_damage * m_currDamageMult, m_hasCrit, m_critDmgMult);

						//Johns Code [Sorry]

						//Shooting Audio

						if (m_currentProjectile.name == "FireBall") {
							shootingAudioSource.pitch = 1 + Random.Range(0f, 0.5f);
							shootingAudioSource.PlayOneShot (fire_shot, 0.7f);
						}
						if (m_currentProjectile.name == "IceShard") {
							shootingAudioSource.pitch = 1 + Random.Range(0f, 0.5f);
							shootingAudioSource.PlayOneShot (ice_shot, 0.7f);
						}
						if (m_currentProjectile.name == "LightningBall") {
							shootingAudioSource.pitch = 1 + Random.Range(-0.1f, 0.6f);
							shootingAudioSource.PlayOneShot (lightning_shot, 0.7f);
						}

						//End of Johns Code
                        if (m_camera != null)
                        {
                            m_camera.Shake(5.0f, m_playerFiringInterval);
                        }
                    }
                    m_manaPool.m_currentMana -= m_shootManaCost;
                }

            }

            m_playerFireTimer += Time.deltaTime;
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



        //dash
        if (Input.GetKeyDown(KeyCode.Space) && m_manaPool.m_currentMana >= m_dashManaCost && m_movement != Vector3.zero)
        {
            if (m_dashTrail != null)
            {
				GameObject.Find ("AudioManager").GetComponent<AudioManager> ().DashSound ();
                m_dashTrail.enabled = true;

            }

            m_dashDirection = m_movement.normalized;
            m_manaPool.m_currentMana -= m_dashManaCost;
            m_dashing = true;

        }

        //get movement input
        m_movement = Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal");

    }


    void FixedUpdate()
    {



        //mouse aiming
        RaycastHit hit;
        float rayLength = 1000;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, rayLength, m_shootPlane))
        {

            Vector3 hitPoint = hit.point;
            hitPoint.y = 0;
            Vector3 playerToMouse = hitPoint - this.transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            this.transform.rotation = newRotation;
            Debug.DrawRay(this.transform.position, this.transform.forward * 10, Color.cyan);

            Debug.DrawLine(this.transform.position, hit.point, Color.red);
        }

        //dashing
        if (m_dashing)
        {
            Dash(m_dashDirection);

        }
        else
        {
            //movement
            m_charCont.Move(m_movement * m_currSpeed * m_currSpeedMult * Time.deltaTime);
        }

    }





    void Dash(Vector3 dir)
    {
        m_currRegenRate = 0.0f;
        m_charCont.Move(dir * m_dashSpeed);

        m_dashTimer += Time.deltaTime;

        if (m_explosionManager != null)
        {
            m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.AfterImage, 0.0f);
        }

        //float ratio = m_dashTimer / m_dashTime;

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



}
