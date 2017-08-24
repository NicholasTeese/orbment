using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : ScriptableObject
{
    public List<TillyBullet> m_bullets;
}

[System.Serializable]
public class TillyBullet
{
    public int m_iDamage;

    public float m_fVelocity;
}
