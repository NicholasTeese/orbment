using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public struct EnemyItem
    {
        public Enemy.EnemyType m_type;
        public Enemy m_script;
        public GameObject m_object;
    }

    public GameObject[] m_enemyPrefabs;
    public int m_poolAmount = 20;
    public float m_maxRange = 10;
    
    private List<List<EnemyItem>> m_pooledEnemies = new List<List<EnemyItem>>();
    // Use this for initialization
    void Start()
    {

        for (int numType = 0; numType < m_enemyPrefabs.Length; ++numType)
        {

            List<EnemyItem> enemyType = new List<EnemyItem>();
            for (uint i = 0; i < m_poolAmount; ++i)
            {

                if (m_enemyPrefabs[numType] != null)
                {
                    EnemyItem newEnemy = new EnemyItem();
                    newEnemy.m_object = (GameObject)Instantiate(m_enemyPrefabs[numType], this.transform);
                    Enemy enemyScript = newEnemy.m_object.GetComponent<Enemy>();

                    if (enemyScript != null)
                    {
                        newEnemy.m_type = enemyScript.m_type;
                        newEnemy.m_script = enemyScript;
                    }
                    newEnemy.m_object.SetActive(false);
                    enemyType.Add(newEnemy);

                }
            }


            m_pooledEnemies.Add(enemyType);
        }
    }





    private void Update()
    {
        Collider[] insideSphere = Physics.OverlapSphere(this.transform.position, 1.0f);

        if(insideSphere.Length <= 1 )
        {
            Vector3 spawnPoint = this.transform.position + Random.onUnitSphere * m_maxRange;
            spawnPoint.y = this.transform.position.y;
            SpawnEnemy(spawnPoint, (Enemy.EnemyType)Random.Range(0,m_enemyPrefabs.Length));
        }
    }

    public void SpawnEnemy(Vector3 a_position, Enemy.EnemyType a_type)
    {
        GameObject enemy = null;

        for (int i = 0; i < m_pooledEnemies.Count; ++i)
        {
            if (m_pooledEnemies[i][0].m_type == a_type)
            {
                enemy = FindInactive(m_pooledEnemies[i]);
                break;
            }
        }

        if(enemy != null)
        {
            enemy.transform.position = a_position;
            enemy.gameObject.SetActive(true);
        }
    }

    GameObject FindInactive (List<EnemyItem> a_pool)
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
