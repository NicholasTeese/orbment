using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGremlin : BaseEnemy
{
    public enum Behaviour
    {
        WANDERING,
        RETREATING,
        HIDING,
        DEAD,
    }

    private float m_fWanderSpeed = 3.5f;
    private float m_fRetreatSpeed = 10.0f;
    private float m_fHideTime = 10.0f;

    private Behaviour m_eBehaviour = Behaviour.WANDERING;

    private Vector3 m_v3RetreatPosition = Vector3.zero;

    private List<GameObject> m_hidingSpots = new List<GameObject>();

    private FindObjectsInRadius m_findOBjectsInRadius;

    [Header("Holds the hiding spots that the bomber can flee towards.")]
    public GameObject m_hidingSpotHolder = null;

    protected override void Awake()
    {
        base.Awake();

        m_navMeshAgent.destination = GetWanderPosition(transform.position);
        m_navMeshAgent.speed = m_fWanderSpeed;

        m_findOBjectsInRadius = GetComponent<FindObjectsInRadius>();
    }

    protected void Start()
    {
        foreach (Transform hidingSpot in m_hidingSpotHolder.transform)
        {
            m_hidingSpots.Add(hidingSpot.gameObject);
        }
    }

    protected override void Update()
    {
        base.Update();

        CheckBehaviour();
        PerformBehavior();
    }

    private void CheckBehaviour()
    {
        if (m_fCurrentHealth <= 0.0f && m_eBehaviour != Behaviour.DEAD)
        {
            m_eBehaviour = Behaviour.DEAD;
            GameObject bomb = Instantiate(Resources.Load("Prefabs/Enemies/Bomber/Bomb_Beta") as GameObject);
            bomb.transform.position = transform.position;
            bomb.transform.SetParent(BombManager.m_bombManager.transform);
            return;
        }

        if (m_findOBjectsInRadius.inSight && m_eBehaviour != Behaviour.RETREATING)
        {
            int iHidingSpotIndex = Random.Range(0, m_hidingSpots.Count);

            m_v3RetreatPosition = m_hidingSpots[iHidingSpotIndex].transform.position;

            m_navMeshAgent.speed = m_fRetreatSpeed;
            m_eBehaviour = Behaviour.RETREATING;
        }

        if (Vector3.Distance(transform.position, m_v3RetreatPosition) <= 3.0f && m_eBehaviour == Behaviour.RETREATING)
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
                    m_animator.SetBool("bWalking", true);
                    m_animator.SetBool("bRunning", false);
                    m_animator.SetBool("bHiding", false);

                    if (Vector3.Distance(transform.position, m_navMeshAgent.destination) <= 1.0f)
                    {
                        m_navMeshAgent.destination = GetWanderPosition(transform.position);
                    }
                    break;
                }

            case Behaviour.RETREATING:
                {
                    m_animator.SetBool("bWalking", false);
                    m_animator.SetBool("bRunning", true);
                    m_animator.SetBool("bHiding", false);

                    m_navMeshAgent.SetDestination(m_v3RetreatPosition);
                    break;
                }

            case Behaviour.HIDING:
                {
                    m_animator.SetBool("bWalking", false);
                    m_animator.SetBool("bRunning", false);
                    m_animator.SetBool("bHiding", true);

                    m_fHideTime -= Time.deltaTime;

                    if (m_fHideTime <= 0.0f)
                    {
                        m_fHideTime = 10.0f;
                        m_eBehaviour = Behaviour.WANDERING;
                    }
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
}
