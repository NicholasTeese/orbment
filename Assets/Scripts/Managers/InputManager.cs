using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------------------------
// Description:  This script is a buffer between the Unity InputManager and the rest of the game,
//               allowing a keybaord & a Xbox 360 controller to be use while the other is plugged
//               in.
// Authors:      Taliesin Millhouse
// Date Created: 5th September 2017
//------------------------------------------------------------------------------------------------

public static class InputManager
{
    /// <summary>
    /// A check to see if the left trigger is being pressed.
    /// </summary>
    private static bool m_bLeftTriggerIsPressed = false;
    /// <summary>
    /// A check to see if the right trigger is being pressed.
    /// </summary>
    private static bool m_bRightTriggerIsPressed = false;

    /// <summary>
    /// Returns the X axis on the keyboard's primary input & left thumb stick.
    /// Return value is clamped between -1 & 1.
    /// </summary>
    /// <returns></returns>
    public static float PrimaryHorizontal()
    {
        float fHorizontalAxis = 0.0f;
        fHorizontalAxis += Input.GetAxis("LeftStickXAxis");
        fHorizontalAxis += Input.GetAxis("KeyboardXAxis");
        return Mathf.Clamp(fHorizontalAxis, -1.0f, 1.0f);
    }

    /// <summary>
    /// Returns the Y axis on the keyboard's primary input & left thumb stick.
    /// Return value is clamped between -1 & 1.
    /// </summary>
    /// <returns></returns>
    public static float PrimaryVertical()
    {
        float fVerticalAxis = 0.0f;
        fVerticalAxis += Input.GetAxis("LeftStickYAxis");
        fVerticalAxis += Input.GetAxis("KeyboardYAxis");
        return Mathf.Clamp(fVerticalAxis, -1.0f, 1.0f);
    }

    /// <summary>
    /// Returns the X axis on the keyboard's secondary input & right thumb stick.
    /// Return value is clamped between -1 & 1.
    /// </summary>
    /// <returns></returns>
    public static float SecondaryHorizontal()
    {
        float fHorizontalAxis = 0.0f;
        fHorizontalAxis += Input.GetAxis("RightStickXAxis");
        return Mathf.Clamp(fHorizontalAxis, -1.0f, 1.0f);
    }

    /// <summary>
    /// Returns the Y axis on the keybaord's secondary input & right thumb stick.
    /// Return value is clamped between -1 & 1.
    /// </summary>
    /// <returns></returns>
    public static float SecondaryVertical()
    {
        float fVertialAxis = 0.0f;
        fVertialAxis += Input.GetAxis("RightStickYAxis");
        return Mathf.Clamp(fVertialAxis, -1.0f, 1.0f);
    }

    /// <summary>
    /// Returns true when the left trigger is first pressed.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Return true when the left trigger is held.
    /// </summary>
    /// <returns></returns>
    public static bool LeftTriggerHold()
    {
        if (Input.GetAxis("LeftTrigger") != 0.0f)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns true when the right trigger is first pressed.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Returns true when the right trigger is held.
    /// </summary>
    /// <returns></returns>
    public static bool RightTriggerHold()
    {
        if (Input.GetAxis("RightTrigger") != 0.0f)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns true when the A button is pressed.
    /// </summary>
    /// <returns></returns>
    public static bool AButton()
    {
        return Input.GetButtonDown("AButton");
    }

    /// <summary>
    /// Returns true when the B button is pressed.
    /// </summary>
    /// <returns></returns>
    public static bool BButton()
    {
        return Input.GetButtonDown("BButton");
    }

    /// <summary>
    /// Returns true when the X button is pressed.
    /// </summary>
    /// <returns></returns>
    public static bool XButton()
    {
        return Input.GetButtonDown("XButton");
    }

    /// <summary>
    /// Returns true when the Y button is pressed.
    /// </summary>
    /// <returns></returns>
    public static bool YButton()
    {
        return Input.GetButtonDown("YButton");
    }

    /// <summary>
    /// Returns true when the start button is pressed.
    /// </summary>
    /// <returns></returns>
    public static bool StartButton()
    {
        return Input.GetButtonDown("StartButton");
    }

    /// <summary>
    /// Returns true when the back button is pressed.
    /// </summary>
    /// <returns></returns>
    public static bool BackButton()
    {
        return Input.GetButtonDown("BackButton");
    }

    /// <summary>
    /// Returns the primary input as a Vector3.
    /// </summary>
    /// <returns></returns>
    public static Vector3 PrimaryInput()
    {
        return new Vector3(PrimaryHorizontal(), 0.0f, PrimaryVertical());
    }

    /// <summary>
    /// Returns the secondary input as a Vector3.
    /// </summary>
    /// <returns></returns>
    public static Vector3 SecondaryInput()
    {
        return new Vector3(SecondaryHorizontal(), 0.0f, SecondaryVertical());
    }
}
