using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class LightningField : StatusEffect
{
    public float m_healthPercentThreshold = 25.0f;
    public int m_lightningFieldDPS = 5;
    public float m_tickInterval = 0.5f;
    private float m_elapsed = 0.0f;

    private LineRenderer m_line;
    private List<Vector3> m_positions = new List<Vector3>();
    // Use this for initialization
    void Start()
    {
        this.m_type = Status.LightningRing;
        m_line = this.GetComponent<LineRenderer>();
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        if (m_entity != null)
        {
            m_entity.m_lightningFieldActive = true;
        }


    }

    void OnDisable()
    {
        if (m_entity != null)
        {
            m_entity.m_lightningFieldActive = false;
        }
    }

    private void FixedUpdate()
    {
        m_line.numPositions = 0;
        int numEnemies = 1;
        m_positions.Clear();
       
        m_positions.Add(this.transform.position);
       Collider[] inSphere =  Physics.OverlapSphere(this.transform.position, 10);


        for (int i = 0; i < inSphere.Length; ++i )
        {
            if(inSphere[i].CompareTag("Enemy"))
            {
                m_positions.Add(inSphere[i].transform.position);
                m_positions.Add(this.transform.position);
                numEnemies += 2;
            }
        }


        m_line.numPositions = numEnemies;
        m_line.SetPositions(m_positions.ToArray());


        if (m_entity != null)
        {
            if (!m_entity.HealthBelowPercentCheck(m_healthPercentThreshold))
            {
                m_entity.m_lightningFieldActive = false;
                ReturnToSender();
            }


        }

    }

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            Enemy entity = col.GetComponent<Enemy>();

            if (entity != null)
            {
                m_elapsed += Time.deltaTime;
                if (m_elapsed >= m_tickInterval)
                {
                    m_elapsed = m_elapsed % m_tickInterval;
                    entity.m_currHealth -= m_lightningFieldDPS;
                }


            }
        }
    }
}
