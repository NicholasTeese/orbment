using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public enum ExplosionType
    {
        Fire,
        Ice,
        Lightning,
        SmallBlood,
        BigBlood,
        Gibs,
        BulletImpact,
        AfterImage,
        GodLightning,
        Shockwave,


    }

    
    public ExplosionType m_type;
    public float m_shockWaveStrength = 100.0f;

    public float m_damage = 0.0f;
    public float m_duration = 1.0f;
    private float m_timer = 0.0f;


    void OnEnable()
    {
        m_timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_timer += Time.deltaTime;
        if(m_timer >= m_duration)
        {
            //disable
            this.gameObject.SetActive(false);
        }
    }
}
