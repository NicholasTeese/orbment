using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMessage : MonoBehaviour
{
    private float m_fTimer = 3.0f;

    private Text m_message;

    public List<Transform> m_messageTriggers = new List<Transform>();

    private void Awake()
    {
        m_message = GetComponent<Text>();
    }

    private void Update()
    {
        //m_fTimer -= Time.deltaTime;

        //if (m_fTimer <= 0.0f)
        //{
        //    m_message.text = "";
        //    m_fTimer = 3.0f;
        //}

        //foreach (Transform messageTrigger in m_messageTriggers)
        //{
        //    if (Vector3.Distance(messageTrigger.position, Player.m_Player.transform.position) <= 2.0f)
        //    {
        //        Debug.Log(m_message.text);
        //        m_message.text = messageTrigger.GetComponent<TutMessage>().m_strMessage;
        //    }
        //}
    }
}
