using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : Bullet
{
    private bool m_hasIceSplinter = false;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        //set on fire
        if (m_enemy != null)
        {
            m_enemy.m_causeSlow = true;
        }

        if(!m_hasIceSplinter && m_playerRef != null && m_playerRef.m_perks.Contains(PerkID.IceSplinter))
        {
            //do only once
            m_hasIceSplinter = true;
        }

        if (m_hasIceSplinter && !collision.collider.CompareTag(m_id))
        {
            m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.Ice, m_damage);
            m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.Shockwave, 0.0f);
        }

        if (!collision.collider.CompareTag(m_id))
        {
            Disable();
        }
    }
}
