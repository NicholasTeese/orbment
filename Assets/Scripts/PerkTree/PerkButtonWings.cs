using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkButtonWings : MonoBehaviour
{
    private float m_fRotationSpeed = 5.0f;

    private bool m_bRotate = true;
    public bool Rotate { get; set; }

    private void Update()
    {
        if (m_bRotate)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * m_fRotationSpeed);
        }
    }
}
