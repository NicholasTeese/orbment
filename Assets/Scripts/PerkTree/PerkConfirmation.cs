using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkConfirmation : MonoBehaviour
{
    private Transform m_perkUpgradeConfirmation;

    private void Awake()
    {
        m_perkUpgradeConfirmation = transform.parent;
    }

    public void OnClick(string a_strConfirmation)
    {
        switch (a_strConfirmation)
        {
            case "YES":
                {
                    PerkTreeManager.m_perkTreeManager.m_selectedPerkButton.PurchasePerk();
                    m_perkUpgradeConfirmation.gameObject.SetActive(false);
                    break;
                }

            case "NO":
                {
                    m_perkUpgradeConfirmation.gameObject.SetActive(false);
                    break;
                }

            default:
                {
                    Debug.Log("Confirmation not recognised: " + a_strConfirmation);
                    break;
                }
        }
    }
}
