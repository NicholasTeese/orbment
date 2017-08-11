using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// this script handles the 3 perks appearing and deals with the user interaction with those perks
/// </summary>
public class LevelUpUI : MonoBehaviour
{

    private const int NUM_OF_PERKS = 3;
    [Tooltip("Invoked when on of the ui elements were pressed")]
    public UnityEvent[] m_PerkClicked = new UnityEvent[NUM_OF_PERKS];

    public GameObject[] m_classSelectObjects;
    private PerkUI[] m_classSelect = new PerkUI[NUM_OF_PERKS];
    private PerkUI[] m_PerkUIElements = new PerkUI[NUM_OF_PERKS];

    private PerkManager m_PerkManager;

    public static LevelUpUI m_Singleton;

    // Use this for initialization
    void Awake()
    {
        if (m_Singleton != null)
        {
            Debug.LogWarning("There are two separate level up UI's");
        }
        m_Singleton = this;

        m_PerkManager = FindObjectOfType<PerkManager>();

        getPerkUI();
        addPerkListerners();
        //hideUI();
        setPerkSelectUI(false);
    }

    /// <summary>
    /// gets buttons from children
    /// </summary>
    private void getPerkUI()
    {
        Transform buttonParent = transform.GetChild(0);//PerkButtons
        for (int i = 0; i < NUM_OF_PERKS; i++)
        {
            m_PerkUIElements[i] = buttonParent.GetChild(i).GetChild(0).GetComponent<PerkUI>();
            if (m_PerkUIElements[i] != null)
            {
                m_PerkUIElements[i].setPerkIndex(i);
            }
        }
        Transform classParent = transform.GetChild(1);
        for (int i = 0; i < NUM_OF_PERKS; i++)
        {
            m_classSelect[i] = classParent.GetChild(i).GetChild(0).GetComponent<PerkUI>();
            if (m_classSelect[i] != null)
            {
                m_classSelect[i].setPerkIndex(i);
            }
        }
    }

    /// <summary>
    /// adds listerners to buttons
    /// </summary>
    private void addPerkListerners()
    {
        for (int i = 0; i < NUM_OF_PERKS; i++)
        {
            if (m_PerkUIElements[i] != null)
            {
                m_PerkUIElements[i].m_PerkClicked.AddListener(perkButtonClicked);
            }
        }
        for (int i = 0; i < NUM_OF_PERKS; i++)
        {
            if (m_classSelect[i] != null)
            {
                m_classSelect[i].m_PerkClicked.AddListener(perkButtonClicked);
            }
        }
    }

    private void perkButtonClicked(int a_ButtonIndex)
    {
        if (a_ButtonIndex == -1 || a_ButtonIndex >= NUM_OF_PERKS)
        {
            Debug.LogWarning("Perk is saying it's larger then num of perks");
            return;
        }
        //PERK CLICKED IS PRETTY USELESS
        m_PerkClicked[a_ButtonIndex].Invoke();

        m_PerkManager.perkSelected(a_ButtonIndex);

        //hideUI();
    }

    public void showUI()
    {
        setUIActive(true);
        //update perk list? (this is done by PerkManager (call that to update perks))
    }

    public void setClassSelectUI(bool a_value)
    {
        foreach (GameObject go in m_classSelectObjects)
        {
            go.SetActive(a_value);
        }
    }

    public void setPerkSelectUI(bool a_value)
    {
        transform.GetChild(0).gameObject.SetActive(a_value);
    }


    public void hideUI()
    {
        setUIActive(false);
    }

    private void setUIActive(bool a_State)
    {
        gameObject.SetActive(a_State);
    }

    public void setPerkInfo(Perk a_Perk, int a_Index)
    {
        m_PerkUIElements[a_Index].updatePerkUI(a_Perk);
    }

    void OnGUI()
    {
        GUI.depth = 1;
    }
}
