using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpManager : MonoBehaviour
{
	public GameObject Xpfiller;
	public GameObject perkUpgradeUI;
	public GameObject XPSlider;
    public Texture2D m_expBarTexture;
    public Texture2D m_emptyBarTexture;
    public int m_expBarWidth = 500;
    public float m_playerExperience = 0.0f;
    public float m_playerMaxXP = 50.0f;
    public int m_playerLevel = 1;
    public float m_percentageAddedXPPerLvl = 0.25f;

	private PerkManager m_PerkManager;



    // Use this for initialization
    void Start()
    {
		m_PerkManager = FindObjectOfType<PerkManager>();
    }

    // Update is called once per frame
    void Update()
    {
		XPSlider.GetComponent<Slider> ().maxValue = m_playerMaxXP;
		Xpfiller.GetComponent<Slider> ().value = m_playerExperience;
		Xpfiller.GetComponent<Slider> ().maxValue = m_playerMaxXP;
		if (XPSlider.GetComponent<Slider> ().value < m_playerExperience) {
			XPSlider.GetComponent<Slider> ().value += 2.5f * Time.deltaTime;
		}

        if(m_playerExperience >= m_playerMaxXP)
        {
            LevelUp();

        }
    }


    private void OnGUI()
    {
        //GUI.DrawTexture(new Rect((Screen.width - m_expBarWidth)/2, 200, m_expBarWidth, 25), m_emptyBarTexture);
        //GUI.DrawTexture(new Rect((Screen.width - m_expBarWidth)/2 + 200, 0, m_expBarWidth * (m_playerExperience/m_playerMaxXP), 25), m_expBarTexture);
    }


    void LevelUp()
    {
		perkUpgradeUI.SetActive (true);
        
        m_playerExperience = m_playerExperience - m_playerMaxXP;
        m_playerMaxXP += m_percentageAddedXPPerLvl*m_playerMaxXP;
        m_playerLevel++;
		//m_PerkManager.genPerkList();
		m_PerkManager.leveledUp();
		//LevelUpUI.m_Singleton.showUI();
	}


  
}
