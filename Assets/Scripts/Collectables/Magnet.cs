using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public string m_tag;
    public float m_attraction = 100;
    private Rigidbody m_rigidBody;

    // Use this for initialization
    void Start()
    {
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

            AttractTowards(player.transform.position);
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
