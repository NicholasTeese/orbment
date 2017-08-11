using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explosion))]
public class IceSplinter : MonoBehaviour
{

    public float m_dmgPerProjectileRatio = 0.5f;
    private Explosion m_explosionScript;

    private Bullet[] m_shards;
    
    void Awake()
    {
        m_shards = this.GetComponentsInChildren<Bullet>();
        m_explosionScript = this.GetComponent<Explosion>();
    }
    void OnEnable()
    {
        if(m_shards == null)
        {
            return;
        }
        //reset position
        for(int i = 0; i< m_shards.Length; ++i)
        {
            m_shards[i].gameObject.transform.position = this.transform.position;
            Vector3 direction = Random.onUnitSphere;
            direction.y = 0;
            m_shards[i].m_direction = direction.normalized;
            m_shards[i].m_damage = Mathf.CeilToInt( m_explosionScript.m_damage * m_dmgPerProjectileRatio);
            m_shards[i].gameObject.SetActive(true);
        }

        

    }
}
