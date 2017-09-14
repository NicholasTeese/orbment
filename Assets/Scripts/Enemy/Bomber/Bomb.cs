using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float m_fRollSpeed = 5.0f;
    private float m_fFuseTimer = 3.0f;
    private float m_fLastColourSwitchTime;

    private bool m_bHasExploded = false;

    private Vector3 m_v3RollPosition = Vector3.zero;

    Renderer m_renderer;

    private void Awake()
    {
        m_fLastColourSwitchTime = m_fFuseTimer;

        m_renderer = GetComponent<Renderer>();
        m_renderer.material.color = Color.black;
    }

    private void Start()
    {
        m_v3RollPosition = Player.m_Player.transform.position;
    }

    private void Update()
    {
        m_fFuseTimer -= Time.deltaTime;
        
        if (m_fLastColourSwitchTime - m_fFuseTimer > 0.5f)
        {
            m_fLastColourSwitchTime = m_fFuseTimer;

            if (m_renderer.material.color == Color.black)
            {
                m_renderer.material.color = Color.red;
            }
            else
            {
                m_renderer.material.color = Color.black;
            }
        }

        if (m_fFuseTimer >= 0.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_v3RollPosition, (m_fRollSpeed * Time.deltaTime));
        }
        else if (m_fFuseTimer <= 0.0f && !m_bHasExploded)
        {
            ExplosionManager.m_explosionManager.RequestExplosion(transform.position, transform.forward, Explosion.ExplosionType.Fire, 100.0f);
            m_bHasExploded = true;
            Destroy(gameObject);
        }
    }
}
