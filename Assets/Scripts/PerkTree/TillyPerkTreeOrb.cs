using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TillyPerkTreeOrb : MonoBehaviour
{
    private int m_iPositionAmount;

    public bool m_bPerkActivated = false;
    public bool m_bPerkPurchased = false;
    private bool m_bPerkAvailable = false;
    public bool PerkAvailable { get { return m_bPerkAvailable; } set { m_bPerkAvailable = value; } }

    private List<Vector3> m_linePositions;

    public GameObject m_perkChild;
    public GameObject m_perkTreeOrbs;

    private GameObject m_parentPerk;
    public GameObject ParentPerk { get { return m_parentPerk; } }

    private List<GameObject> m_childPerks;
    public List<GameObject> ChildPerks { get { return m_childPerks; } }

    private GameObject[] m_perkOrbs;

    public List<GameObject> m_branchLengths;

    private LineRenderer m_lineRenderer;

    /// <summary>
    /// Drawing lines between perks to make editing them easier.
    /// </summary>
    //private void OnDrawGizmos()
    //{
    //    if (m_branchLengths[1].gameObject != this.gameObject)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine(transform.position, m_branchLengths[1].transform.position);
    //        m_perkOrbs = GameObject.FindGameObjectsWithTag("perkOrb");
    //    }
    //    else
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine(transform.position, m_branchLengths[2].transform.position);
    //    }
    //}

    /// <summary>
    /// When the mouse is clicked this enables new perk tree branch.
    /// </summary>
    private void OnMouseDown()
    {
        // If there aren't any perk points or the perk is already purchased or the perk is not available to use exit the function.
        if (TillyPerkTreeManager.m_tillyPerkManager.PerkPoints <= 0 || m_bPerkPurchased || !m_bPerkAvailable)
        {
            return;
        }

        // Initialise a list for the parent perk's children.
        List<GameObject> parentPerksChildren = null;

        // If there is a parent perk.
        if (m_parentPerk != null)
        {
            // Get it's children.
            parentPerksChildren = m_parentPerk.GetComponent<TillyPerkTreeOrb>().ChildPerks;

            // Iterate through the children and check if one has been already purchased.
            for (int iCount = 0; iCount < parentPerksChildren.Count; ++iCount)
            {
                if (parentPerksChildren[iCount].GetComponent<TillyPerkTreeOrb>().m_bPerkPurchased)
                {
                    // If so exit the function as the perk cannot be selected.
                    return;
                }
            }
        }

        // If the perk has a parent perk and it has not been purchased exit the function.
        if (m_parentPerk != null && !m_parentPerk.GetComponent<TillyPerkTreeOrb>().m_bPerkPurchased)
        {
            return;
        }

        // Send this perk the PerkTreeManager to be activated set the confirmation buttons to be active.
        TillyPerkTreeManager.m_tillyPerkManager.perkToActivate = gameObject;
        GameObject.Find("PerkTreeCanvas").transform.GetChild(0).gameObject.SetActive(true);
    }

    /// <summary>
    /// Shows the description of the perk.
    /// </summary>
    private void OnMouseOver()
    {
        GameObject.Find("PerkTreeCanvas").transform.GetChild(1).GetComponentInChildren<Text>().GetComponent<Text>().text = transform.GetChild(0).GetComponent<Text>().text;
    }

    private void Awake()
    {
        m_perkTreeOrbs = GameObject.Find("PerkTreeOrbs");

        m_parentPerk = InitialiseParentPerk();
        m_childPerks = InitialiseChildPerks();

        m_lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (m_parentPerk != null)
        {
            if (m_parentPerk.GetComponent<TillyPerkTreeOrb>().m_bPerkActivated)
            {
                m_bPerkAvailable = true;
            }

        }

        m_perkOrbs = GameObject.FindGameObjectsWithTag("perkOrb");

        // If an orb is activated, create a link down the branch.
        if (m_bPerkActivated)
        {
            // Turn on the glow for each perk in the branch.
            foreach (GameObject childPerk in m_branchLengths)
            {
                childPerk.transform.GetChild(0).gameObject.SetActive(true);
                childPerk.transform.GetChild(1).gameObject.SetActive(true);
            }

            m_lineRenderer.enabled = true;

            m_lineRenderer.numPositions = m_branchLengths.Count;
            
            m_iPositionAmount = m_lineRenderer.numPositions;

            // Set LineRenderer's position for each orb in the branch.
            for (int iCount = 0; iCount < m_iPositionAmount; ++iCount)
            {
                m_lineRenderer.SetPosition(iCount, m_branchLengths[iCount].transform.position);
            }
        }
        else
        {
            m_lineRenderer.enabled = false;
        }
    }

    private GameObject InitialiseParentPerk()
    {
        GameObject parentOrb = transform.parent.gameObject;
        
        if (!parentOrb.CompareTag("perkOrb"))
        {
            parentOrb = null;
            m_bPerkAvailable = true;
        }

        return parentOrb;
    }

    private List<GameObject> InitialiseChildPerks()
    {
        List<GameObject> childPerks = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("perkOrb"))
            {
                childPerks.Add(child.gameObject);
            }
        }

        if (childPerks.Count == 0)
        {
            return null;
        }

        return childPerks;
    }

    public void UnclickOrbs()
    {
        foreach (GameObject orb in m_perkOrbs)
        {
            orb.transform.GetChild(0).gameObject.SetActive(false);
            orb.GetComponent<TillyPerkTreeOrb>().m_bPerkActivated = false;
        }
    }
}
