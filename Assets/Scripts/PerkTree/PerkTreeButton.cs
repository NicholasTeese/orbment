using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkTreeButton : MonoBehaviour
{
    private float m_fGrowMultiplier = 1.5f;
    private float m_fShrinkMultiplier = 1.0f;
    private float m_fGrowShrinkSpeed = 0.05f;

    private bool m_bIsHightlighted = false;
    public bool IsHighlighted { get { return m_bIsHightlighted; } set { m_bIsHightlighted = value; } }

    private Button m_perkTreeButton;

    [Header("Child Perk Tree")]
    public GameObject m_childPerkTree;

    [Header("Perk Trees")]
    public Button m_firePerkTreeButton;
    public Button m_icePerkTreeButton;
    public Button m_lightningPerkTreeButton;

    [Header("Perk Tree Description")]
    public Text m_perkCanvasDescription;

    private void Awake()
    {
        m_perkTreeButton = GetComponent<Button>();
    }

    public void OnCursorEnter(string a_strPerkTreeDescription)
    {
        m_bIsHightlighted = true;
        m_perkCanvasDescription.text = a_strPerkTreeDescription;
    }

    public void OnCursorExit()
    {
        m_bIsHightlighted = false;
    }

    public void OnClick()
    {
        if (m_firePerkTreeButton != null)
        {
            m_firePerkTreeButton.gameObject.SetActive(false);
        }

        if (m_icePerkTreeButton != null)
        {
            m_icePerkTreeButton.gameObject.SetActive(false);
        }

        if (m_lightningPerkTreeButton != null)
        {
            m_lightningPerkTreeButton.gameObject.SetActive(false);
        }

        m_childPerkTree.SetActive(true);
    }

    private void Update()
    {
        if (m_bIsHightlighted)
        {
            Grow();
        }
        else
        {
            Shrink();
        }
    }

    private void Grow()
    {
        if (m_perkTreeButton.transform.localScale.x < m_fGrowMultiplier)
        {
            m_perkTreeButton.transform.localScale += new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
    }

    private void Shrink()
    {
        if (m_perkTreeButton.transform.localScale.x > m_fShrinkMultiplier)
        {
            m_perkTreeButton.transform.localScale -= new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
    }
}
