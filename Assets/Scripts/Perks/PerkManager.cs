using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkManager : MonoBehaviour
{
    public Player m_player;
    public Mana m_playerMana;
    private ExpManager m_XpManager;

    [SerializeField]
    private PerkType m_PerkTypeSelection;

    /// <summary>
    /// reference to the level up ui
    /// </summary>
    private LevelUpUI m_levelUpUI;

    public GameObject m_UpgradeAvailableText = null;

    [SerializeField]
    public Perk[] m_StartingPerks = new Perk[3];

    [SerializeField]
    public List<Perk> m_perks = new List<Perk>();

    /// <summary>
    /// list of the currently shown perks
    /// </summary>
    private Perk[] m_ShownPerks = new Perk[3];

    /// <summary>
    /// list of usable perks
    /// </summary>
    private List<Perk> m_UsablePerks = new List<Perk>();

    private bool m_SelectingStartingPerks = true;


    private int m_AmtPerkUpgrades = -1;
    private bool m_ShowingUI = false;

    //public Button m_fireSelect;
    //public Button m_iceSelect;
    //public Button m_lightningSelect;

    void Awake()
    {
        m_levelUpUI = FindObjectOfType<LevelUpUI>();
        m_XpManager = FindObjectOfType<ExpManager>();

        m_player = GameObject.FindObjectOfType<Player>();

        if (m_player != null)
        {
            m_playerMana = m_player.gameObject.GetComponent<Mana>();
        }


        m_levelUpUI.setClassSelectUI(true);
        //showPerk(m_StartingPerks[0], 0);
        //showPerk(m_StartingPerks[1], 1);
        //showPerk(m_StartingPerks[2], 2);
        //m_levelUpUI.showUI();
        //m_ShowingUI = true;

        m_UpgradeAvailableText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_AmtPerkUpgrades == -1)
        {
            return;
        }
        if (m_ShowingUI)
        {
            KeyCode code = KeyCode.Tab;
            if (Input.GetKey(code))
            {
                m_levelUpUI.showUI();
                m_UpgradeAvailableText.SetActive(false);
                //Time.timeScale = 0.2f;
              //  Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            else
            {
                m_levelUpUI.hideUI();
                m_UpgradeAvailableText.SetActive(true);
              //  Time.timeScale = 1.0f;
              //  Time.fixedDeltaTime = 0.02f;
            }

        }

    }

    private void genPerkList()
    {
        updateUseablePerks();
        //weight based on rarity
        pickPerks();

        showPerk(m_ShownPerks[0], 0);
        showPerk(m_ShownPerks[1], 1);
        showPerk(m_ShownPerks[2], 2);

        m_levelUpUI.showUI();
    }

    private void showPerk(Perk a_Perk, int a_Index)
    {
        m_ShownPerks[a_Index] = a_Perk;
        m_levelUpUI.setPerkInfo(a_Perk, a_Index);
    }

    private void updateUseablePerks()
    {
        m_UsablePerks.Clear();
        for (int i = 0; i < m_perks.Count; i++)
        {
            Perk perk = m_perks[i];
            bool shouldAdd = false;

            
            if(perk.m_Unique)
            {
                shouldAdd = (perk.m_TimesUpgraded == 0 && perk.m_type == m_PerkTypeSelection);
            }
            else
            {
                shouldAdd = (true & m_AmtPerkUpgrades >= perk.m_minLevel & (perk.m_type == m_PerkTypeSelection | perk.m_type == PerkType.Normal));
            }

            if (shouldAdd)
            {
                m_UsablePerks.Add(perk);
            }
            //print(bq.m_name + " - " + shouldAdd);
        }
    }
    private void pickPerks()
    {
        //reset perks
        m_ShownPerks[0] = null;
        m_ShownPerks[1] = null;
        m_ShownPerks[2] = null;
        float perkWeightMax = 0.0f;
        for (int i = 0; i < m_UsablePerks.Count; i++)
        {
            perkWeightMax += m_UsablePerks[i].m_PerkWeight;
        }

        for (int i = 0; i < 3; i++)
        {
            while (m_ShownPerks[i] == null && m_UsablePerks.Count != 0)
            {
                float randWeight = UnityEngine.Random.Range(0, perkWeightMax);
                float weightCalc = 0.0f;
                for (int q = 0; q < m_UsablePerks.Count; q++)
                {
                    weightCalc += m_UsablePerks[q].m_PerkWeight;
                    if (weightCalc >= randWeight)
                    {

                        //set perk
                        m_ShownPerks[i] = m_UsablePerks[q];
                        //remove perk from weight list, and remove it from the perk list
                        perkWeightMax -= m_UsablePerks[q].m_PerkWeight;
                        m_UsablePerks.Remove(m_UsablePerks[q]);

                        break;
                    }
                }

            }
        }

    }

    public void perkSelected(int a_PerkIndex)
    {
        if (m_SelectingStartingPerks)
        {
            m_PerkTypeSelection = m_StartingPerks[a_PerkIndex].m_type;
            m_SelectingStartingPerks = false;
            m_levelUpUI.setClassSelectUI(false);
            m_levelUpUI.setPerkSelectUI(true);
            m_StartingPerks[a_PerkIndex].Activate();
            m_levelUpUI.hideUI();
            return;
        }

        m_ShownPerks[a_PerkIndex].Activate();

        m_ShownPerks[a_PerkIndex].m_TimesUpgraded++;

        m_AmtPerkUpgrades++;

        if (m_AmtPerkUpgrades != m_XpManager.m_playerLevel)
        {
            genPerkList();
            m_ShowingUI = true;
            return;
        }
        m_levelUpUI.hideUI();
        m_ShowingUI = false;
        m_UpgradeAvailableText.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void leveledUp()
    {
        if (!m_ShowingUI)
        {
            genPerkList();
            m_ShowingUI = true;
        }
    }
}
