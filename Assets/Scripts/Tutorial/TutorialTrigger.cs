using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject m_MessageHolder;

	void Awake()
    {

    }
	
	void Update()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (m_MessageHolder != null && TutorialManager.m_TutorialManager != null)
        {
            m_MessageHolder.SetActive(true);
            TutorialManager.m_TutorialManager.WindowOpen = true;
        }
        else
        {
            Debug.Log("Message holder = " + m_MessageHolder);
            Debug.Log("Tutorial Manager = " + TutorialManager.m_TutorialManager);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.gameObject.SetActive(false);
    }
}