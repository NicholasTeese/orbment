using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    private float m_fPopSpeed = 800.0f;
    private float m_fPoppedInHeight = 200.0f;
    private float m_fPoppedOutHeight = -150.0f;
    private float m_fMessageBuffer = 20.0f;

    private bool m_bPopIn = false;

    private Vector3 m_v3StartPosition = Vector3.zero;
    private Vector3 m_v3EndPosition = Vector3.zero;

    private RectTransform m_canvasRectTransform;

    private RectTransform m_messageBorder;

    public GameObject m_MessageHolder;
    public GameObject m_tutorialCanvas;

    private void Awake()
    {
        m_v3StartPosition = transform.position;

        if (m_MessageHolder != null)
        {
            m_messageBorder = m_MessageHolder.transform.Find("Border").GetComponent<RectTransform>();
        }

        if (m_tutorialCanvas != null)
        {
            m_canvasRectTransform = m_tutorialCanvas.GetComponent<RectTransform>();
        }

        m_v3EndPosition = new Vector3(m_canvasRectTransform.rect.xMin + m_messageBorder.rect.width + m_fMessageBuffer, m_canvasRectTransform.rect.yMin + m_messageBorder.rect.height + m_fMessageBuffer, 0.0f);
    }
	
	void Update()
    {
        if (PerkTreeManager.m_perkTreeManager.gameObject.activeInHierarchy ||
            PauseMenuManager.m_pauseMenuManager.gameObject.activeInHierarchy ||
            DeathMenuManager.m_deathMenuManager.gameObject.activeInHierarchy)
        {
            m_tutorialCanvas.gameObject.SetActive(false);
        }
        else
        {
            m_tutorialCanvas.gameObject.SetActive(true);
        }

		if (m_bPopIn)
        {
            PopIn();
        }
        else
        {
            PopOut();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_bPopIn = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_bPopIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_bPopIn = false;
        }
    }

    private void PopIn()
    {
        if (m_MessageHolder.transform.position.y < m_fPoppedInHeight)
        {
            m_MessageHolder.transform.position = new Vector3(m_MessageHolder.transform.position.x,
                                                             m_MessageHolder.transform.position.y + m_fPopSpeed * Time.deltaTime,
                                                             m_MessageHolder.transform.position.z);
        }
    }

    private void PopOut()
    {
        if (m_MessageHolder.transform.position.y > m_fPoppedOutHeight)
        {
            m_MessageHolder.transform.position = new Vector3(m_MessageHolder.transform.position.x,
                                                             m_MessageHolder.transform.position.y - m_fPopSpeed * Time.deltaTime,
                                                             m_MessageHolder.transform.position.z);
        }
    }
}