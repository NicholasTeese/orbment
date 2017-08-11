using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PerkTreeManager : MonoBehaviour {
	public int perkPoints;
	public Text perkPointText;

	public GameObject perkToActivate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		perkPointText.text = "Perk Points: " + perkPoints.ToString ();
	}
	public void ActivatePerk(){
		perkToActivate.GetComponent<PerkTreeOrb>().unClickOrbs ();
		perkToActivate.GetComponent<PerkTreeOrb>().perkActivated = true;
		perkToActivate.GetComponent <PerkTreeOrb> ().boughtPerk = true;
		perkPoints -= 1;
	
	}
}
