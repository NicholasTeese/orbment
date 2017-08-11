using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingWeapon : BaseWeapon
{

    public override void Fire(Vector3 a_direction, int damagePerProjectile, bool a_hasCrit, float a_critMult)
    {
        base.PoolToActive(a_direction, damagePerProjectile, 1);


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
