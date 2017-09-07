using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkWings : MonoBehaviour
{
    private float m_fRotationAmount = 0.1f;
    private float m_fOriginalRoationAmount;

    private bool m_bRotate = false;
    public bool Rotate { get { return m_bRotate; } set { m_bRotate = value; } }

    private void Awake()
    {
        m_fOriginalRoationAmount = m_fRotationAmount;
    }

    private void Update()
    {
        if (m_bRotate)
        {
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, m_fRotationAmount);
            m_fRotationAmount += m_fOriginalRoationAmount;
        }
    }
}
