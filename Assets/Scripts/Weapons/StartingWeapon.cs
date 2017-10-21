using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingWeapon : BaseWeapon
{
    private AudioClip[] m_normalBullets;
    private AudioClip[] m_fireBullets;

    private void Awake()
    {
        m_normalBullets = Resources.LoadAll<AudioClip>("Audio/Beta/Normal_Bullet");
        m_fireBullets = Resources.LoadAll<AudioClip>("Audio/Beta/Fire_Bullet");

        m_audioSource = GetComponent<AudioSource>();
    }

    public override void Fire(Vector3 a_direction, int damagePerProjectile, bool a_hasCrit, float a_critMult)
    {
        base.PoolToActive(a_direction, damagePerProjectile, 1);

        switch (m_projectile.name)
        {
            case "PlayerBullet":
                {
                    m_audioSource.PlayOneShot(m_normalBullets[Random.Range(0, 4)]);
                    break;
                }

            case "FireBall":
                {
                    m_audioSource.PlayOneShot(m_fireBullets[Random.Range(0, 4)]);
                    break;
                }

            case "EnemyBullet":
                {
                    //m_audioSource.PlayOneShot(m_normalBullets[Random.Range(0, 4)]);
                    break;
                }

            default:
                {
                    Debug.Log(m_projectile.name + " not recognised.");
                    break;
                }
        }

        for (int i = 0; i < m_activePool.Count; ++i)
        {
            m_activePool[i].m_isCrit = a_hasCrit;
            m_activePool[i].m_direction = a_direction;
            m_activePool[i].m_damage = m_activePool[i].m_baseDamage + damagePerProjectile;

            if(a_hasCrit)
            {
                m_activePool[i].m_damage = (int) (m_activePool[i].m_damage * a_critMult);
            }
        }
    }
}
