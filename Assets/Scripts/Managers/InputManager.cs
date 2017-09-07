using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    private static bool m_bPrimaryInputIsPressed = false;
    private static bool m_bLeftTriggerIsPressed = false;
    private static bool m_bRightTriggerIsPressed = false;

    public static float PrimaryHorizontal()
    {
        float fHorizontalAxis = 0.0f;
        fHorizontalAxis += Input.GetAxis("LeftStickXAxis");
        fHorizontalAxis += Input.GetAxis("KeyboardXAxis");
        return Mathf.Clamp(fHorizontalAxis, -1.0f, 1.0f);
    }

    public static float PrimaryVertical()
    {
        float fVerticalAxis = 0.0f;
        fVerticalAxis += Input.GetAxis("LeftStickYAxis");
        fVerticalAxis += Input.GetAxis("KeyboardYAxis");
        return Mathf.Clamp(fVerticalAxis, -1.0f, 1.0f);
    }

    public static float SecondaryHorizontal(float a_fMouseXAxis)
    {
        float fHorizontalAxis = 0.0f;
        fHorizontalAxis += Input.GetAxis("RightStickXAxis");
        fHorizontalAxis += a_fMouseXAxis;
        return Mathf.Clamp(fHorizontalAxis, -1.0f, 1.0f);
    }

    public static float SecondaryVertical(float a_fMouseYAxis)
    {
        float fVertialAxis = 0.0f;
        fVertialAxis += Input.GetAxis("RightStickYAxis");
        fVertialAxis += a_fMouseYAxis;
        return Mathf.Clamp(fVertialAxis, -1.0f, 1.0f);
    }

    public static bool LeftTriggerDown()
    {
        if (Input.GetAxis("LeftTrigger") != 0.0f)
        {
            if (!m_bLeftTriggerIsPressed)
            {
                m_bLeftTriggerIsPressed = true;
                return true;
            }
            return false;
        }

        m_bLeftTriggerIsPressed = false;
        return false;
    }

    public static bool LeftTriggerHold()
    {
        if (Input.GetAxis("LeftTrigger") != 0.0f)
        {
            return true;
        }
        return false;
    }

    public static bool RightTriggerDown()
    {
        if (Input.GetAxis("RightTrigger") != 0.0f)
        {
            if (!m_bRightTriggerIsPressed)
            {
                m_bRightTriggerIsPressed = true;
                return true;
            }
            return false;
        }

        m_bRightTriggerIsPressed = false;
        return false;
    }

    public static bool RightTriggerHold()
    {
        if (Input.GetAxis("RightTrigger") != 0.0f)
        {
            return true;
        }
        return false;
    }

    public static bool AButton()
    {
        return Input.GetButtonDown("AButton");
    }

    public static bool BButton()
    {
        return Input.GetButtonDown("BButton");
    }

    public static bool XButton()
    {
        return Input.GetButtonDown("XButton");
    }

    public static bool YButton()
    {
        return Input.GetButtonDown("YButton");
    }

    public static bool StartButton()
    {
        return Input.GetButtonDown("StartButton");
    }

    public static bool BackButton()
    {
        return Input.GetButtonDown("BackButton");
    }

    public static Vector3 PrimaryInputDown()
    {
        Debug.Log(PrimaryInputHold());
        if (PrimaryInputHold() != Vector3.zero)
        {
            if (!m_bPrimaryInputIsPressed)
            {
                m_bPrimaryInputIsPressed = true;
                //Debug.Log(PrimaryInputHold());
                //Debug.Log(m_bPrimaryInputIsPressed);
                return PrimaryInputHold();
            }
            //m_bPrimaryInputIsPressed = false;
            //Debug.Log(m_bPrimaryInputIsPressed);
            return Vector3.zero;
        }

        m_bPrimaryInputIsPressed = false;
        Debug.Log(m_bPrimaryInputIsPressed);
        return Vector3.zero;
    }

    public static Vector3 PrimaryInputHold()
    {
        return new Vector3(PrimaryHorizontal(), 0.0f, PrimaryVertical());
    }

    public static Vector3 SecondaryInputHold(Vector3 a_v3MouseInput)
    {
        return new Vector3(SecondaryHorizontal(a_v3MouseInput.x), 0.0f, SecondaryVertical(a_v3MouseInput.z));
    }
}
