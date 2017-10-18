using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbCountDisplay : MonoBehaviour
{
    private Player m_playerRef;
    private Text m_textDisplay;
    private int m_iIndex;
    public List<OrbGate> GateList = new List<OrbGate>();

    void Start()
    {
        m_playerRef = GameObject.FindObjectOfType<Player>();
        m_iIndex = 0;
        m_textDisplay = GetComponent<Text>();
    }

    void Update()
    {
        if (GateList[m_iIndex].m_isOpen)
        {
              if (m_iIndex != GateList.Count - 1)
              {
                m_iIndex++;
              }
        }
        else
        {
            if (m_playerRef != null && m_textDisplay != null)
            {
                m_textDisplay.text = "Keys Collected " + m_playerRef.m_orbsCollected.ToString() + "/" + GateList[m_iIndex].m_numOfOrbsForOpen;
            }
        }
    }

    public void ResetIndex()
    {
        m_iIndex = 0;
    }
}