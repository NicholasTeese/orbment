using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum ActiveBullet
    {
        NORMAL,
        FIRE,
        ICE,
        LIGHTNING
    }

    private int m_iMaxBulletsOnScreen = 50;

    private List<GameObject> m_normalBullets = new List<GameObject>();
    private List<GameObject> m_fireBullets = new List<GameObject>();
    private List<GameObject> m_iceBullets = new List<GameObject>();
    private List<GameObject> m_lightningBullets = new List<GameObject>();

    private ActiveBullet m_eActiveBullet = ActiveBullet.NORMAL;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            switch (child.tag)
            {
                case "NormalBullet":
                    {
                        m_normalBullets.Add(child.gameObject);
                        break;
                    }

                case "FireBullet":
                    {
                        m_fireBullets.Add(child.gameObject);
                        break;
                    }

                case "IceBullet":
                    {
                        m_iceBullets.Add(child.gameObject);
                        break;
                    }

                case "LightningBullet":
                    {
                        m_lightningBullets.Add(child.gameObject);
                        break;
                    }

                default:
                    {
                        Debug.Log("Bullet tag not recognised.");
                        break;
                    }
            }
        }
    }

    private void Fire(List<GameObject> a_activePool, Vector3 a_v3Direction, bool a_bIsCrit)
    {
        for (int iCount = 0; iCount < m_iMaxBulletsOnScreen; ++iCount)
        {
            if (!a_activePool[iCount].activeInHierarchy)
            {
                a_activePool[iCount].transform.parent = null;
                a_activePool[iCount].transform.position = transform.position;
                a_activePool[iCount].SetActive(true);
            }
        }
    }
}
