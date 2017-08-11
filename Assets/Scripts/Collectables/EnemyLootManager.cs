using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLootManager : MonoBehaviour
{
    public GameObject[] collectablePrefabs;//originals
    public int m_poolAmount = 20; // max amount per collectable type
    public float m_lootsplosionForce = 10;

    public struct Item
    {
        public Collectable m_script;
        public GameObject m_object;
    }

    public List<List<Item>> m_pool = new List<List<Item>>();
    // Use this for initialization


    void Start()
    {

        for (int j = 0; j < collectablePrefabs.Length; ++j)
        {
            List<Item> m_newType = new List<Item>();
            for (int i = 0; i < m_poolAmount; ++i)
            {


                if (collectablePrefabs[j] != null)
                {
                    Item newItem = new Item();
                    newItem.m_object = GameObject.Instantiate(collectablePrefabs[j]);
                    Collectable script = newItem.m_object.GetComponent<Collectable>();
                    if(script != null)
                    {
                        newItem.m_script = script;
                    }

                    newItem.m_object.transform.SetParent(this.transform);
                    newItem.m_object.SetActive(false);


                    m_newType.Add(newItem);
                }



            }

            m_pool.Add(m_newType);
        }

    }

    public void RequestLootsplosion(Vector3 a_position, int minAmount, int maxAmount, Collectable.CollectableType a_type)
    {
        for(int i = 0; i< m_pool.Count; ++i)
        {
            if(m_pool[i][0].m_script.m_type == a_type)
            {
                //found correct type of collectable

                //dig in
                int spawnAmount = Random.Range(minAmount, maxAmount);

                for(int j = 0; j < spawnAmount; ++j)
                {
                    Vector3 forceVector = Random.onUnitSphere;
                    forceVector.y = 0;
                    GameObject loot = FindInactive(m_pool[i]);

                    if(loot != null)
                    {
                        loot.transform.position = a_position + forceVector.normalized;
                        loot.SetActive(true);

                        Rigidbody rb = loot.GetComponent<Rigidbody>();

                        if (rb != null)
                        {
                            rb.AddForce(forceVector.normalized * m_lootsplosionForce, ForceMode.Impulse);
                        }
                    }

                }
                break;
               
            }
        }
    }

    GameObject FindInactive(List<Item> a_pool)
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
