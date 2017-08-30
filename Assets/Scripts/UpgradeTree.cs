using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTree : MonoBehaviour
{
    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameManager.m_GameManager.inRange = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameManager.m_GameManager.inRange = false;
        }
    }
}
