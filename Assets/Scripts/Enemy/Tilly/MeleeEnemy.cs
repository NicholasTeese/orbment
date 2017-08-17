using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public enum Behaviour
    {
        WANDERING,
        PREPARING,
        CHARGING,
        RECOVERING
    }

    private float m_fWanderSpeed = 2.0f;
    private float m_fChargeSpeed = 300.0f;
    private float m_fRecoverSpeed = 0.5f;

    private float m_fPrepareChargeTime = 2.0f;
    private float m_fRecoverTime = 3.0f;

    private Behaviour m_eBehaviour = Behaviour.WANDERING;

    private Vector3 m_v3ChargeTarget = Vector3.zero;

    public GameObject m_target;

    private NavMeshAgent m_navMeshAgent;

    private void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.destination = GetWanderPosition(transform.position);
        m_navMeshAgent.speed = m_fWanderSpeed;
    }

    private void Update()
    {
        switch (m_eBehaviour)
        {
            case Behaviour.WANDERING:
                {
                    if (Vector3.Distance(transform.position, m_target.transform.position) <= 15.0f)
                    {
                        m_eBehaviour = Behaviour.PREPARING;
                        break;
                    }

                    m_navMeshAgent.speed = m_fWanderSpeed;

                    if (Vector3.Distance(transform.position, m_navMeshAgent.destination) <= 1.0f)
                    {
                        m_navMeshAgent.destination = GetWanderPosition(transform.position);
                    }
                    break;
                }

            case Behaviour.PREPARING:
                {
                    m_fPrepareChargeTime -= Time.deltaTime;

                    if (m_fPrepareChargeTime <= 0.0f)
                    {
                        m_v3ChargeTarget = m_target.transform.position;
                        m_fPrepareChargeTime = 2.0f;
                        m_eBehaviour = Behaviour.CHARGING;
                        break;
                    }
                    break;
                }

            case Behaviour.CHARGING:
                {
                    if (Vector3.Distance(transform.position, m_v3ChargeTarget) <= 3.0f)
                    {
                        m_eBehaviour = Behaviour.RECOVERING;
                        break;
                    }

                    m_navMeshAgent.speed = m_fChargeSpeed;

                    if (m_navMeshAgent.destination != m_v3ChargeTarget)
                    {
                        m_navMeshAgent.destination = m_v3ChargeTarget;
                    }
                    break;
                }

            case Behaviour.RECOVERING:
                {
                    m_fRecoverTime -= Time.deltaTime;

                    if (m_navMeshAgent.destination != m_target.transform.position)
                    {
                        m_navMeshAgent.destination = GetWanderPosition(transform.position);
                        m_navMeshAgent.speed = m_fRecoverSpeed;
                        break;
                    }

                    if (m_fRecoverTime <= 0.0f)
                    {
                        if (Vector3.Distance(transform.position, m_target.transform.position) <= 15.0f)
                        {
                            m_eBehaviour = Behaviour.PREPARING;
                            break;
                        }

                        m_eBehaviour = Behaviour.WANDERING;
                    }
                    break;
                }

            default:
                {
                    Debug.Log("Enemy behaviour not recognised. Name: " + gameObject.name);
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
