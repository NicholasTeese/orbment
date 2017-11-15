using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    private float m_fPopSpeed = 200.0f;
    private float m_fPoppedInHeight = 200.0f;
    private float m_fPoppedOutHeight = -150.0f;
    private float m_fMessageBuffer = 400.0f;

    private bool m_bPopIn = false;

    private Vector3 m_v3StartPosition = Vector3.zero;
    private Vector3 m_v3EndPosition = Vector3.zero;

    private RectTransform m_canvasRectTransform;

    private RectTransform m_messageBorder;

    public GameObject m_MessageHolder;
    public GameObject m_tutorialCanvas;

    private void Awake()
    {
        if (m_MessageHolder != null)
        {
            m_messageBorder = m_MessageHolder.transform.Find("Border").GetComponent<RectTransform>();
        }

        if (m_tutorialCanvas != null)
        {
            m_canvasRectTransform = m_tutorialCanvas.GetComponent<RectTransform>();
        }

        m_v3StartPosition = m_MessageHolder.transform.localPosition;
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
            if (m_MessageHolder.transform.localPosition.y < m_v3EndPosition.y)
            {
                m_MessageHolder.transform.localPosition += new Vector3(0.0f, 5.0f, 0.0f) * m_fPopSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (m_MessageHolder.transform.localPosition.y > m_v3StartPosition.y)
            {
                m_MessageHolder.transform.localPosition -= new Vector3(0.0f, 5.0f, 0.0f) * m_fPopSpeed * Time.deltaTime;
            }
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
}