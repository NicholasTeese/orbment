using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//-------------------------------------------------------------------------------------------------------------------------------------------------------------

// Creators: Vince & John
// Additions: Taliesin Millhouse & Nicholas Teese
// Description: Updates mana bar

//-------------------------------------------------------------------------------------------------------------------------------------------------------------
public class Mana : MonoBehaviour
{
	public GameObject manaBar;
    public Texture2D m_manaBarTexture;
    public Texture2D m_emptyBarTexture;
    public int m_manaBarWidth = 500;
    public float m_currentMana = 100.0f;
    public float m_maxMana = 100.0f;

    void Start()
    {

    }

    void Update()
    {
        manaBar.GetComponent<Slider> ().value = m_currentMana;
		manaBar.GetComponent<Slider> ().maxValue = m_maxMana;
    }

    private void OnGUI()
    {
        
    }
}