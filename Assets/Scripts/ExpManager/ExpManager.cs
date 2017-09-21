using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpManager : MonoBehaviour
{
    private bool m_bUpgradeTreeInRange = false;
    public bool UpgradeTreeInRange { get { return m_bUpgradeTreeInRange; } set { m_bUpgradeTreeInRange = value; } }
    private bool m_bPerkTreeOpen = false;
    public bool PerkTreeOpen { get { return m_bPerkTreeOpen; } set { m_bPerkTreeOpen = value; } }

    private GameObject m_perkTreeCanvas;
    private GameObject m_perkTreeCamera;

    public GameObject m_upgradeAvailableText;
    public GameObject m_upgradeUnavailableText;
	public GameObject Xpfiller;
	public GameObject XPSlider;
    public Texture2D m_expBarTexture;
    public Texture2D m_emptyBarTexture;
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

        m_perkTreeCanvas = GameObject.FindGameObjectWithTag("PerkTreeCanvas");
        m_perkTreeCamera = GameObject.FindGameObjectWithTag("PerkTreeCamera");
    }

    private void Start()
    {
        ValidateInitialisation();

        m_perkTreeCamera.SetActive(false);
        m_perkTreeCanvas.SetActive(false);
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
                m_upgradeAvailableText.SetActive(false);
                m_upgradeUnavailableText.SetActive(true);
            }
            else
            {
                m_upgradeAvailableText.SetActive(true);
                m_upgradeUnavailableText.SetActive(false);
            }
        }
        else
        {
            m_upgradeAvailableText.SetActive(false);
            m_upgradeUnavailableText.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Tab) || InputManager.BackButton())
        {
            if(!GameManager.m_gameManager.GameIsPaused)
            {
                if (PerkTreeManager.m_perkTreeManager.AvailiablePerks != 0 && !m_bPerkTreeOpen)
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


    private void OnGUI()
    {
        //x GUI.DrawTexture(new Rect((Screen.width - m_expBarWidth)/2, 200, m_expBarWidth, 25), m_emptyBarTexture);
        //x GUI.DrawTexture(new Rect((Screen.width - m_expBarWidth)/2 + 200, 0, m_expBarWidth * (m_playerExperience/m_playerMaxXP), 25), m_expBarTexture);
    }

    private void ValidateInitialisation()
    {
        if (m_perkTreeCanvas == null)
        {
            Debug.Log("PerkTreeCanvas was unable to be initialised.");
        }

        if (m_perkTreeCamera == null)
        {
            Debug.Log("PerkTreeCamera was unable to be initialised.");
        }
    }

    void LevelUp()
    {
        //x perkUpgradeUI.SetActive (true);

        m_playerExperience = m_playerExperience - m_playerMaxXP;
        m_playerMaxXP += m_percentageAddedXPPerLvl*m_playerMaxXP;
        m_playerLevel++;
        PerkTreeManager.m_perkTreeManager.IncrementAvailiablePerks();
		//x m_PerkManager.genPerkList();
		//x m_PerkManager.leveledUp();
		//x LevelUpUI.m_Singleton.showUI();
	}

    private void EnablePerkTree()
    {
        m_perkTreeCamera.SetActive(true);
        m_perkTreeCanvas.SetActive(true);
        PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(false);
        m_bPerkTreeOpen = true;
        Time.timeScale = 0;
    }

    private void DisablePerkTree()
    {
        m_perkTreeCamera.SetActive(false);
        m_perkTreeCanvas.SetActive(false);
        PlayerHUDManager.m_playerHUDManager.gameObject.SetActive(true);
        m_bPerkTreeOpen = false;
        Time.timeScale = 1;
    }
}
