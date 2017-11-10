using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Bullet
{
    public float m_stunChance = 25.0f;
    private bool m_hasSplashDamagePerk = false;
    private bool m_hasStunPerk = false;

    protected override void OnCollisionEnter(Collision collision)
    {
        if (base.m_bColliding)
        {
            if (collision.collider.CompareTag("Player"))
            {
                // do nothing - fixes the "fart" vs invincible bug
            }
            else
            {
                return;
            }
        }
        base.m_bColliding = true;

        if (collision.collider.CompareTag(m_id))
        {
            base.m_bColliding = false;
            return;
        }

        //base.OnCollisionEnter(collision);
        if (!collision.collider.CompareTag(m_id))
        {
            ExplosionManager.m_explosionManager.RequestExplosion(this.transform.position, -m_direction, Explosion.ExplosionType.BulletImpact, 0.0f);

            m_target = collision.gameObject.GetComponent<Entity>();
            //do base damage
            if (m_target != null)
            {
                m_target.m_beenCrit = this.m_isCrit;
                m_target.m_currHealth -= m_damage;
                m_target.m_recentDamageTaken = m_damage;

                ExplosionManager.m_explosionManager.RequestExplosion(this.transform.position, -m_direction, Explosion.ExplosionType.SmallBlood, 0.0f);

                if (!m_hasStunPerk && Player.m_player != null && Player.m_player.m_perks.Contains(PerkID.StunChance))
                {
                    //do once
                    m_hasStunPerk = true;
                }

                m_target.m_setOnFire = true;

                Player.m_player.m_currSpeedMult += 1;
            }
            Disable();
        }

        //if AOE toggled on 
        if(!m_hasSplashDamagePerk && Player.m_player != null && Player.m_player.m_perks.Contains(PerkID.SplashDamage))
        {
            //do once
            m_hasSplashDamagePerk = true;
        }

        if (m_hasSplashDamagePerk && !collision.collider.CompareTag(m_id))
        {
            ExplosionManager.m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.Fire, m_damage);
            ExplosionManager.m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.Shockwave, 0.0f);
        }

        if (!collision.collider.CompareTag(m_id))
        {
            Disable();
        }
    }



}
