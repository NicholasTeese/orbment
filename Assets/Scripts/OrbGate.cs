using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbGate : MonoBehaviour
{
    public GUIStyle m_textStyle;
    public GameObject m_visualLock;


    private float m_origScale = 1.0f;
    private float m_lockScale = 1.0f;


    private Player m_Player;
    private Animator m_Animator;

    public int m_numOfOrbsForOpen = 10;


    public int m_currNumOrbsInvested = 0;


    private float m_holdTimer = 0.0f;
    private float m_holdDuration = 0.1f;
    private float m_orbSpendRate = 0.02f;
    private float m_orbSpendTimer = 0.0f;

    private bool m_isOpen = false;

    private bool m_playerIsNear = false;



    public void Awake()
    {
        if (m_visualLock != null)
        {
            m_origScale = m_visualLock.transform.localScale.x;
        }

        m_Player = FindObjectOfType<Player>();
        m_Animator = GetComponentInChildren<Animator>();
    }


    //public void Update() {
    //		checkIfShouldOpen();
    //}

    public void OnTriggerStay(Collider other)
    {
        //maybe check layer instead? or if it has the Player script
        if (other.gameObject == m_Player.gameObject && !m_isOpen)
        {
            m_playerIsNear = true;
            //hold e to spend orbs

            checkIfShouldOpen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if leave outside of zone
        if (other.gameObject == m_Player.gameObject)
        {
            m_playerIsNear = false;
        }
    }




    private void checkIfShouldOpen()
    {
        //if(m_isOpen)
        //{
        //    m_holdTimer += Time.deltaTime;
        //    if (m_holdTimer >= m_holdDuration)
        //    {
        //        if (m_orbSpendTimer == 0.0f)
        //        {
                    
        //        }
        //        m_orbSpendTimer += Time.deltaTime;
        //        if (m_orbSpendTimer >= m_orbSpendRate)
        //        {
        //            m_orbSpendTimer = 0.0f;
        //        }
        //    }
        //}
        if (m_Player.m_orbsCollected >= m_numOfOrbsForOpen)
        {

            SpendOrb(m_numOfOrbsForOpen);

            m_Animator.SetTrigger("OpenGate");
            if (m_visualLock != null)
            {
                m_visualLock.SetActive(false);
            }
            m_isOpen = true;
        }
    }

    private void SpendOrb( int a_num)
    {
        
        if (m_Player.m_orbsCollected > 0)
        {
     
            m_Player.m_orbsCollected -= a_num;
            m_Player.EmitSpentOrb(a_num);
            m_lockScale = 1.0f - ((float)m_currNumOrbsInvested / (float)m_numOfOrbsForOpen);
            Vector3 m_targetScale = new Vector3(m_lockScale * m_origScale, m_lockScale * m_origScale, m_visualLock.transform.localScale.z);
            m_visualLock.transform.localScale = m_targetScale;
        }

    }


}
