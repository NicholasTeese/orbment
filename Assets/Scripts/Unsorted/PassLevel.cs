using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PassLevel : MonoBehaviour
{
	public string goToLevel;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
	void OnTriggerEnter(Collider col)
    {
		if (col.tag == "Player")
        {
            LevelManager.m_levelManager.InitialiseDontDestroyOnLoad();
            LevelManager.m_levelManager.LoadNextLevelAsyncOperation.allowSceneActivation = true;
		}
	}
}
