using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public List<Enemy> m_enemyList = new List<Enemy>();

    public GameObject Ramp_1; public GameObject Ramp_2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(m_enemyList != null)
            {
                foreach(Enemy enemy in m_enemyList)
                {
                    enemy.m_currHealth = 0;
                }
            }
            if(Ramp_1 != null)
                Ramp_1.GetComponent<LiftManager>().Move = true;

            if(Ramp_2 != null)
                Ramp_2.GetComponent<LiftManager>().Move = true;
        }
    }
}