using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputTest : MonoBehaviour
{
	private void Update ()
    {
		if (InputManager.AButton())
        {
            //Debug.Log(InputManager.PrimaryInput());
        }

        if (InputManager.BButton())
        {
            Debug.Log("B");
        }

        if (InputManager.XButton())
        {
            Debug.Log("X");
        }

        if (InputManager.YButton())
        {
            Debug.Log("Y");
        }

        //if (InputManager.PrimaryInput() != Vector3.zero)
        //{
        //    Debug.Log(InputManager.PrimaryInput());
        //}
    }
}
