using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkButtonWings : MonoBehaviour
{
    private float m_fRotationSpeed = 5.0f;

    private PerkButton m_perkButton;

    private void Awake()
    {
        m_perkButton = transform.GetComponentInParent<PerkButton>();
    }

    private void Update()
    {
        if (m_perkButton.IsPurchased)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * m_fRotationSpeed);
        }
    }
}
