using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public string m_tag;
    public float m_attraction = 100;
    public bool m_magnetOff = false;
    private Rigidbody m_rigidBody;
    Collectable m_collectableRef;
    // Use this for initialization
    void Start()
    {
        m_collectableRef = GetComponent<Collectable>();
        m_rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(m_tag))
        {
            GameObject player = other.gameObject;
            if (this.tag == "Orb")
            {
                if (m_collectableRef != null)
                {
                    if (m_collectableRef.m_healthCap && m_collectableRef.m_type == Collectable.CollectableType.GreenOrb)
                    {
                        return;
                    }
                    if (!m_collectableRef.m_healthCap && m_collectableRef.m_type == Collectable.CollectableType.GreenOrb)
                        AttractTowards(player.transform.position);

                    if (m_collectableRef.m_manaCap && m_collectableRef.m_type == Collectable.CollectableType.BlueOrb)
                    {
                        return;
                    }
                    if (!m_collectableRef.m_manaCap && m_collectableRef.m_type == Collectable.CollectableType.BlueOrb)
                        AttractTowards(player.transform.position);

                    if (m_collectableRef.m_type == Collectable.CollectableType.YellowOrb)
                        AttractTowards(player.transform.position);
                }
            }
            else
            {
                AttractTowards(player.transform.position);
            }
        }
    }

    void AttractTowards(Vector3 a_position)
    {
        float distance = Vector3.Distance(a_position, this.transform.position);
        Vector3 dir = a_position - this.transform.position;
        dir.Normalize();

        Vector3 forceVect = Vector3.zero;
        if (distance > 0.5f)
        {
            m_rigidBody.velocity = Vector3.zero;
            forceVect = (dir * m_attraction) / (distance * distance);
            m_rigidBody.AddForce(forceVect, ForceMode.Acceleration);
            
        }
    }
}