using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveScatter : MonoBehaviour
{
    public BreakOnImpactWith m_script;
    public float m_explosiveForce = 100;
    public float m_explosiveRadius = 10;
    public float m_explosiveUpMod = 1;
    public Transform m_explsionPosition;
    private Rigidbody m_rb;
   
    // Use this for initialization
    void Start()
    {
        
    }
    
    void OnEnable()
    {
        m_rb = this.GetComponent<Rigidbody>();
        if (m_script != null && m_rb != null && m_explsionPosition != null)
        {
            m_rb.AddExplosionForce(m_explosiveForce, m_explsionPosition.position, m_explosiveRadius, m_explosiveUpMod);
        }
    }
}
