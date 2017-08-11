using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DefenderVision : MonoBehaviour
{
    public string m_targetTag;
    public float m_sightRadius = 20.0f;
    //private NavMeshAgent m_agent;
    public Vector3 m_originalPos;
    private Collider[] insideSphere;
    public Transform m_target;
    // Use this for initialization
    public bool inSight = false;
    void Start()
    {
        //m_agent = this.GetComponent<NavMeshAgent>();
        m_originalPos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        insideSphere = Physics.OverlapSphere(m_originalPos, m_sightRadius);

        if (m_targetTag == "")
        {
            return;
        }

        foreach (Collider col in insideSphere)
        {
            if (col.CompareTag(m_targetTag))
            {
                inSight = true;
                m_target = col.transform;
                break;
            }
            else
            {
                inSight = false;
                m_target = null;
            }
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_originalPos, m_sightRadius);
    }
}
