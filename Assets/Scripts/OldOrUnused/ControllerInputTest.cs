using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputTest : MonoBehaviour
{
    private float m_fInputBuffer = 0.2f;

    private bool m_bIsPressed = false;

	private void Update ()
    {
        Vector3 v3PrimaryInputDirection = InputManager.PrimaryInputHold();

        if (v3PrimaryInputDirection.z >= m_fInputBuffer)
        {
            if (v3PrimaryInputDirection.x <= -m_fInputBuffer)
            {
                if (!m_bIsPressed)
                {
                    m_bIsPressed = true;
                    v3PrimaryInputDirection = InputManager.PrimaryInputHold();
                }
            }
            else if (v3PrimaryInputDirection.x >= m_fInputBuffer)
            {
                if (!m_bIsPressed)
                {
                    m_bIsPressed = true;
                    v3PrimaryInputDirection = InputManager.PrimaryInputHold();
                }
            }
        }
        else if (v3PrimaryInputDirection.z <= -m_fInputBuffer)
        {
            if (v3PrimaryInputDirection.x <= -m_fInputBuffer)
            {
                if (!m_bIsPressed)
                {
                    m_bIsPressed = true;
                    v3PrimaryInputDirection = InputManager.PrimaryInputHold();
                }
            }
            else if (v3PrimaryInputDirection.x >= m_fInputBuffer)
            {
                if (!m_bIsPressed)
                {
                    m_bIsPressed = true;
                    v3PrimaryInputDirection = InputManager.PrimaryInputHold();
                }
            }
        }
        else
        {
            m_bIsPressed = false;
        }
    }
}
