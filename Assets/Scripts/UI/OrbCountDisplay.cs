using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbCountDisplay : MonoBehaviour
{
    private Player m_playerRef;
    private Text m_textDisplay;
    // Use this for initialization
    void Start()
    {
        m_playerRef = GameObject.FindObjectOfType<Player>();
        m_textDisplay = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_playerRef != null && m_textDisplay != null)
        {
            m_textDisplay.text = m_playerRef.m_orbsCollected.ToString();
        }
    }
}
