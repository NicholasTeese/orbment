using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbGate : MonoBehaviour
{
    public GameObject m_portal;

    public GUIStyle m_textStyle;
    public GameObject m_visualLock;
    public int m_iIndexTracker;

    private float m_fOriginalScale = 1.0f;

    public Animator m_DoorLeftAnimator;
    public Animator m_DoorRightAnimator;

    private int m_iNumberOfOrbsToUnlock;
    public int m_iCurrentNumberOfOrbsCollected = 0;
    private int m_iTotalOrbs;

    public float m_fDivisionRate;
    public float m_reduction;

    public bool m_bUnlocked = false;

    private GameObject m_orbSlot;

    [Header("Enemy sections that open this door")]
    public List<GameObject> m_enemySections = new List<GameObject>();

    public int NumberOfOrbsToOpen { get { return m_iNumberOfOrbsToUnlock; } }

    public void Awake()
    {
        m_iNumberOfOrbsToUnlock = CalculateNumberOfOrbsToOpen(m_enemySections);

        if (m_visualLock != null)
        {
            m_fOriginalScale = m_visualLock.transform.localScale.x;
            m_fDivisionRate = m_visualLock.transform.localScale.x / m_iNumberOfOrbsToUnlock;
            m_reduction = 0;
        }
        m_iTotalOrbs = m_iNumberOfOrbsToUnlock;

        m_orbSlot = transform.Find("OrbSlot").gameObject;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !m_bUnlocked)
        {
            CollectOrbs();
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

    private void CollectOrbs()
    {
        if (Player.m_player.m_orbsCollected > 0)
        {
            int iOrbsToCollect = Player.m_player.m_orbsCollected;
            m_iCurrentNumberOfOrbsCollected += iOrbsToCollect;
            m_iNumberOfOrbsToUnlock = m_iTotalOrbs - m_iCurrentNumberOfOrbsCollected;
            ReturnOrbs(Player.m_player.m_orbsCollected);
        }

        if (m_iNumberOfOrbsToUnlock <= 0)
        {
            m_bUnlocked = true;

            if (m_DoorLeftAnimator != null)
            {
                m_DoorLeftAnimator.SetTrigger("Open");
                m_DoorRightAnimator.SetTrigger("Open");
            }

            if (m_visualLock != null)
            {
                m_visualLock.SetActive(false);
            }

            if (m_portal != null)
            {
                m_portal.SetActive(true);
            }
        }
    }

    private void ReturnOrbs(int a_iOrbsToCollect)
    {
        if (Player.m_player.m_orbsCollected > 0)
        {
            Player.m_player.m_orbsCollected -= a_iOrbsToCollect;
            Player.m_player.EmitSpentOrb(a_iOrbsToCollect);
            m_reduction = m_fDivisionRate * m_iCurrentNumberOfOrbsCollected;

            if (m_visualLock != null)
            {
                Vector3 m_targetScale = new Vector3(m_fOriginalScale - m_reduction, m_fOriginalScale - m_reduction, m_visualLock.transform.localScale.z);
                m_visualLock.transform.localScale = m_targetScale;
            }
        }
    }
}
