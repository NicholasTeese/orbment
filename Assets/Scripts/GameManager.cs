using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager m_GameManager;
    public GameObject PerkCam;
    public GameObject PerkScreen;
	public GameObject Buffer; //AA added this
    public GameObject Tree;

    public bool perkOpen = false;
	public bool gameStart = false;
	public bool paused = false;
    public bool dead = false;
    public bool inRange = false;

	public GameObject healthBar;
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject hud;

	void Awake()
    {
        if (m_GameManager == null)
        {
            m_GameManager = this;
        }
        else if (m_GameManager != this)
        {
            Destroy(gameObject);
        }
    }
    
    // Use this for initialization
	void Start ()
    {
        PerkCam.SetActive(false);
        PerkScreen.SetActive(false); //AA changed this
        gameStart = true;
		//Buffer.SetActive(true); //AA added this
	}
	
	// Update is called once per frame
	void Update ()
    {
        //pause game
        if(inRange)
        {
		    if (Input.GetKeyDown(KeyCode.Tab))
            {
		    	//x if (!perkOpen)
                //x {
                //x     Debug.Log("open");
		    	//x 	PerkCam.SetActive (true);
                //x     PerkScreen.SetActive(true);
                //x     hud.SetActive(false);
                //x     perkOpen = true;
				//x 	//Buffer.SetActive(false); //AA added this
		    	//x 	Time.timeScale = 0;
		    	//x }
                //x else
                //x {
                //x     Debug.Log("close");
		    	//x 	PerkCam.SetActive (false);
                //x     PerkScreen.SetActive(false); //AA changed this
                //x     hud.SetActive(true);
				//x 	//Buffer.SetActive(true); //AA added this
                //x     perkOpen = false;
		    	//x 	Time.timeScale = 1;
		    	//x }
                EnablePerkTree();
            }
        }

        if (gameStart == true)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (!paused)
                {
                    paused = true;
                }
                else
                {
                    paused = false;
                }
            }
			if (paused)
            {
				Time.timeScale = 0;
				pauseMenu.SetActive (true);
				hud.SetActive (false);
			}
            else if(perkOpen)
            {
                Time.timeScale = 0;
                hud.SetActive(false);
                pauseMenu.SetActive(false);
            }
            else if (dead == true)
            {
                Time.timeScale = 0;
                deathMenu.SetActive(true);
                hud.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(false);
                hud.SetActive(true);
                Time.timeScale = 1;
            }
		}
	}
	public void ContinueGame()
    {
		paused = false;
	}
	public void Options()
    {

	}
	public void QuitToMain()
    {

	}
	public void QuitToDesktop()
    {

	}
	public void RestartGame()
    {
		Scene loadedLevel = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (loadedLevel.buildIndex);
	}
	public void StartGame()
    {
		gameStart = true;
	}

    public void EnablePerkTree()
    {
        if (!perkOpen)
        {
            PerkCam.SetActive(true);
            PerkScreen.SetActive(true);
            hud.SetActive(false);
            perkOpen = true;
            Time.timeScale = 0;
        }
        else
        {
            PerkCam.SetActive(false);
            PerkScreen.SetActive(false);
            hud.SetActive(true);

            perkOpen = false;
            Time.timeScale = 1;
        }
    }
}