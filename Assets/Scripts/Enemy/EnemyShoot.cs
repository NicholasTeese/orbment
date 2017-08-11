using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(FindObjectsInRadius))]

public class EnemyShoot : MonoBehaviour
{
    private int m_damagePerProjectile;
    public float m_attackInterval = 1.0f;
    public bool m_canAttack = true;

    private FindObjectsInRadius m_foir;
    private BaseWeapon m_weapon;
    private float m_attackTimer = 0.0f;
    private Vector3 m_shootDir;
    private Enemy m_enemyScript;
    // Use this for initialization
    void Start()
    {
        m_enemyScript = this.GetComponent<Enemy>();
        m_weapon = this.GetComponent<BaseWeapon>();
        m_foir = this.GetComponent<FindObjectsInRadius>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_canAttack)
        {
            return;
        }
        if (m_foir != null && m_foir.inRange)
        {
            this.transform.LookAt(m_foir.m_target);
            if (m_attackTimer >= m_attackInterval)
            {
                m_attackTimer = 0.0f;
            }

            if (m_attackTimer == 0.0f)
            {
                if (m_foir.m_target != null && m_enemyScript != null)
                {
                    //shoot
                    m_shootDir = m_foir.m_target.position - this.transform.position;
                    m_weapon.Fire(m_shootDir.normalized, m_enemyScript.m_currDamage, false, 1);
                }
            }


            m_attackTimer += Time.deltaTime;
        }

    }
}
