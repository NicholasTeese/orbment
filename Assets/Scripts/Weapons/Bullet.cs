using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("ShooterID")]
    public string m_id;
    [Header("Direction Vector")]
    public Vector3 m_direction;
    [Header("Damage")]
    public int m_baseDamage = 5;
    [HideInInspector]
    public int m_damage = 5;
    [Header("Projecile Speed")]
    public float m_projectileSpeed = 50;
    [HideInInspector]
    public BaseWeapon m_weaponFiredFrom;
    [Header("Culling Offset")]
    public int m_cullOffset = 500;

    [Header("Projectile Lifetime")]
    public float m_lifetime = 2.0f;
    private float m_timer = 0.0f;
    private MeshRenderer m_renderer;
    private Collider m_collider;
    private Light m_light;
    private TrailRenderer m_trail;

    public bool m_bColliding;

    private int L_Ground;
    private int L_Bullet;

    //x protected ExplosionManager m_explosionManager;
    protected Entity m_target = null;

    public Player m_playerRef = null;

    public bool m_isCrit = false;


    public enum ProjectileType
    {
        Normal,
        FireBall,
        IceShard,
        Lightning,
    }

    public ProjectileType m_type;

    protected virtual void Awake()
    {
        L_Ground = LayerMask.NameToLayer("Ground");
        L_Bullet = LayerMask.NameToLayer("Bullet");
    }

    // Use this for initialization
    protected virtual void Start()
    {
//        Debug.Log(this.transform.localScale);
        m_bColliding = false;
        //x m_explosionManager = GameObject.FindObjectOfType<ExplosionManager>();
        m_trail = this.GetComponent<TrailRenderer>();
        Physics.IgnoreLayerCollision(L_Bullet, L_Ground);
    }

    protected void OnEnable()
    {
        #region
        // "FOOTBALL BUG" - Caused by not setting the size of the bullets
        // Vector3 uniformScale = new Vector3(0.2f, 0.2f, 0.6f); // Arrows
        Vector3 uniformScale = new Vector3(0.5f, 0.5f, 0.5f);
        if (m_id == "Enemy")
        {
            this.transform.localScale = uniformScale;
        }
        #endregion

        m_light = this.GetComponent<Light>();
        m_collider = this.GetComponent<Collider>();
        m_renderer = this.GetComponent<MeshRenderer>();
        m_renderer.enabled = true;
        m_collider.enabled = true;
        if (m_light != null)
        {
            m_light.enabled = true;
        }
        m_bColliding = false;
    }


    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (!CalculateFrustrum(IsoCam.m_playerCamera.FrustrumPlanes, m_collider))
        {
            Disable();
        }

        this.transform.position += m_direction * m_projectileSpeed * Time.deltaTime;
        CameraCheck();
        m_timer += Time.deltaTime;

        //cull timer
        if (m_timer > m_lifetime)
        {
            Disable();
        }
    }

    //is in camera view?
    protected void Disable()
    {

        m_timer = 0.0f;
        m_target = null;
        this.gameObject.SetActive(false);
        if (m_trail != null)
        {
            m_trail.Clear();
        }


        if (m_weaponFiredFrom != null)
        {
            this.transform.position = m_weaponFiredFrom.transform.position;
            this.transform.parent = m_weaponFiredFrom.transform;
        }
    }

    protected void CameraCheck()
    {
        //disable when object leaves camera view
        Vector2 screenPosition = IsoCam.m_playerCamera.MainPlayerCamera.WorldToScreenPoint(this.transform.position);
        if (screenPosition.x < -m_cullOffset || screenPosition.x > Screen.width + m_cullOffset || screenPosition.y < -m_cullOffset || screenPosition.y > Screen.height + m_cullOffset)
        {
            Disable();
        }
    }


    protected virtual void OnCollisionEnter(Collision a_collision)
    {
        if (m_bColliding)
        {
            if (!a_collision.collider.CompareTag("Player"))
            {
                if (a_collision.collider.CompareTag("Untagged"))
                {
                    // Do nothing
                }
                else
                {
                    return;
                }
            }
        }

        m_bColliding = true;

        if (!a_collision.collider.CompareTag(m_id))
        {
            ExplosionManager.m_explosionManager.RequestExplosion(transform.position, -m_direction, Explosion.ExplosionType.BulletImpact, 0.0f);

            m_target = a_collision.collider.GetComponent<Entity>();
            //do base damage
            if (m_target != null)
            {
                if (!a_collision.collider.CompareTag("Player") || !Player.m_player.GodModeIsActive)
                {
                    if(Player.m_player.m_dashing && m_target.CompareTag("Player"))
                    {
                        // take no damage
                    }
                    else
                    {
                        if (Player.m_player.IceShield == true)
                        {
                            float adjustedDamage = m_damage * 0.25f;
                            m_target.m_beenCrit = m_isCrit;
                            m_target.m_currHealth -= (int)adjustedDamage;
                            m_target.m_recentDamageTaken = (int)adjustedDamage;
                            ExplosionManager.m_explosionManager.RequestExplosion(transform.position, -m_direction, Explosion.ExplosionType.SmallBlood, 0.0f);
                        }
                        else if (Player.m_player.IceShield == true)
                        {
                            float adjustedDamage = m_damage * 0.5f;
                            m_target.m_beenCrit = m_isCrit;
                            m_target.m_currHealth -= (int)adjustedDamage;
                            m_target.m_recentDamageTaken = (int)adjustedDamage;
                            ExplosionManager.m_explosionManager.RequestExplosion(transform.position, -m_direction, Explosion.ExplosionType.SmallBlood, 0.0f);
                        }
                        else
                        {
                            m_target.m_beenCrit = m_isCrit;
                            m_target.m_currHealth -= m_damage;
                            m_target.m_recentDamageTaken = m_damage;
                            ExplosionManager.m_explosionManager.RequestExplosion(transform.position, -m_direction, Explosion.ExplosionType.SmallBlood, 0.0f);
                        }
                    }
                }
            }
            Disable();
        }           
    }

    protected bool CalculateFrustrum(Plane[] a_frustrumPlanes, Collider a_collider)
    {
        if (GeometryUtility.TestPlanesAABB(a_frustrumPlanes, a_collider.bounds))
        {
            return true;
        }
        return false;
    }
}
