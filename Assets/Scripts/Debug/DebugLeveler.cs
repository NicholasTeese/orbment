using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLeveler : MonoBehaviour
{
    private ExpManager m_xpManager;
    public float m_XP_Per_Hit = 10;
    // Use this for initialization
    void Start()
    {
        m_xpManager = GameObject.FindObjectOfType<ExpManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Bullet"))
        {
            m_xpManager.m_playerExperience += m_XP_Per_Hit;
        }
    }

}
