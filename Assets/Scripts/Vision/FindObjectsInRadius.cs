using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObjectsInRadius : MonoBehaviour
{

    public string m_targetTag;
    public float m_sightRadius = 20;
    public float m_sightAngle = 45;
    public float m_attackRange = 1.5f;

    public bool inRange = false;
    public bool inSight = false;
    private Vector3 direction;
    private Vector3 forwardPoint;
    private Vector3 leftPoint;
    private Vector3 rightPoint;
    private Collider[] insideSphere;
    public Transform m_target;
    public bool m_manualSetTarget;
    // Use this for initialization
    void Start()
    {
        direction = this.transform.forward;
    }

    //maybe turn this into a call so it's not run every frame?
    private void FixedUpdate()
    {
        insideSphere = Physics.OverlapSphere(this.transform.position, m_sightRadius);


        if (m_targetTag == "")
        {
            return;
        }

  


        foreach (Collider col in insideSphere)
        {
            if (col.CompareTag(m_targetTag))
            {


                direction = col.transform.position - this.transform.position;
                direction.y = 0;
                direction.Normalize();

                float angle = Vector3.Angle(this.transform.forward, direction);
                if (angle < m_sightAngle)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(this.transform.position, direction * m_sightRadius, out hit))
                    {
                        if (hit.collider.CompareTag(m_targetTag))
                        {
                            inSight = true;

                            m_target = hit.collider.transform;

                            
                        }
                        else
                        {
                            inSight = false;

                            m_target = null;
                            
                        }
                    }
                    else
                    {

                        inSight = false;
                        m_target = null;
                        
                    }

                }
                else
                {

                    inSight = false;
                    m_target = null;
                   
                }



            }

        }




        if(m_target != null)
        {
            float distance = Vector3.Distance(this.transform.position, m_target.position);

            if(distance <= m_attackRange)
            {
                inRange = true;

            }
            else
            {
                inRange = false;
            }
        }
        else
        {
            inRange = false;
        }




    }

    void OnDrawGizmos()
    {

        Vector3 leftLine = Quaternion.Euler(this.transform.up * m_sightAngle) * this.transform.forward;
        Vector3 rightLine = Quaternion.Euler(this.transform.up * -m_sightAngle) * this.transform.forward;


        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(this.transform.position, m_sightRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + leftLine * m_sightRadius);
        Gizmos.DrawLine(this.transform.position, this.transform.position + rightLine * m_sightRadius);


    }

}
