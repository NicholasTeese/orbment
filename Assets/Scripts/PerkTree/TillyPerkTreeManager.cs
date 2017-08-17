using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TillyPerkTreeManager : MonoBehaviour
{
    public int m_iPerkPoints;
    public int PerkPoints { get { return m_iPerkPoints; } }

    public Text perkPointText;

    public GameObject perkToActivate;

    public static TillyPerkTreeManager m_tillyPerkManager;

    // Use this for initialization
    void Start()
    {
        if (m_tillyPerkManager == null)
        {
            m_tillyPerkManager = this;
        }
        else if (m_tillyPerkManager != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        perkPointText.text = "Perk Points: " + m_iPerkPoints.ToString();
    }

    public void ActivatePerk()
    {
        //GameObject parentPerk = perkToActivate.GetComponent<TillyPerkTreeOrb>().ParentPerk;
        //List<GameObject> parentPerksChildren = null;

        //if (parentPerk != null)
        //{
        //    parentPerksChildren = parentPerk.GetComponent<TillyPerkTreeOrb>().ChildPerks;

        //    for (int iCount = 0; iCount < parentPerksChildren.Count; ++iCount)
        //    {
        //        if (parentPerksChildren[iCount].GetComponent<TillyPerkTreeOrb>().m_bPerkPurchased)
        //        {
        //            return;
        //        }
        //    }
        //}

        //perkToActivate.GetComponent<TillyPerkTreeOrb>().UnclickOrbs();
        perkToActivate.GetComponent<TillyPerkTreeOrb>().m_bPerkActivated = true;
        perkToActivate.GetComponent<TillyPerkTreeOrb>().m_bPerkPurchased = true;
        perkToActivate.GetComponent<TillyPerkTreeOrb>().PerkAvailable = false;
        m_iPerkPoints -= 1;

    }
}