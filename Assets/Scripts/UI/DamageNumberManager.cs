using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumberManager : MonoBehaviour
{
    public GameObject m_dmgNumPrefab;
    public int m_poolAmount = 50;
    private List<GameObject> m_dmgNums = new List<GameObject>();
    private List<DamageNumber> m_dmgTexts = new List<DamageNumber>();
    private GameObject m_canvas;
    // Use this for initialization
    void Start()
    {
        m_canvas = GameObject.Find("Canvas");

        if(m_dmgNumPrefab != null)
        {
            for(int i = 0; i< m_poolAmount; ++i)
            {
                GameObject dmgText = GameObject.Instantiate(m_dmgNumPrefab);
                dmgText.transform.SetParent(m_canvas.transform, false);
                m_dmgNums.Add(dmgText);

                DamageNumber text = dmgText.GetComponent<DamageNumber>();

                if(text != null)
                {
                    m_dmgTexts.Add(text);
                }

                dmgText.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateDamageNumber(string number, Transform location, Color a_color)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(location.position);
        for (int i = 0; i < m_poolAmount; ++i)
        {
            if (m_dmgNums.Count != 0)
            {
                if (!m_dmgNums[i].activeInHierarchy)
                {

                    m_dmgNums[i].SetActive(true);
                    m_dmgNums[i].transform.position = screenPos;
                    m_dmgTexts[i].m_text.text = number;
                    m_dmgTexts[i].m_color = a_color;
                    m_dmgTexts[i].parent = location;
                    break;
                }
            }

        }
    }
}
