using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkButtonWings : MonoBehaviour
{
    private float m_fRotationSpeed = 0.5f;

    private bool m_bRotate = false;
    public bool Rotate { get; set; }

    private void Update()
    {
        if (m_bRotate)
        {
            Debug.Log("Rotate.");
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, m_fRotationSpeed + m_fRotationSpeed);
            m_fRotationSpeed += m_fRotationSpeed;
        }
    }
}
