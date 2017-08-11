using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// this just allows the UnityEvent to invoke itself with a int argument 
/// </summary>
public class UnityEventInt : UnityEvent<int> {
}

//manager for this perk
public class PerkUI : MonoBehaviour {

	private int m_PerkIndex = -1;

	public UnityEventInt m_PerkClicked = new UnityEventInt();

	private Text m_Text;

    public Image m_image;
	
	public void setPerkIndex(int a_Index) {

        //perk -> panel -> text -> text class
        m_Text = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        
		m_PerkIndex = a_Index;
		getButton().onClick.AddListener(buttonClickedPressed);
	} 

	private void buttonClickedPressed() {
		m_PerkClicked.Invoke(m_PerkIndex);
	}

	public Button getButton() {
		return GetComponentInChildren<Button>();//maybe get the child transform and get component from that
	}

	public void updatePerkUI(Perk a_Perk) {
		if(a_Perk == null) {
			//m_Text.text = "NO PERK";
		}
        //todo set text/image
        m_Text.text = a_Perk.m_name;
        if(m_image != null)
        {
            m_image.sprite = a_Perk.m_PerkImage;
        }
		//print("perk "+a_Perk.m_name+": setting the image of perk " + m_PerkIndex + " is not implemented");
	}
    void OnGUI()
    {
        GUI.depth = 0;
    }
}
