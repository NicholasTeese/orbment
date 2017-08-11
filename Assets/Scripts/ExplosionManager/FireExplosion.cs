using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explosion))]
public class FireExplosion : MonoBehaviour
{
    public float m_splashRatio = 0.5f;

    private Explosion m_explosionScript;

    private void Start()
    {
        m_explosionScript = this.GetComponent<Explosion>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            Entity entity = col.GetComponent<Entity>();

            if (entity != null)
            {

                entity.m_currHealth -= Mathf.CeilToInt(m_explosionScript.m_damage * m_splashRatio);

            }
        }
    }
}
