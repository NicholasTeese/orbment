using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorlMessage : MonoBehaviour
{
    private Text m_message;

    public List<Transform> m_messageTriggers = new List<Transform>();

    private void Awake()
    {
        m_message = GetComponent<Text>();
    }
}
