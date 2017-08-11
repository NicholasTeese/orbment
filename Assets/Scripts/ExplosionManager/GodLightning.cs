using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodLightning : MonoBehaviour
{

    public float m_startHeight = 100.0f;
    public int m_numSegments = 20;
    public int m_damage = 1000;

    private LineRenderer m_line;
    private List<Vector3> m_positions = new List<Vector3>();
    // Use this for initialization

    private void OnEnable()
    {
        m_line = this.GetComponent<LineRenderer>();
        Strike();
    }
   
    void Strike()
    {
        m_positions.Clear();
        Vector3 newPosition = Vector3.zero;
        for (int i = 0; i < m_numSegments; ++i)
        {
            if (i == 0)
            {
                m_positions.Add(this.transform.position + Vector3.up * m_startHeight);
            }
            else if (i == m_numSegments - 1)
            {
                Vector3 endPos = this.transform.position;
                endPos.y = 0.0f;
                m_positions.Add(endPos);
            }
            else
            {
                Vector3 deviation = (Vector3.down + Random.onUnitSphere).normalized;
                deviation.y = -(m_startHeight / m_numSegments);
                newPosition = m_positions[i - 1] + deviation;
                m_positions.Add(newPosition);
            }

        }
        m_line.numPositions = m_positions.Count;
        m_line.SetPositions(m_positions.ToArray());
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            Entity enemy = col.GetComponent<Entity>();

            if (enemy != null)
            {

                enemy.m_currHealth -= m_damage;

            }
        }
    }
}
