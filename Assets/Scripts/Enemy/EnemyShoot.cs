using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(FindObjectsInRadius))]

public class EnemyShoot : MonoBehaviour
{
//    private int m_damagePerProjectile;
    public float m_attackInterval = 1.0f;
    public bool m_canAttack = true;

    private float m_fSearchTimer = 0.5f;
    private float m_fOriginalSearchTimer;

    private FindObjectsInRadius m_foir;
    private BaseWeapon m_weapon;
    private float m_attackTimer = 0.0f;
    private Vector3 m_shootDir;
    private Enemy m_enemyScript;
    private Collider m_collider;

    private Animator m_animator;

    private void Awake()
    {
        m_fOriginalSearchTimer = m_fSearchTimer;
    }

    void Start()
    {
        m_enemyScript = this.GetComponent<Enemy>();
        m_weapon = this.GetComponent<BaseWeapon>();
        m_foir = this.GetComponent<FindObjectsInRadius>();
        m_collider = GetComponent<Collider>();

        m_animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (!CalculateFrustrum(IsoCam.m_playerCamera.FrustrumPlanes, m_collider))
        {
            return;
        }

        if (!m_canAttack)
        {
            return;
        }

        if (m_foir.inRange)
        {
            m_fSearchTimer = m_fOriginalSearchTimer;

            if (m_enemyScript.Running)
            {
                m_animator.SetTrigger("Run");
            }
            else
            {
                m_animator.SetTrigger("Prepare");
            }
        }
        else
        {
            m_fSearchTimer -= Time.deltaTime;

            if (m_fSearchTimer <= 0.0f)
            {
                m_animator.SetTrigger("Idle");
            }
        }

        if (m_foir != null && m_foir.inRange)
        {
            transform.LookAt(new Vector3(m_foir.m_target.position.x, transform.position.y, m_foir.m_target.position.z));

            if (m_attackTimer >= m_attackInterval)
            {
                m_attackTimer = 0.0f;
            }

            if (m_attackTimer == 0.0f)
            {
                if (m_foir.m_target != null && m_enemyScript != null)
                {
                    //shoot
                    Vector3 V_targetOffset = new Vector3(m_foir.m_target.transform.position.x - this.transform.position.x, transform.position.y - this.transform.position.y, m_foir.m_target.transform.position.z - this.transform.position.z);
                    //Debug.Log(V_targetOffset);
                    m_shootDir = (m_foir.m_target.position - this.transform.position);
                    m_animator.SetTrigger("Fire");
                    transform.LookAt(new Vector3(m_foir.m_target.transform.position.x, transform.position.y, m_foir.m_target.transform.position.z));
                    m_weapon.Fire(V_targetOffset.normalized, m_enemyScript.m_currDamage, false, 1);
                }
            }
            else
            {
                m_animator.SetTrigger("Wait_For_Action");
            }
            m_attackTimer += Time.deltaTime;
        }
    }

    private bool CalculateFrustrum(Plane[] a_frustrumPlanes, Collider a_collider)
    {
        if (GeometryUtility.TestPlanesAABB(a_frustrumPlanes, a_collider.bounds))
        {
            return true;
        }

        return false;
    }
}
