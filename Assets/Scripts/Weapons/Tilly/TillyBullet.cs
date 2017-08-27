using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TillyBullet : MonoBehaviour
{
    private int m_iDamage = 5;
    private int m_iCullOffset = 500;

    private float m_fSpeed = 50.0f;
    private float m_fLifeTime = 2.0f;
    private float m_fTimer = 0.0f;

    private BulletType m_eBulletType;

    private Vector3 m_v3Direction = Vector3.zero;

    private GameObject m_weapon;

    private MeshRenderer m_meshRenderer;

    private TrailRenderer m_trailRenderer;

    private Collider m_collider;

    private Light m_light;

    private ExplosionManager m_explosionManager;

    private Entity m_enemy = null;

    private void Awake()
    {
        m_trailRenderer = GetComponent<TrailRenderer>();

        //TODO: Create public static variable of ExplosionManager.
        m_explosionManager = FindObjectOfType<ExplosionManager>();
    }

    private void FixedUpdate()
    {
        m_fTimer += Time.deltaTime;

        transform.position += m_v3Direction * m_fSpeed * Time.deltaTime;

        CameraCheck();

        if (m_fTimer > m_fLifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();

        m_collider = GetComponent<Collider>();

        m_light = GetComponent<Light>();
    }

    private void OnDisable()
    {
        m_fTimer = 0.0f;

        m_enemy = null;

        m_trailRenderer.Clear();

        //TODO: Set weapon to be accessed by a public static reference from the player.
        transform.position = m_weapon.transform.position;
        transform.parent = m_weapon.transform;
    }

    private void OnCollisionEnter(Collision a_collision)
    {
        m_explosionManager.RequestExplosion(transform.position, -m_v3Direction, Explosion.ExplosionType.BulletImpact, 0.0f);

        m_enemy = a_collision.collider.GetComponent<Entity>();

        if (m_enemy != null)
        {
            m_enemy.m_currHealth -= m_iDamage;

            m_explosionManager.RequestExplosion(transform.position, -m_v3Direction, Explosion.ExplosionType.SmallBlood, 0.0f);
        }

        gameObject.SetActive(false);
    }

    private void CameraCheck()
    {
        //TODO: Set camera to be accessed by a public static reference from the player.
        Vector2 v2ScreenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (v2ScreenPosition.x < -m_iCullOffset ||
            v2ScreenPosition.x > Screen.width + m_iCullOffset ||
            v2ScreenPosition.y < -m_iCullOffset ||
            v2ScreenPosition.y > Screen.height + m_iCullOffset)
        {
            gameObject.SetActive(false);
        }
    }
}
