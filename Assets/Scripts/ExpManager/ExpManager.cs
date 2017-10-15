using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExpManager : MonoBehaviour
{
    private bool m_bUpgradeTreeInRange = false;
    public bool UpgradeTreeInRange { get { return m_bUpgradeTreeInRange; } set { m_bUpgradeTreeInRange = value; } }
    private bool m_bPerkTreeOpen = false;
    public bool PerkTreeOpen { get { return m_bPerkTreeOpen; } set { m_bPerkTreeOpen = value; } }

    private GameObject m_upgradeAvailableText;
    private GameObject m_upgradeUnavailableText;
	private GameObject Xpfiller;
	private GameObject XPSlider;

    private Texture2D m_experienceBarFull;
    private Texture2D m_experienceBarEmpty;

    public int m_expBarWidth = 500;
    public float m_playerExperience = 0.0f;
    public float m_playerMaxXP = 50.0f;
    public int m_playerLevel = 1;
    public float m_percentageAddedXPPerLvl = 0.25f;

    public static ExpManager m_experiencePointsManager;

    private void Awake()
    {
        if (m_experiencePointsManager == null)
        {
            m_experiencePointsManager = this;
        }
        else if (m_experiencePointsManager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == LevelManager.m_strLevelOneSceneName)
        {
            // Initialise perk.
            PerkTreeManager.m_perkTreeManager.m_selectedPerkTreeButton.m_childPerkTree.SetActive(true);
            PerkTreeManager.m_perkTreeManager.m_selectedPerkButton.PurchasePerk();
            PerkTreeManager.m_perkTreeManager.m_selectedPerkTreeButton.m_childPerkTree.SetActive(false);
        }

        PerkTreeManager.m_perkTreeManager.gameObject.SetActive(false);
        PerkTreeCamera.m_perkTreeCamera.gameObject.SetActive(false);

        m_upgradeAvailableText = GameObject.FindGameObjectWithTag("UpgradeAvailableText");
        m_upgradeUnavailableText = GameObject.FindGameObjectWithTag("UpgradeUnavailableText");
        Xpfiller = PlayerHUDManager.m_playerHUDManager.transform.Find("ExperienceBar").Find("ExperienceFiller").gameObject;
        XPSlider = PlayerHUDManager.m_playerHUDManager.transform.Find("ExperienceBar").Find("ExperienceSlider").gameObject;

        m_experienceBarFull = Resources.Load("Textures/Experience_Bar_Full") as Texture2D;
        m_experienceBarEmpty = Resources.Load("Textures/Experience_Bar_Empty") as Texture2D;
    }

    void Update()
    {
		XPSlider.GetComponent<Slider> ().maxValue = m_playerMaxXP;
		Xpfiller.GetComponent<Slider> ().value = m_playerExperience;
		Xpfiller.GetComponent<Slider> ().maxValue = m_playerMaxXP;
		if (XPSlider.GetComponent<Slider> ().value < m_playerExperience)
        {
			XPSlider.GetComponent<Slider> ().value += 5.0f * Time.deltaTime;
		}
        if (m_playerExperience < XPSlider.GetComponent<Slider>().value)
        {
            XPSlider.GetComponent<Slider>().value = m_playerExperience;
        }

        if (m_playerExperience >= m_playerMaxXP)
        {
            LevelUp();
        }

        if (m_bUpgradeTreeInRange && !m_bPerkTreeOpen)
        {
            if (PerkTreeManager.m_perkTreeManager.AvailiablePerks == 0)
            {
                //m_upgradeAvailableText.SetActive(false);
                //m_upgradeUnavailableText.SetActive(true);
            }
            else
            {
                //m_upgradeAvailableText.SetActive(true);
                //m_upgradeUnavailableText.SetActive(false);
            }
        }
        else
        {
            //m_upgradeAvailableText.SetActive(false);
            //m_upgradeUnavailableText.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Tab) || InputManager.BackButton())
        {
            if(!GameManager.m_gameManager.GameIsPaused)
            {
                if (!m_bPerkTreeOpen)
                {
                    EnablePerkTree();
                }
                else if (m_bPerkTreeOpen)
                {
                    DisablePerkTree();
                }    
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && m_bPerkTreeOpen)
        {
            DisablePerkTree();
        }
    }

    void LevelUp()
    {
        m_playerExperience = m_playerExperience - m_playerMaxXP;
        m_playerMaxXP += m_percentageAddedXPPerLvl*m_playerMaxXP;
        m_playerLevel++;
        PerkTreeManager.m_perkTreeManager.IncrementAvailiablePerks();
	}

    private void EnablePerkTree()
    {
        PerkTreeManager.m_perkTreeManager.gameObject.SetActive(true);
        PerkTreeCamera.m_perkTreeCamera.gameObject.SetActive(true);
        PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(false);
        IsoCam.m_playerCamera.gameObject.SetActive(false);
        m_bPerkTreeOpen = true;
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    private void DisablePerkTree()
    {
        PerkTreeManager.m_perkTreeManager.gameObject.SetActive(false);
        PerkTreeCamera.m_perkTreeCamera.gameObject.SetActive(false);
        PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);
        IsoCam.m_playerCamera.gameObject.SetActive(true);
        m_bPerkTreeOpen = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }
}
