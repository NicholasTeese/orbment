using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TillyPerkTreeOrb : MonoBehaviour
{
    private int m_iPositionAmount;

    public bool m_bPerkActivated = false;
    public bool m_bPerkPurchased = false;

    private List<Vector3> m_linePositions;

    public GameObject m_perkChild;
    public GameObject m_perkTreeOrbs;

    private GameObject[] m_perkOrbs;

    public List<GameObject> m_branchLengths;

    private LineRenderer m_lineRenderer;

    /// <summary>
    /// Drawing lines between perks to make editing them easier.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (m_branchLengths[1].gameObject != this.gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, m_branchLengths[1].transform.position);
            m_perkOrbs = GameObject.FindGameObjectsWithTag("perkOrb");
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, m_branchLengths[2].transform.position);
        }
    }

    private void Awake()
    {
        m_perkTreeOrbs = GameObject.Find("PerkTreeOrbs");

        m_lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        m_perkOrbs = GameObject.FindGameObjectsWithTag("perkOrbs");

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
}
