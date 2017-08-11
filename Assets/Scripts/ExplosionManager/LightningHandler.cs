using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Explosion))]
public class LightningHandler : MonoBehaviour
{

    public float m_range = 10.0f;
    public float m_timeBetweenHits = 0.05f;
    public float m_damageDecay = 0.75f;


    private float m_currDamage = 0.0f;

    private List<Collider> m_zapped = new List<Collider>();

    private Collider m_closestCollider = null;
    private Collider m_lastHit = null;

    private float m_timer = 0.0f;

    private LineRenderer m_LineRenderer;
    private Explosion m_explosionScript;

    private void Start()
    {
        m_LineRenderer = this.GetComponent<LineRenderer>();

    }

    private void OnEnable()
    {
        m_closestCollider = FindClosestEnemy(this.transform.position);
        m_zapped.Add(m_closestCollider);
        m_lastHit = m_closestCollider;
        m_explosionScript = this.GetComponent<Explosion>();
        m_currDamage = m_explosionScript.m_damage;
    }

    private void OnDisable()
    {
        m_zapped.Clear();
    }

    public void AttackEnemy(Collider a_enemy)
    {
        if (m_lastHit != null && a_enemy != null)
        {
            m_LineRenderer.SetPosition(0, m_lastHit.transform.position);
            m_LineRenderer.SetPosition(1, a_enemy.transform.position);
        }

        //if damage too small or cant find next target then disable
        if (m_currDamage <= 1.0f || a_enemy == null)
        {
            this.gameObject.SetActive(false);
        }

        Entity enemy = a_enemy.GetComponent<Entity>();
        if (enemy != null && !m_zapped.Contains(a_enemy))
        {
            enemy.m_currHealth -= Mathf.CeilToInt(m_currDamage);
            m_currDamage *= m_damageDecay;

            if (a_enemy != null)
            {
                m_zapped.Add(a_enemy);
            }
        }
    }

    public void Update()
    {

        if (m_closestCollider == null)
        {
            return;
        }

        if (m_timer == 0.0f)
        {
            if (m_closestCollider != null)
            {

                AttackEnemy(m_closestCollider);
                m_lastHit = m_closestCollider;
                m_closestCollider = FindClosestEnemy(m_closestCollider.transform.position);
            }
        }

        m_timer += Time.deltaTime;

        if (m_timer > m_timeBetweenHits)
        {
            ResetTimer();

        }
    }


    private void ResetTimer()
    {
        m_timer = 0.0f;
    }


    Collider FindClosestEnemy(Vector3 a_position)
    {
        Collider[] nearby = Physics.OverlapSphere(a_position, 10.0f);
        float closest = Mathf.Infinity;

        foreach (Collider col in nearby)
        {
            if (!m_zapped.Contains(col) && col.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(col.transform.position, this.transform.position);
                if (distance < closest)
                {
                    m_closestCollider = col;
                    closest = distance;
                }

            }
        }

        return m_closestCollider;
    }
}
