using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollectable : MonoBehaviour
{
    [Range(0, 100)]
    public int m_healDropChance = 0;
    [Range(0, 100)]
    public int m_manaDropChance = 0;


    public int m_yellowOrbsMin = 1;
    public int m_yellowOrbsMax = 3;

    public int m_greenOrbsMin = 0;
    public int m_greenOrbsMax = 3;

    public int m_blueOrbsMin = 0;
    public int m_blueOrbsMax = 3;

    private EnemyLootManager m_lootManager;
    private void Start()
    {
        m_lootManager = GameObject.FindObjectOfType<EnemyLootManager>();

    }

    private void OnDisable()
    {
        if (m_lootManager != null)
        {
            m_lootManager.RequestLootsplosion(this.transform.position, m_yellowOrbsMin, m_yellowOrbsMax, Collectable.CollectableType.YellowOrb);


            if (Random.Range(0, 100) <= m_healDropChance)
            {
                m_lootManager.RequestLootsplosion(this.transform.position, m_greenOrbsMin, m_greenOrbsMax, Collectable.CollectableType.GreenOrb);
            }

            if (Random.Range(0, 100) <= m_manaDropChance)
            {
                m_lootManager.RequestLootsplosion(this.transform.position, m_blueOrbsMin, m_blueOrbsMax, Collectable.CollectableType.BlueOrb);
            }
        }
    }



}
