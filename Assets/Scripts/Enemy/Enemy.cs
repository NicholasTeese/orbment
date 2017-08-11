using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public enum EnemyType
    {
        Hunter,
        Defender,
        Protector
    }

    public EnemyType m_type;


    // Update is called once per frame
    new void Update()
    {
        if (m_currHealth <= 0)
        {

            m_expManager.m_playerExperience += m_experienceValue;
            if (m_killStreakManager != null)
            {
                m_killStreakManager.AddKill();

            }

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
                m_agent.SetDestination(collision.collider.transform.position - m_bulletScript.m_direction);
            }
        }
    }

    new private void OnGUI()
    {
        base.OnGUI();

        Vector2 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);

        GUI.Label(new Rect(screenPoint.x - 0.5f * m_healthBarWidth, Screen.height - screenPoint.y - 60, m_healthBarWidth, 50), "Lvl " + m_currLevel);
    }
}
