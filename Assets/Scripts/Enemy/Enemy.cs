using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public enum EnemyType
    {
        Hunter,
        Defender,
        Protector,
        MELEE,
        BOMBER
    }

    protected Collider m_collider;

    private Animator m_Animator;

    public EnemyType m_type;

    protected void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider>();

        if (m_type == EnemyType.Hunter)
        {
            //m_Animator.SetBool("Walking2Recovery", false);
        }
    }

    new void Update()
    {
        // kill code for debugging
        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Equals) && Input.GetKey(KeyCode.Alpha0))
        {
            this.m_currHealth = 0;
        }

        if (m_currHealth <= 0)
        {
            m_expManager.m_playerExperience += m_experienceValue;
            if (m_killStreakManager != null)
            {
                m_killStreakManager.AddKill();
            }
        }

        if (!CalculateFrustrum(IsoCam.m_playerCamera.FrustrumPlanes, m_collider))
        {
            return;
        }

        base.Update();

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Bullet m_bulletScript = collision.collider.GetComponent<Bullet>();

            if (m_bulletScript != null && m_bulletScript.m_id == "Player")
            {
                if (m_type != EnemyType.BOMBER)
                {
                    m_agent.SetDestination(collision.collider.transform.position - m_bulletScript.m_direction); // Travel to bullet origin
                }
                //m_agent.transform.LookAt(collision.collider.transform.position - m_bulletScript.m_direction); // Look at bullet origin
            }
        }
    }

    new private void OnGUI()
    {
        if (ExpManager.m_experiencePointsManager.PerkTreeOpen || GameManager.m_gameManager.GameIsPaused)
        {
            return;
        }
        else if (m_currHealth != m_maxHealth)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
            GUI.DrawTexture(new Rect(screenPoint.x - 0.5f * m_healthBarWidth, Screen.height - screenPoint.y - 40, m_healthBarWidth, 10), m_emptyBarTexture);
            GUI.DrawTexture(new Rect(screenPoint.x - 0.5f * m_healthBarWidth, Screen.height - screenPoint.y - 40, m_healthBarWidth * ((float)m_currHealth / (float)m_maxHealth), 10), m_healthBarTexture);
        }
        else
        {
            base.OnGUI();
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
            GUI.Label(new Rect(screenPoint.x - 0.5f * m_healthBarWidth, Screen.height - screenPoint.y - 60, m_healthBarWidth, 50), "Lvl " + m_currLevel);
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
