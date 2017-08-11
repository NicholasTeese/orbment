using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{

    public enum Status
    {
        OnFire,
        Stunned,
        Slowed,
        Buffed,
        LightningRing,
        FireRing,

    }
    protected Entity m_entity;
    public Status m_type;
    public Transform m_manager;

    protected virtual void OnEnable()
    {
        m_entity = this.GetComponentInParent<Entity>();
    }

    protected virtual void Update()
    {

        if (m_entity != null && m_entity.m_currHealth <= 0.0f)
        {
            ReturnToSender();
        }
    }

    protected void ReturnToSender()
    {
        if (m_manager != null)
        {
            this.transform.position = m_manager.position;
            this.transform.SetParent(m_manager);
        }

        this.gameObject.SetActive(false);
    }
}
