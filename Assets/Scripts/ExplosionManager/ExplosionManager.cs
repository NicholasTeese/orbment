using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    /// <summary>
    /// Object pooled explosions
    /// </summary>

    public struct ExplosionItem
    {
        public Explosion.ExplosionType m_type;
        public GameObject m_object;
        public float m_shockWaveStrength;

    }

    public GameObject[] m_explosionPrefabs;


    private Player m_player;
    private IsoCam m_camera;


    private List<List<ExplosionItem>> m_explosionPool = new List<List<ExplosionItem>>();



    public int m_poolAmount = 20;//amount of each type of explosion

    // Use this for initialization
    void Start()
    {
        //object pooling
        m_player = GameObject.FindObjectOfType<Player>();
        m_camera = GameObject.FindObjectOfType<IsoCam>();

        for (int numType = 0; numType < m_explosionPrefabs.Length; ++numType)
        {

            List<ExplosionItem> explosionType = new List<ExplosionItem>();
            for (uint i = 0; i < m_poolAmount; ++i)
            {

                if (m_explosionPrefabs[numType] != null)
                {
                    ExplosionItem newExplosion = new ExplosionItem();
                    newExplosion.m_object = (GameObject)Instantiate(m_explosionPrefabs[numType], this.transform);
                    Explosion explosionScript = newExplosion.m_object.GetComponent<Explosion>();
                    if(explosionScript != null)
                    {
                        newExplosion.m_shockWaveStrength = explosionScript.m_shockWaveStrength;
                        newExplosion.m_type = explosionScript.m_type;
                    }
                    newExplosion.m_object.SetActive(false);
                    explosionType.Add(newExplosion);

                }
            }


            m_explosionPool.Add(explosionType);
        }


    }

    public GameObject RequestExplosion(Vector3 a_position, Vector3 a_forward, Explosion.ExplosionType a_type, float a_damage)
    {
        GameObject explosion = null;

        for(int i = 0;  i < m_explosionPool.Count; ++i)
        {
            if(m_explosionPool[i][0].m_type == a_type)
            {
                explosion = FindInactive(m_explosionPool[i]);
                break;
            }
        }

        if (explosion != null)
        {
            explosion.transform.position = a_position;
            explosion.transform.forward = a_forward;
            Explosion explosionScript = explosion.GetComponent<Explosion>();
            if (explosionScript != null)
            {
                explosionScript.m_damage = a_damage;
            }

            explosion.SetActive(true);

            if (m_camera != null)
            {
                float shockWave = explosionScript.m_shockWaveStrength / Vector3.Distance(explosion.transform.position, m_player.transform.position);
                if (explosion.transform.position == m_player.transform.position)
                {
                    shockWave = explosionScript.m_shockWaveStrength/ 2.0f;
                }
                m_camera.Shake(shockWave, 0.2f);
            }

        }
        return explosion;
    }

    GameObject FindInactive(List<ExplosionItem> a_pool)
    {
        for (int i = 0; i < a_pool.Count; ++i)
        {
            if (!a_pool[i].m_object.activeInHierarchy)
            {
                return a_pool[i].m_object;

            }
        }

        return null;

    }
}
