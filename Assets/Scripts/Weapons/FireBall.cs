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
        Debug.Log(collision.collider.tag);

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
            m_explosionManager.RequestExplosion(this.transform.position, -m_direction, Explosion.ExplosionType.BulletImpact, 0.0f);

            m_enemy = collision.collider.GetComponent<Entity>();
            //do base damage
            if (m_enemy != null)
            {
                m_enemy.m_beenCrit = this.m_isCrit;
                m_enemy.m_currHealth -= m_damage;
                m_enemy.m_recentDamageTaken = m_damage;

                m_explosionManager.RequestExplosion(this.transform.position, -m_direction, Explosion.ExplosionType.SmallBlood, 0.0f);
            }
            Disable();
        }

        //set on fire
        if (m_enemy != null)
        {
            if(!m_hasStunPerk && m_playerRef != null && m_playerRef.m_perks.Contains(PerkID.StunChance))
            {
                //do once
                m_hasStunPerk = true;
            }

            //stun
            if (m_hasStunPerk && Random.Range(0.0f, 100.0f) <= m_stunChance)
            {
                m_enemy.m_causeStun = true;
            }

            m_enemy.m_setOnFire = true;

            Player.m_Player.m_currSpeedMult += 1;
        }


        //if AOE toggled on 

        if(!m_hasSplashDamagePerk && m_playerRef != null && m_playerRef.m_perks.Contains(PerkID.SplashDamage))
        {
            //do once
            m_hasSplashDamagePerk = true;
        }

        if (m_hasSplashDamagePerk && !collision.collider.CompareTag(m_id))
        {
            m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.Fire, m_damage);
            m_explosionManager.RequestExplosion(this.transform.position, this.transform.forward, Explosion.ExplosionType.Shockwave, 0.0f);
        }

        if (!collision.collider.CompareTag(m_id))
        {
            Disable();
        }
    }



}
