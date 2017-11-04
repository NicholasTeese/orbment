using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkWings : MonoBehaviour
{
    private float m_fRotationAmount = 0.1f;
    private float m_fFirstRotationAmount = 7.0f;
    private float m_fOriginalRoationAmount;
    private float m_fFirstRotationOriginalRotationAmount;

    private bool m_bRotate = false;
    private bool m_bFirstRotation = true;

    public bool Rotate { get { return m_bRotate; } set { m_bRotate = value; } }

    private void Awake()
    {
        m_fOriginalRoationAmount = m_fRotationAmount;
        m_fFirstRotationOriginalRotationAmount = m_fFirstRotationAmount;
    }

    private void Update()
    {
        if (m_bRotate)
        {
            if (m_bFirstRotation)
            {
                transform.localRotation = Quaternion.Euler(0.0f, 0.0f, m_fFirstRotationAmount);
                m_fFirstRotationAmount -= m_fFirstRotationOriginalRotationAmount;

                if (m_fFirstRotationAmount <= -180.0f)
                {
                    m_bFirstRotation = false;
                }
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0.0f, 0.0f, m_fRotationAmount);
                m_fRotationAmount -= m_fOriginalRoationAmount;
            }
        }
    }
}
