using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PerkTreeOrb : MonoBehaviour {
	private Vector3[] linePosition;

	public GameObject[] branchLength;
	private GameObject[] perkOrbs;
	public GameObject perkChild;

	private int positionAmount;
	public bool perkActivated = false;


	private LineRenderer lineRend;
	private GameObject perkTreeSystem;

	public bool boughtPerk = false;

	void Start () 
	{
		lineRend = GetComponent<LineRenderer> ();
		perkTreeSystem = GameObject.Find ("PerkTreeSystem");
	}
	
	// Update is called once per frame

	//Lines for Perks to make editing them a tad easier
	void OnDrawGizmos(){
		if (branchLength [1].gameObject != this.gameObject) {
			Gizmos.color = Color.red;
			Gizmos.DrawLine (transform.position, branchLength [1].transform.position);
			//Finding all Orbs, Change this later to individual Orb Types. 
			perkOrbs = GameObject.FindGameObjectsWithTag ("perkOrb");
		} else {
			Gizmos.color = Color.red;
			Gizmos.DrawLine (transform.position, branchLength [2].transform.position);
			//Finding all Orbs, Change this later to individual Orb Types. 
		
		}
	}
	void Update () 
	{
		
		perkOrbs = GameObject.FindGameObjectsWithTag ("perkOrb");
		//If an orb has been clicked on, create a link down the branch. 
		if (perkActivated) 
		{
			//Turns on the glow for all perks in the perk branch. 
			foreach (GameObject perkchildren in branchLength) 
			{
				perkchildren.transform.GetChild(0).gameObject.SetActive(true);
				perkchildren.transform.GetChild(1).gameObject.SetActive(true);
			}
			lineRend.enabled = true;
			lineRend.numPositions = branchLength.Length;
			positionAmount = lineRend.numPositions;

			//Sets the line rend for all branch positions
			for (int i = 0; i < positionAmount; i++) 
			{
				lineRend.SetPosition (i, branchLength [i].transform.position);

			}
		} 
		else 
		{
			lineRend.enabled = false;
		}
	}

	//Enables new branch
	void OnMouseDown()
	{
		//buying perk
		if (boughtPerk == false) {
			if (GameObject.Find ("PerkTreeSystem").GetComponent<PerkTreeManager> ().perkPoints >= 1) {
				if (branchLength [1].GetComponent <PerkTreeOrb> ().boughtPerk == true) {
					perkTreeSystem.GetComponent <PerkTreeManager> ().perkToActivate = this.gameObject;
					GameObject.Find ("CanvasPerk").transform.GetChild (0).gameObject.SetActive (true);
				} else {
					if (branchLength [1].gameObject == this.gameObject) {
						perkTreeSystem.GetComponent <PerkTreeManager> ().perkToActivate = this.gameObject;
						GameObject.Find ("CanvasPerk").transform.GetChild (0).gameObject.SetActive (true);
					}
				}
			}

		} else {
			unClickOrbs ();
			perkActivated = true;		
		}
		//Disables all orbs before enabling the next requested branch

		//perkActivated = true;
	}
	//Shows Discription for this perk
	void OnMouseOver(){
		GameObject.Find ("CanvasPerk").transform.GetChild (1).GetComponentInChildren<Text>().GetComponent<Text>().text = transform.GetChild (0).GetComponent<Text>().text;
	}

	//Disables All Perk Tree's 
	public void unClickOrbs()
	{
		foreach (GameObject orb in perkOrbs) 
		{
			orb.transform.GetChild(0).gameObject.SetActive(false);
			orb.GetComponent<PerkTreeOrb>().perkActivated = false;

		}
	}


}
