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

    private float m_fMoveSpeed = 2.0f;
    private float m_fChargeSpeed = 20.0f;

    private float m_fPrepareChargeTime = 2.0f;
    private float m_fRecoverTime = 0.5f;

    private Behaviour m_eBehaviour = Behaviour.WANDERING;

    private Vector3 m_v3ChargeTarget = Vector3.zero;

    public GameObject m_target;

    private NavMeshAgent m_navMeshAgent;

    private FindObjectsInRadius m_foir;

    private void Awake()
    {
        m_foir = this.GetComponent<FindObjectsInRadius>();
        //m_foir.m_sightAngle = 360;
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.destination = GetWanderPosition(transform.position);
        m_navMeshAgent.speed = m_fMoveSpeed;
    }

    private void Update()
    {
        Debug.Log(m_eBehaviour);

        switch (m_eBehaviour)
        {
            case Behaviour.WANDERING:
                {
                    if (m_foir.m_target != null)
                    {
                        if (Vector3.Distance(transform.position, m_target.transform.position) <= 15.0f)
                        {
                            m_eBehaviour = Behaviour.PREPARING;
                            break;
                        }
                    }


                    if (Vector3.Distance(transform.position, m_navMeshAgent.destination) <= 1.0f)
                    {
                        m_navMeshAgent.destination = GetWanderPosition(transform.position);
                    }
                    break;
                }

            case Behaviour.PREPARING:
                {
                    if (Vector3.Distance(transform.position, m_target.transform.position) > 15.0f)
                    {
                        m_fPrepareChargeTime = 2.0f;
                        m_eBehaviour = Behaviour.WANDERING;
                    }

                    this.transform.LookAt(m_foir.m_target);

                    m_fPrepareChargeTime -= Time.deltaTime;

                    if (m_fPrepareChargeTime <= 0.0f)
                    {
                        m_v3ChargeTarget = new Vector3(m_target.transform.position.x, transform.position.y, m_target.transform.position.z);
                        m_navMeshAgent.enabled = false;
                        m_fPrepareChargeTime = 2.0f;
                        m_eBehaviour = Behaviour.CHARGING;
                        break;
                    }
                    break;
                }

            case Behaviour.CHARGING:
                {
                    if (Vector3.Distance(transform.position, m_v3ChargeTarget) <= 1.0f)
                    {
                        m_eBehaviour = Behaviour.RECOVERING;
                        break;
                    }

                    transform.position = Vector3.MoveTowards(transform.position, m_v3ChargeTarget, (m_fChargeSpeed * Time.deltaTime));
                    
                    break;
                }

            case Behaviour.RECOVERING:
                {
                    if (m_fRecoverTime <= 0.0f)
                    {
                        if (Vector3.Distance(transform.position, m_target.transform.position) >= 4.0f)
                        {
                            if (Vector3.Distance(transform.position, m_target.transform.position) <= 15.0f)
                            {
                                m_navMeshAgent.enabled = true;
                                m_navMeshAgent.speed = m_fMoveSpeed;
                                m_fRecoverTime = 3.0f;
                                m_eBehaviour = Behaviour.PREPARING;
                                break;
                            }

                            m_navMeshAgent.enabled = true;
                            m_navMeshAgent.speed = m_fMoveSpeed;
                            m_fRecoverTime = 3.0f;
                            m_eBehaviour = Behaviour.WANDERING;
                            break;
                        }

                        Vector3 v3RetreatDirection = transform.position - m_target.transform.position;
                        v3RetreatDirection = new Vector3(v3RetreatDirection.x, 0.0f, v3RetreatDirection.z).normalized;
                        transform.position += v3RetreatDirection * m_fMoveSpeed * Time.deltaTime;
                        break;
                    }

                    m_fRecoverTime -= Time.deltaTime;
                    break;
                }

            default:
                {
                    Debug.Log("MeleeEnemy behaviour not recognised. Name: " + gameObject.name);
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
