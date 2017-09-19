using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutMessage : MonoBehaviour
{
    public string m_strMessage;

    public Text m_message;

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.name);
        if (other.GetComponentInParent<Transform>().name == "PlayerAlpha")
        {
            Debug.Log("Hit.");
            m_message.text = m_strMessage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.name);
        if (other.GetComponentInParent<Transform>().name == "PlayerAlpha")
        {
            Debug.Log("Hit.");
            m_message.text = "";
        }
    }
}
