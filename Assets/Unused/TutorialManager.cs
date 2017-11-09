using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager m_TutorialManager;
    //public GameObject m_eMessageHolder;
    public List<GameObject> m_MessageList = new List<GameObject>();

    public bool WindowOpen;

    void Awake()
    {
        if (m_TutorialManager == null)
        {
            m_TutorialManager = this;
        }
        else if (m_TutorialManager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitList();
        WindowOpen = false;
	}

    private int PopulateMessageList(List<GameObject> m_aMessageList)
    {
        int iMessageCount = 0;

        for (int iCount = 0; iCount < m_aMessageList.Count; ++iCount)
        {
            iMessageCount += m_aMessageList[iCount].transform.childCount;
        }

        return iMessageCount;
    }

    private void InitList()
    {
        foreach (GameObject a_message in m_MessageList)
        {
            //a_message.SetActive(false);
        }
    }

    public void CloseWindow()
    {
        foreach (GameObject a_message in m_MessageList)
        {
            if (a_message.activeInHierarchy)
            {
                WindowOpen = false;
                //Time.timeScale = 1;
                //a_message.SetActive(false);
            }
        }
    }

    public void NextWindow()
    {
        foreach (GameObject a_message in m_MessageList)
        {
            if (a_message.activeInHierarchy)
            {
                WindowOpen = false;
                //Time.timeScale = 1;
                //a_message.SetActive(false);
            }
        }
    }
}