using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
	public GameObject PerkCam;
	bool perkOpen = false;
	public bool gameStart = false;
	public bool paused = false;

	public GameObject healthBar;
	public GameObject pauseMenu;
	public GameObject hud;

	// Use this for initialization
	void Start () {
		PerkCam.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//pause game
		if (Input.GetKeyUp (KeyCode.Tab)) {
			if (perkOpen == false) {
				PerkCam.SetActive (true);
				perkOpen = true;
				Time.timeScale = 0;
			} else {
				PerkCam.SetActive (false);
				perkOpen = false;
				Time.timeScale = 1;
			}
		}


		if (gameStart == true) {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				if (paused == false) {
					paused = true;
				} else {
					paused = false;
				}
			}
			if (paused == true) {
				Time.timeScale = 0;
				pauseMenu.SetActive (true);
				hud.SetActive (false);

			} else {
				if (!perkOpen) {
					Time.timeScale = 1;
				}
				pauseMenu.SetActive (false);
				hud.SetActive (true);
			}
		}
	}
	public void ContinueGame(){
		paused = false;
	}
	public void Options(){

	}
	public void QuitToMain(){

	}
	public void QuitToDesktop(){

	}
	public void RestartGame(){
		Scene loadedLevel = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (loadedLevel.buildIndex);
	}
	public void StartGame(){
		gameStart = true;
	}
}
