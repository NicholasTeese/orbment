using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ProtectorVision : MonoBehaviour
{
    
    public float m_sightRadius = 1000.0f;
    //private NavMeshAgent m_agent;

    private Collider[] insideSphere;
    public Transform m_leader;
    //public Transform m_target;
    // Use this for initialization
    public bool leaderInSight = false;
 
    
    void Start()
    {
        //m_agent = this.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        insideSphere = Physics.OverlapSphere(this.transform.position, m_sightRadius);
        float closest = Mathf.Infinity;
        foreach (Collider col in insideSphere)
        {
            //if ally and not a protector
            if (col.CompareTag("Enemy") && col.transform != this.transform && col.gameObject.GetComponent<ProtectorVision>() == null)
            {
                float distance = Vector3.Distance(this.transform.position, col.transform.position);

                if(distance < closest)
                {
                    closest = distance;
                    leaderInSight = true;
                    m_leader = col.transform;
                    
                }
                
               
            }

        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, m_sightRadius);
    }
}
