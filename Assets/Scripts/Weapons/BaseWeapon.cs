using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [Header("ShooterID")]
    public string m_id;
    public GameObject m_projectile;
    public uint m_maxBulletsOnScreen = 50;

    public Player m_playerRef = null;
    //[Header("Perk Effects")]
    //public bool m_hasFireSplash = false;
    //public bool m_hasIceSplit = false;
    //public bool m_hasStunPerk = false;
    //public bool m_hasGodLightning = false;

    protected List<GameObject> m_projectilePool = new List<GameObject>();
    protected List<Bullet> m_projectileScripts = new List<Bullet>();

    public GameObject m_SBasic;
    public GameObject m_SLighting;
    public GameObject m_SFire;
    public GameObject m_SIce;


    protected List<Bullet> m_activePool = new List<Bullet>();

    protected AudioSource m_audioSource;

    public AudioSource BulletAudioSource { get { return m_audioSource; } }

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }


    private int projectileCount = 0;
    // Use this for initialization
    void Start()
    {
        //m_SBasic = GameObject.Find("Basic_Staff");
        //m_SLighting = GameObject.Find("Lightning_Staff");
        //m_SFire = GameObject.Find("Fire_Staff");
        //m_SIce = GameObject.Find("Ice_Staff");

        //if (m_SBasic != null)
        //    m_SBasic.SetActive(true);

        //object pooling
        if (m_projectile != null)
        {
            for (uint i = 0; i < m_maxBulletsOnScreen; ++i)
            {
                GameObject projectile = Instantiate(m_projectile, transform);
                projectile.SetActive(false);
                projectile.transform.parent = transform;
                m_projectilePool.Add(projectile);
                Bullet projectileScript = projectile.GetComponent<Bullet>();
                if (projectileScript != null)
                {
                    projectileScript.m_weaponFiredFrom = this;
                    projectileScript.m_id = m_id;
                    m_projectileScripts.Add(projectileScript);
                }
                // Staff switching
                if(m_id == "Player")
                {
                    switch (m_projectileScripts[0].m_type)
                    {
                        case Bullet.ProjectileType.Normal:
                            {
                                // Set active staff model to Normal staff
                                m_SBasic.SetActive(true);
                                m_SLighting.SetActive(false);
                                m_SFire.SetActive(false);
                                m_SIce.SetActive(false);

                                break;
                            }
                        case Bullet.ProjectileType.Lightning:
                            {
                                // Set active staff model to Lightning staff
                                m_SBasic.SetActive(false);
                                m_SLighting.SetActive(true);
                                m_SFire.SetActive(false);
                                m_SIce.SetActive(false);

                                break;
                            }
                        case Bullet.ProjectileType.FireBall:
                            {
                                // Set active staff model to Fire staff
                                m_SBasic.SetActive(false);
                                m_SLighting.SetActive(false);
                                m_SFire.SetActive(true);
                                m_SIce.SetActive(false);

                                break;
                            }
                        case Bullet.ProjectileType.IceShard:
                            {
                                // Set active staff model to Ice staff
                                m_SBasic.SetActive(false);
                                m_SLighting.SetActive(false);
                                m_SFire.SetActive(false);
                                m_SIce.SetActive(true);

                                break;
                            }
                        default:
                            {
                                // Output error code
                                Debug.Log("Projectile type could not be found. " + m_projectileScripts[0].m_type);
                                break;
                            }
                    }
                }
            }
        }
    }

    public virtual void Fire(Vector3 a_direction, int damagePerProjectile, bool a_hasCrit, float a_critMult)
    {
        //set velocities and directions
    }
    

    //find inactive and insert into active pool
    protected void PoolToActive(Vector3 a_direction, int damagePerProjectile, int numOfProjectilesRequested)
    {
        m_activePool.Clear();
        for (int i = 0; i < m_maxBulletsOnScreen; ++i)
        {
            if (m_projectilePool.Count != 0)
            {
                if (!m_projectilePool[i].activeInHierarchy)
                {
                   
                    m_projectilePool[i].transform.parent = null;
                    m_projectilePool[i].transform.position = this.transform.position;
                    m_projectilePool[i].SetActive(true);

                   if(m_playerRef != null)
                    {
                        //pass the player reference through to let bullet know of perks on player
                        m_projectileScripts[i].m_playerRef = this.m_playerRef;
                    }
                    m_projectileScripts[i].m_damage = m_projectileScripts[i].m_baseDamage + damagePerProjectile;
                    //insert to active pool

                    m_activePool.Add(m_projectileScripts[i]);


                    if (projectileCount == numOfProjectilesRequested -1)
                    {
                        projectileCount = 0;
                        break;
                    }
                    projectileCount++;
                }
            }
        }
    }

    public void SetProjectile(GameObject a_projectile)
    {
        //clear pool
        m_projectilePool.Clear();
        m_projectileScripts.Clear();
        
        //set new projectile
        m_projectile = a_projectile;

        Start();
    }
}