using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillStreakManager : MonoBehaviour
{
    public Text m_display;
    public Text m_descriptionDisplay;

    public string[] m_descriptions;
    public Color[] m_colours;

    public int m_killStreak = 0;
    public float m_timeAllowedBetweenKills = 1.0f;
    private float m_timeOfLastKill = 0.0f;

    public void ResetKillStreak()
    {
        m_killStreak = 0;
        m_timeOfLastKill = 0.0f;
    }

    public void AddKill()
    {
        if(CheckKill())
        {
            m_killStreak++;
            m_timeOfLastKill = Time.time;
        }



    }

    public bool CheckKill()
    {
        return (m_timeOfLastKill == 0.0f || Time.time - m_timeOfLastKill <= m_timeAllowedBetweenKills);
    }

    private void Update()
    {
        if(m_display != null)
        {
            m_display.text = "x" + m_killStreak.ToString();
        }

        if(m_descriptionDisplay != null && m_killStreak < m_descriptions.Length)
        {
            m_descriptionDisplay.text = m_descriptions[m_killStreak];
            if(m_killStreak < m_colours.Length)
            {
                m_display.color = m_colours[m_killStreak];
                m_descriptionDisplay.color = m_colours[m_killStreak];
            }
        }

        if(!CheckKill())
        {
            ResetKillStreak();
        }
    }
}
