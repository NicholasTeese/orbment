using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private BreakOnImpactWith m_breakableWallHolder;

    public GameObject Boulders;

    public List<GameObject> m_BoulderList = new List<GameObject>();

    private void Awake()
    {
        m_breakableWallHolder = GetComponentInParent<BreakOnImpactWith>();
    }

    private void OnTriggerEnter(Collider a_other)
    {
        if (a_other.gameObject.tag == "Bullet")
        {
            m_breakableWallHolder.WallHealth -= 45;

            int a_CurrentBoulder = RandomBoulder();
            m_BoulderList[a_CurrentBoulder].SetActive(false);

            a_CurrentBoulder = RandomBoulder();
            m_BoulderList[a_CurrentBoulder].SetActive(false);
        }
        if (m_breakableWallHolder.WallHealth <= 0)
        {
            Boulders.SetActive(false);
            Destroy(transform.parent.gameObject, 2.6f);

        }
    }

    private int RandomBoulder()
    {
        int a_value = Random.Range(0, m_BoulderList.Count);

        if (m_BoulderList[a_value] == null || !m_BoulderList[a_value].activeInHierarchy)
        {
            RandomBoulder();
        }

        return a_value;
    }
}
