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
        HIDING
    }

    private float m_fMoveSpeed = 2.0f;

    private Behaviour m_eBehaviour = Behaviour.WANDERING;

    private NavMeshAgent m_navMeshAgent;

    private void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.speed = m_fMoveSpeed;
    }

    private void Update()
    {

    }

    private Vector3 GetWanderPosition(Vector3 a_v3CurrentPosition)
    {
        float fXOffset = Random.Range(-5, 5);
        float fZOffset = Random.Range(-5, 5);

        return new Vector3(a_v3CurrentPosition.x + fXOffset, a_v3CurrentPosition.y, a_v3CurrentPosition.z + fZOffset);
    }
}