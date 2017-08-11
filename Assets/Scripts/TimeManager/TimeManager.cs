using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private const float m_originalTimeScale = 1.0f;
    public bool m_speedChanged = false;
    public float m_timeScale = 1.0f;
    private float m_targetSpeed = 1.0f;
    public float m_timer = 0.0f;
    private float m_duration = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        //alter time

        if(m_speedChanged)
        {
            m_timer += Time.unscaledDeltaTime;

            Time.timeScale = m_targetSpeed;

            if(m_timer >= m_duration)
            {
                Time.timeScale = m_originalTimeScale;
                m_timer = 0.0f;
                m_speedChanged = false;
            }
        }


        m_timeScale = Time.timeScale;

    }


    public void TimeScale(float a_speed, float a_duration)
    {
        if(m_speedChanged)
        {
            return;
        }

        m_targetSpeed = a_speed;
        m_duration = a_duration;

        m_speedChanged = true;
    }
}
