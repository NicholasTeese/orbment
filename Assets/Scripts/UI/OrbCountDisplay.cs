using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbCountDisplay : MonoBehaviour
{
    private Player m_playerRef;
    private OrbGate m_gateRef;
    private Text m_textDisplay;

    void Start()
    {
        m_playerRef = GameObject.FindObjectOfType<Player>();
        m_gateRef = GameObject.FindObjectOfType<OrbGate>();
        m_textDisplay = this.GetComponent<Text>();
    }

    void Update()
    {
        if (m_gateRef.m_isOpen)
        {
            m_textDisplay.text = "Door unlocked!";
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