using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : ScriptableObject
{
    private List<BulletInformation> m_bulletInformation = new List<BulletInformation>();
}

public enum BulletType
{
    NORMAL,
    FIRE,
    ICE,
    LIGHTNING
}

[System.Serializable]
public class BulletInformation
{
    public int m_iDamage = 5;
    public float m_fSpeed;
    public BulletType m_eBulletType;
}
