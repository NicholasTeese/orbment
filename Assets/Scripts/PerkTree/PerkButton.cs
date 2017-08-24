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

    private StartingWeapon m_startingWeapon;

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

        m_startingWeapon = GameObject.FindGameObjectWithTag("StartingWeapon").GetComponent<StartingWeapon>();
    }

    private void PurchasePerk(string a_strPerk)
    {
        switch (a_strPerk)
        {
            case "FireBullet":
                {
                    m_startingWeapon.SetProjectile(Resources.Load("Prefabs/Projectiles/FireBall") as GameObject);
                    break;
                }

            case "IceBullet":
                {
                    m_startingWeapon.SetProjectile(Resources.Load("Prefabs/Projectiles/IceShard") as GameObject);
                    break;
                }

            case "LightningBullet":
                {
                    m_startingWeapon.SetProjectile(Resources.Load("Prefabs/Projectiles/LightningBall") as GameObject);
                    break;
                }

            default:
                {
                    Debug.Log("Perk name to be purchased not recognised.");
                    break;
                }
        }
    }

    public void OnClick(string a_strPerk)
    {
        if (PerkTreeManager.m_perkTreeManager.AvailiablePerks == 0)
        {
            Debug.Log("No availiable perks to spend.");
            return;
        }

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
        PerkTreeManager.m_perkTreeManager.DecrementAvailiablePerks();
        PurchasePerk(a_strPerk);
    }
}
