using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkButtonWings : MonoBehaviour
{
    private float m_fRotationSpeed = 0.01f;

    private bool m_bRotate = true;
    public bool Rotate { get; set; }

    private void Update()
    {
        if (m_bRotate)
        {
            //Debug.Log("Rotate");
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, m_fRotationSpeed);
            m_fRotationSpeed += m_fRotationSpeed;
        }
    }
}
