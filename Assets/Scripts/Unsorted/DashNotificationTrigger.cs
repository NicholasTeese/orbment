using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashNotificationTrigger : MonoBehaviour
{
    private float m_fDestroyTimer = 4.0f;

    public GameObject m_dashNotificationText;

    bool m_bTriggered = false;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_dashNotificationText.SetActive(true);
            m_bTriggered = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_dashNotificationText.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (m_bTriggered)
        {
            m_fDestroyTimer -= Time.deltaTime;

            if (m_fDestroyTimer <= 0)
            {
                m_dashNotificationText.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
