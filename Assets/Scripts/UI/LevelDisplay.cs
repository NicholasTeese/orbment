using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelDisplay : MonoBehaviour
{
    private ExpManager m_expManager;
    private Text m_playerLevelDisplay;
    // Use this for initialization
    void Start()
    {
        m_playerLevelDisplay = this.GetComponent<Text>();
        m_expManager = GameObject.FindObjectOfType<ExpManager>();
    }

    // Update is called once per frame
    void Update()
    {
        m_playerLevelDisplay.text = m_expManager.m_playerLevel.ToString();
    }
}
