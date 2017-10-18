using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbCountDisplay : MonoBehaviour
{
    private Player m_playerRef;
    private OrbGate m_gateRef;
    private Text m_textDisplay;
    private int m_iIndex;
    private int m_iIndexEnd;
    public List<OrbGate> GateList = new List<OrbGate>();

    void Start()
    {
        m_playerRef = GameObject.FindObjectOfType<Player>();
        m_iIndex = 0;
        m_gateRef = GateList[m_iIndex]; //GameObject.FindObjectOfType<OrbGate>();
        m_iIndexEnd = GateList.Count - 1;
        m_textDisplay = GetComponent<Text>();
    }

    void Update()
    {
        if (m_gateRef.m_isOpen)
        {
            if (m_iIndex != GateList.Count - 1)
            {
                m_iIndex++;
                m_gateRef = GateList[m_iIndex];
            }
            else
            {
                m_textDisplay.text = "Door unlocked!";
            }
        }
        else
        {
            if (m_playerRef != null && m_textDisplay != null)
            {
                m_textDisplay.text = "Keys Collected " + m_playerRef.m_orbsCollected.ToString() + "/" + m_gateRef.m_numOfOrbsForOpen;
            }
        }
    }
}