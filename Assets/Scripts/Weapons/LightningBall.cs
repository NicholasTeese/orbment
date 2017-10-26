using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBall : Bullet
{
    public int m_godBoltChance = 10;
    private bool m_hasGodBolt = false;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        //set on fire
        if (m_target != null)
        {
            ExplosionManager.m_explosionManager.RequestExplosion(collision.collider.transform.position, this.transform.forward, Explosion.ExplosionType.Lightning, m_damage);


            if(!m_hasGodBolt && Player.m_player != null && Player.m_player.m_perks.Contains(PerkID.GodBolt) && !collision.collider.CompareTag(m_id))
            {
                //do once
                m_hasGodBolt = true;
            }

            if(m_hasGodBolt && !collision.collider.CompareTag(m_id))
            {
                if (Random.Range(0, 100) <= m_godBoltChance)
                {
                    ExplosionManager.m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.GodLightning, m_damage);
                    ExplosionManager.m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.Shockwave, 0.0f);
                }
            }
        }


        if (!collision.collider.CompareTag(m_id))
        {
            Disable();
        }
    }
}
