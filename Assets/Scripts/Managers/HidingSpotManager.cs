using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotManager : MonoBehaviour
{
    private List<GameObject> m_hidingSpots = new List<GameObject>();
    public List<GameObject> HidingSpots { get { return m_hidingSpots; } }

    public static HidingSpotManager m_hidingSpotManager;

    private void Awake()
    {
        if (m_hidingSpotManager == null)
        {
            m_hidingSpotManager = this;
        }
        else if (m_hidingSpotManager != this)
        {
            Destroy(gameObject);
        }

        foreach (Transform child in transform)
        {
            m_hidingSpots.Add(child.gameObject);
        }
    }
}
