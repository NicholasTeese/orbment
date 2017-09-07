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

    public int m_numOfOrbsForOpen = 20;
    public int m_currNumOrbsInvested = 0;

    public float m_fDivisionRate;
    public float m_reduction;

    //private float m_holdTimer = 0.0f;
    //private float m_holdDuration = 0.1f;
    //private float m_orbSpendRate = 0.02f;
    //private float m_orbSpendTimer = 0.0f;

    public bool m_isOpen = false;

    private bool m_playerIsNear;

    public void Awake()
    {
        m_playerIsNear = false;
        if (m_visualLock != null)
        {
            m_origScale = m_visualLock.transform.localScale.x;
            m_fDivisionRate = m_visualLock.transform.localScale.x / m_numOfOrbsForOpen;
            m_reduction = 0;
        }

        m_Player = FindObjectOfType<Player>();
        m_Animator = GetComponentInChildren<Animator>();
    }

    public void OnTriggerStay(Collider other)
    {
        //maybe check layer instead? or if it has the Player script
        if (other.gameObject == m_Player.gameObject && !m_isOpen)
        {
            m_playerIsNear = true;
            //hold e to spend orbs
            if (m_playerIsNear)// && Input.GetKeyDown(KeyCode.E))
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
        if (m_isOpen)
        {
            return;
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
        }
        else if (m_Player.m_orbsCollected > 0) //>= m_numOfOrbsForOpen)
        {
            int orbsPassed = m_Player.m_orbsCollected;
            if (m_numOfOrbsForOpen > 0)
            {
                m_currNumOrbsInvested = m_currNumOrbsInvested + orbsPassed;
                m_numOfOrbsForOpen = 20 - m_currNumOrbsInvested;
                SpendOrb(m_Player.m_orbsCollected);
            }
            else
            {
                m_Animator.SetTrigger("OpenGate");
                if (m_visualLock != null)
                {
                    m_visualLock.SetActive(false);
                }
                m_isOpen = true;
            }
        }
        else if (m_numOfOrbsForOpen <= 0)
        {
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
            m_reduction = m_fDivisionRate * m_currNumOrbsInvested; //1.0f - ((float)m_currNumOrbsInvested / (float)m_numOfOrbsForOpen);
//            m_lockScale = 1.0f - ((float)m_currNumOrbsInvested / (float)m_numOfOrbsForOpen);
//            Vector3 m_targetScale = new Vector3(m_lockScale * m_origScale, m_lockScale * m_origScale, m_visualLock.transform.localScale.z);
            Vector3 m_targetScale = new Vector3(m_origScale - m_reduction, m_origScale - m_reduction, m_visualLock.transform.localScale.z);
            m_visualLock.transform.localScale = m_targetScale;
        }
    }
}