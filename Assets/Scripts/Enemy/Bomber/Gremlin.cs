using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gremlin : Enemy
{
    public enum Behaviour
    {
        WANDERING,
        RETREATING,
        HIDING,
        DEAD,
        FROZEN
    }

    private float m_fWanderSpeed = 3.5f;
    private float m_fRetreatSpeed = 10.0f;
    private float m_fHideTime = 10.0f;

    private Behaviour m_eBehaviour = Behaviour.WANDERING;
    private Behaviour m_eTempBehaviour = Behaviour.WANDERING;

    private Vector3 m_v3RetreatPosition = Vector3.zero;

    private List<GameObject> m_hidingSpots = new List<GameObject>();

    private NavMeshAgent m_navMeshAgent;

    private Animator m_animator;

    private FindObjectsInRadius m_findOBjectsInRadius;

    [Header("Holds the hiding spots that the bomber can flee towards.")]
    public GameObject m_hidingSpotHolder = null;

    private new void Awake()
    {
        base.Awake();

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.destination = GetWanderPosition(transform.position);
        m_navMeshAgent.speed = m_fWanderSpeed;

        m_animator = GetComponentInChildren<Animator>();

        m_findOBjectsInRadius = GetComponent<FindObjectsInRadius>();
    }

    private new void Start()
    {
        base.Start();

        foreach (Transform hidingSpot in m_hidingSpotHolder.transform)
        {
            m_hidingSpots.Add(hidingSpot.gameObject);
        }
    }

    private new void Update()
    {
        // kill code for debugging
        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Equals) && Input.GetKey(KeyCode.Alpha0))
        {
            this.m_currHealth = 0;
        }
        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Equals) && Input.GetKey(KeyCode.F))
        {
            Frozen = true;
        }

        if (m_currHealth <= 0)
        {
            ExpManager.m_experiencePointsManager.m_playerExperience += m_experienceValue;
            if (m_killStreakManager != null)
            {
                m_killStreakManager.AddKill();
            }
        }

        if (!CalculateFrustrum(IsoCam.m_playerCamera.FrustrumPlanes, m_collider))
        {
            return;
        }

        base.Update();

        if (Frozen)
        {
            m_eTempBehaviour = m_eBehaviour;
            m_eBehaviour = Behaviour.FROZEN;
        }

        CheckBehaviour();
        PerformBehavior();
    }

    private void CheckBehaviour()
    {
        if (m_currHealth <= 0.0f && m_eBehaviour != Behaviour.DEAD)
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
            m_eTempBehaviour = m_eBehaviour;
        }

        if (Vector3.Distance(transform.position, m_v3RetreatPosition) <= 3.0f && m_eBehaviour == Behaviour.RETREATING)
        {
            m_navMeshAgent.speed = m_fWanderSpeed;
            m_eBehaviour = Behaviour.HIDING;
            m_eTempBehaviour = m_eBehaviour;
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

            case Behaviour.FROZEN:
                {
                    m_navMeshAgent.destination = transform.position;
                    StartCoroutine(FreezeTime());
                    m_eBehaviour = m_eTempBehaviour;
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
