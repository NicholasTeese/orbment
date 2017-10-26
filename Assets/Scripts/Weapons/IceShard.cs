using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : Bullet
{
    private bool m_hasIceSplinter = false;

    new protected void OnEnable()
    {
        base.OnEnable();

        if (!Player.m_player.IceSplatterUnlocked)
        {
            return;
        }

        int iIceSplinter = Random.Range(0, 2);

        if (iIceSplinter == 0)
        {
            m_hasIceSplinter = false;
        }
        else if (iIceSplinter == 1)
        {
            m_hasIceSplinter = true;
        }
        else
        {
            Debug.Log(gameObject.name + "ice splinter rolled an invalid number.");
        }
    }

    new protected void Disable()
    {
        base.Disable();

        m_hasIceSplinter = false;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        //set on fire
        if (m_target != null)
        {
            m_target.m_causeSlow = true;
        }

        if(!m_hasIceSplinter && m_playerRef != null && m_playerRef.m_perks.Contains(PerkID.IceSplinter))
        {
            //do only once
            m_hasIceSplinter = true;
        }

        if (m_hasIceSplinter && !collision.collider.CompareTag(m_id))
        {
            ExplosionManager.m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.Ice, m_damage);
            ExplosionManager.m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.Shockwave, 0.0f);
        }

        if (!collision.collider.CompareTag(m_id))
        {
            Disable();
        }
    }
}
