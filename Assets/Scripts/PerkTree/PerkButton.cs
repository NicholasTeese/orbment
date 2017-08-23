using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkButton : MonoBehaviour
{
    private bool m_bIsPurchased = false;
    private bool m_bChildPathChosen = false;

    public string m_strPerkDescription = "Default Description";

    private GameObject m_parentPerk = null;
    private List<GameObject> m_childPerks = new List<GameObject>();

    public Text m_perkDescriptionText;

    public void OnCursorOver()
    {
        m_perkDescriptionText.text = m_strPerkDescription;
    }

    private void Awake()
    {
        if (transform.parent.CompareTag("PerkButton"))
        {
            m_parentPerk = transform.parent.gameObject;
        }

        foreach (Transform child in transform)
        {
            if (child.CompareTag("PerkButton"))
            {
                m_childPerks.Add(child.gameObject);
            }
        }
    }

    public void OnClick()
    {
        if (m_parentPerk != null)
        {
            if (!m_parentPerk.GetComponent<PerkButton>().m_bIsPurchased || m_parentPerk.GetComponent<PerkButton>().m_bChildPathChosen)
            {
                Debug.Log("Parent Perk not purchased or Child Perk path already chosen.");
                return;
            }

            m_parentPerk.GetComponent<PerkButton>().m_bChildPathChosen = true;
        }

        m_bIsPurchased = true;
        gameObject.GetComponent<Image>().color = Color.red;
    }
}
