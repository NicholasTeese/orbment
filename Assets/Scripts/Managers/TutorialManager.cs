using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //public GameObject m_eMessageHolder;
    public List<GameObject> m_MessageList = new List<GameObject>();

	void Start()
    {
		
	}
	
	void Update()
    {
		
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
}