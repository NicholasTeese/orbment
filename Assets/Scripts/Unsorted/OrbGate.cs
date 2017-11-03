using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbGate : MonoBehaviour
{
    public GUIStyle m_textStyle;
    public GameObject m_visualLock;
    public int m_iIndexTracker;

    private float m_origScale = 1.0f;
    private float m_lockScale = 1.0f;

    private Player m_Player;
    //private Animator m_Animator;
    public Animator m_DoorLeftAnimator;
    public Animator m_DoorRightAnimator;


    private int m_numOfOrbsForOpen;
    public int m_currNumOrbsInvested = 0;
    private int m_iTotalOrbs;

    public float m_fDivisionRate;
    public float m_reduction;

    public bool m_isOpen = false;

    private bool m_playerIsNear;

    private GameObject m_orbSlot;

    [Header("Enemy sections that open this door")]
    public List<GameObject> m_enemySections = new List<GameObject>();

    public int NumberOfOrbsToOpen { get { return m_numOfOrbsForOpen; } }

    public void Awake()
    {
        m_playerIsNear = false;
        m_numOfOrbsForOpen = CalculateNumberOfOrbsToOpen(m_enemySections);

        if (m_visualLock != null)
        {
            m_origScale = m_visualLock.transform.localScale.x;
            m_fDivisionRate = m_visualLock.transform.localScale.x / m_numOfOrbsForOpen;
            m_reduction = 0;
            m_iTotalOrbs = m_numOfOrbsForOpen;
        }

        m_Player = FindObjectOfType<Player>();
        //m_Animator = GetComponentInChildren<Animator>();
        
        m_orbSlot = transform.Find("OrbSlot").gameObject;
    }

    public void OnTriggerStay(Collider other)
    {
        //maybe check layer instead? or if it has the Player script
        if (other.gameObject == Player.m_player.gameObject && !m_isOpen)
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

    private int CalculateNumberOfOrbsToOpen(List<GameObject> a_enemySections)
    {
        int iOrbsToOpen = 0;

        for (int iCount = 0; iCount < a_enemySections.Count; ++iCount)
        {
            iOrbsToOpen += a_enemySections[iCount].transform.childCount;
        }

        return iOrbsToOpen;
    }

    private void checkIfShouldOpen()
    {
//        m_DoorLeft.transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);

        if (m_isOpen)
        {
            return;
        }
        else if (m_Player.m_orbsCollected > 0) //>= m_numOfOrbsForOpen)
        {
            int orbsPassed = m_Player.m_orbsCollected;
            if (m_numOfOrbsForOpen > 0)
            {
                m_currNumOrbsInvested = m_currNumOrbsInvested + orbsPassed;
                m_numOfOrbsForOpen = m_iTotalOrbs - m_currNumOrbsInvested;
                SpendOrb(m_Player.m_orbsCollected);
            }
            else
            {
                //m_Animator.SetTrigger("OpenGate");
                m_DoorLeftAnimator.SetTrigger("Open");
                m_DoorRightAnimator.SetTrigger("Open");

                if (m_visualLock != null)
                {
                    m_visualLock.SetActive(false);
                }
                m_isOpen = true;
                //Destroy(m_orbSlot, 0.5f);
            }
        }
        else if (m_numOfOrbsForOpen <= 0)
        {
            //m_Animator.SetTrigger("OpenGate");
            m_DoorLeftAnimator.SetTrigger("Open");
            m_DoorRightAnimator.SetTrigger("Open");

            if (m_visualLock != null)
            {
                m_visualLock.SetActive(false);
            }
            m_isOpen = true;
            //Destroy(m_orbSlot, 0.5f);
        }
    }

    private void SpendOrb( int a_num)
    {
        if (m_Player.m_orbsCollected > 0)
        {
            m_Player.m_orbsCollected -= a_num;
            m_Player.EmitSpentOrb(a_num);
            m_reduction = m_fDivisionRate * m_currNumOrbsInvested;
            Vector3 m_targetScale = new Vector3(m_origScale - m_reduction, m_origScale - m_reduction, m_visualLock.transform.localScale.z);
            m_visualLock.transform.localScale = m_targetScale;
        }
    }
}