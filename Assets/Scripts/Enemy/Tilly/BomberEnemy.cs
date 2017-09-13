using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BomberEnemy : MonoBehaviour
{
    public enum Behaviour
    {
        WANDERING,
        RETREATING,
        HIDING,
        DEAD
    }

    private float m_fHealth = 50.0f;
    private float m_fWanderSpeed = 3.5f;
    private float m_fRetreatSpeed = 50.0f;
    private float m_fViewDistance = 10.0f;

    private Behaviour m_eBehaviour = Behaviour.WANDERING;

    private Vector3 m_v3RetreatPosition = Vector3.zero;

    private List<GameObject> m_hidingSpots = new List<GameObject>();

    private NavMeshAgent m_navMeshAgent;

    private FindObjectsInRadius m_findOBjectsInRadius;

    private void Awake()
    {
        m_hidingSpots = HidingSpotManager.m_hidingSpotManager.HidingSpots;

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.destination = GetWanderPosition(transform.position);
        m_navMeshAgent.speed = m_fWanderSpeed;

        m_findOBjectsInRadius = GetComponent<FindObjectsInRadius>();
    }

    private void Update()
    {
        Debug.Log(m_eBehaviour);
        CheckBehaviour();
        PerformBehavior();
    }

    private void CheckBehaviour()
    {
        if (m_fHealth <= 0.0f)
        {
            m_eBehaviour = Behaviour.DEAD;
            return;
        }

        if (m_findOBjectsInRadius.inSight && m_eBehaviour != Behaviour.RETREATING)
        {
            foreach (GameObject hidingSpot in m_hidingSpots)
            {
                if (m_v3RetreatPosition == Vector3.zero)
                {
                    m_v3RetreatPosition = hidingSpot.transform.position;
                }
            
                if (Vector3.Distance(Player.m_Player.transform.position, hidingSpot.transform.position) > Vector3.Distance(Player.m_Player.transform.position, m_v3RetreatPosition))
                {
                    m_v3RetreatPosition = hidingSpot.transform.position;
                }
            }

            m_navMeshAgent.speed = m_fRetreatSpeed;
            m_eBehaviour = Behaviour.RETREATING;
        }

        if (Vector3.Distance(transform.position, m_v3RetreatPosition) <= 1.0f)
        {
            m_navMeshAgent.speed = m_fWanderSpeed;
            m_eBehaviour = Behaviour.HIDING;
        }
    }

    private void PerformBehavior()
    {
        switch (m_eBehaviour)
        {
            case Behaviour.WANDERING:
                {
                    if (Vector3.Distance(transform.position, m_navMeshAgent.destination) <= 1.0f)
                    {
                        m_navMeshAgent.destination = GetWanderPosition(transform.position);
                    }
                    break;
                }

            case Behaviour.RETREATING:
                {
                    m_navMeshAgent.destination = m_v3RetreatPosition;
                    break;
                }

            case Behaviour.HIDING:
                {

                    break;
                }

            case Behaviour.DEAD:
                {

                    break;
                }

            default:
                {
                    Debug.Log("BomberEnemy behaviour not recognised. Name: " + gameObject.name);
                    break;
                }
        }
    }

    private Vector3 GetWanderPosition(Vector3 a_v3CurrentPosition)
    {
        float fXOffset = Random.Range(-5, 5);
        float fZOffset = Random.Range(-5, 5);

        return new Vector3(a_v3CurrentPosition.x + fXOffset, a_v3CurrentPosition.y, a_v3CurrentPosition.z + fZOffset);
    }
}