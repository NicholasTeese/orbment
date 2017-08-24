using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TillyPerkTreeOrb : MonoBehaviour
{
    /// <summary>
    /// How far down the Perk tree this Perk is.
    /// </summary>
    private int m_iPositionAmount;

    /// <summary>
    /// A check for if this Perk is activated or not.
    /// </summary>
    public bool m_bPerkActivated = false;
    /// <summary>
    /// A check for if this Perk is purchased or not.
    /// </summary>
    public bool m_bPerkPurchased = false;
    /// <summary>
    /// A check for if this Perk is available or not.
    /// </summary>
    private bool m_bPerkAvailable = false;
    public bool PerkAvailable { get { return m_bPerkAvailable; } set { m_bPerkAvailable = value; } }

    /// <summary>
    /// The description of this Perk.
    /// </summary>
    public string m_perkDescription;

    /// <summary>
    /// The spend perk confirmation buttons.
    /// </summary>
    public GameObject m_spendPerkConfirmation;

    /// <summary>
    /// This Perk's parent Perk if it has one.
    /// </summary>
    private GameObject m_parentPerk;
    public GameObject ParentPerk { get { return m_parentPerk; } }

    /// <summary>
    /// A list of this Perk's child Perks if there are any.
    /// </summary>
    private List<GameObject> m_childPerks;
    public List<GameObject> ChildPerks { get { return m_childPerks; } }

    /// <summary>
    /// A List of the parent Perks for this Perk.
    /// </summary>
    public List<GameObject> m_branchLengths;

    /// <summary>
    /// The Text that displays the moused over Perk's description.
    /// </summary>
    public Text m_mouseOverPerkDescriptionText;

    /// <summary>
    /// This Perk's LineRenderer.
    /// </summary>
    private LineRenderer m_lineRenderer;

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
        m_spendPerkConfirmation.SetActive(true);
    }

    /// <summary>
    /// On mouse over of a perk this shows the description of the perk.
    /// </summary>
    private void OnMouseOver()
    {
        m_mouseOverPerkDescriptionText.text = m_perkDescription;
    }

    /// <summary>
    /// Intitialise remaining variables and instances.
    /// </summary>
    private void Awake()
    {
        m_parentPerk = InitialiseParentPerk();
        m_childPerks = InitialiseChildPerks();

        m_lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Update PerkTreeOrb logic.
    /// </summary>
    private void Update()
    {
        // If the current perk has a parent perk & it is activated, set the current perk available to be activated.
        if (m_parentPerk != null)
        {
            if (m_parentPerk.GetComponent<TillyPerkTreeOrb>().m_bPerkActivated)
            {
                m_bPerkAvailable = true;
            }

        }

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
            m_lineRenderer.positionCount = m_branchLengths.Count;
            m_iPositionAmount = m_lineRenderer.positionCount;

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

    /// <summary>
    /// Gets the parent perk for the current perk if there is one.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Gets the child perks for the current perk if there are any.
    /// </summary>
    /// <returns></returns>
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
}
