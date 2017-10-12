using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private BreakOnImpactWith m_breakableWallHolder = null;

    private void Awake()
    {
        m_breakableWallHolder = GetComponentInParent<BreakOnImpactWith>();
    }

    private void OnTriggerEnter(Collider a_other)
    {
        if (a_other.gameObject.tag == "Bullet")
        {
            m_breakableWallHolder.WallHealth -= 45;
        }
    }
}
