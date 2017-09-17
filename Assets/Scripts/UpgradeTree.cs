using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTree : MonoBehaviour
{
    private void OnTriggerStay(Collider a_collider)
    {
        if (a_collider.CompareTag("Player"))
        {
            ExpManager.m_experiencePointsManager.UpgradeTreeInRange = true;
        }
    }

    private void OnTriggerExit(Collider a_collider)
    {
        if (a_collider.CompareTag("Player"))
        {
            ExpManager.m_experiencePointsManager.UpgradeTreeInRange = false;
        }
    }
}
