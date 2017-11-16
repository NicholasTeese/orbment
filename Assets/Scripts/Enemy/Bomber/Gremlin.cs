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
    }

    private float m_fWanderSpeed = 3.5f;
    private float m_fRetreatSpeed = 10.0f;
    private float m_fHideTime = 10.0f;

    private Behaviour m_eBehaviour = Behaviour.WANDERING;

    private Vector3 m_v3RetreatPosition = Vector3.zero;

    private List<GameObject> m_hidingSpots = new List<GameObject>();

    private NavMeshAgent m_navMeshAgent;

    public Animator m_animator;

    private FindObjectsInRadius m_findOBjectsInRadius;

    [Header("Holds the hiding spots that the bomber can flee towards.")]
    public GameObject m_hidingSpotHolder = null;

    private new void Awake()
    {
        base.Awake();

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.destination = GetWanderPosition(transform.position);
        m_navMeshAgent.speed = m_fWanderSpeed;

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
#if UNITY_EDITOR
        // Debug shortcut for insta-kill
        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Equals) && Input.GetKey(KeyCode.Alpha0))
        {
            this.m_currHealth = 0;
        }
#endif

        if (m_currHealth <= 0)
        {
            ExpManager.m_experiencePointsManager.m_playerExperience += m_experienceValue;
            if (m_killStreakManager != null)
            {
                m_killStreakManager.AddKill();
                AudioManager.m_audioManager.PlayOneShotEnemyDeath();
            }
        }

        if (!CalculateFrustrum(IsoCam.m_playerCamera.FrustrumPlanes, m_collider))
        {
            return;
        }

        base.Update();

        CheckBehaviour();
        PerformBehavior();
    }

    protected sealed override void OnCollisionEnter(Collision a_collision)
    {
        base.OnCollisionEnter(a_collision);

        if (a_collision.collider.CompareTag("Bullet"))
        {
            Bullet bullet = a_collision.collider.GetComponent<Bullet>();

            if (bullet == null)
            {
                Debug.Log("Bullet does not have bullet script.");
                return;
            }

            if (bullet.m_id == "Player" && m_eBehaviour == Behaviour.HIDING)
            {
                m_eBehaviour = Behaviour.RETREATING;
                m_animator.SetBool("bRun", true);
                m_animator.SetBool("bAlive", true);
                m_animator.SetBool("bHide", false);
                GetRetreatPosition();
            }
        }
    }

    private void CheckBehaviour()
    {
        if (m_currHealth <= 0.0f && m_eBehaviour != Behaviour.DEAD)
        {
            m_eBehaviour = Behaviour.DEAD;
            m_animator.SetBool("bRun", false);
            m_animator.SetBool("bAlive", false);
            m_animator.SetBool("bHide", false);
            GameObject bomb = Instantiate(Resources.Load("Prefabs/Enemies/Bomber/Gremlin_Shell") as GameObject);
            bomb.transform.position = transform.position;
            bomb.transform.SetParent(BombManager.m_bombManager.transform);
            return;
        }

        if (m_findOBjectsInRadius.inSight && m_eBehaviour != Behaviour.RETREATING)
        {
            m_animator.SetTrigger("Detect");
            m_animator.SetBool("bRun", true);
            m_animator.SetBool("bAlive", true);
            m_animator.SetBool("bHide", false);
            GetRetreatPosition();
        }

        if (Vector3.Distance(transform.position, m_v3RetreatPosition) <= 3.0f && m_eBehaviour == Behaviour.RETREATING)
        {
            m_navMeshAgent.speed = m_fWanderSpeed;
            m_eBehaviour = Behaviour.HIDING;
            m_animator.SetBool("bRun", false);
            m_animator.SetBool("bAlive", true);
            m_animator.SetBool("bHide", true);
        }
    }

    private void PerformBehavior()
    {
        switch (m_eBehaviour)
        {
            case Behaviour.WANDERING:
                {
                    if (Vector3.Distance(transform.position, m_navMeshAgent.destination) <= 1.5f)
                    {
                        m_navMeshAgent.destination = GetWanderPosition(transform.position);
                        m_animator.SetBool("bRun", true);
                        m_animator.SetBool("bAlive", true);
                        m_animator.SetBool("bHide", false);
                    }
                    break;
                }

            case Behaviour.RETREATING:
                {
                    m_navMeshAgent.SetDestination(m_v3RetreatPosition);
                    break;
                }

            case Behaviour.HIDING:
                {
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

    private Vector3 GetWanderPosition(Vector3 a_v3CurrentPosition)
    {
        return new Vector3(a_v3CurrentPosition.x + Random.Range(-5, 5), a_v3CurrentPosition.y, a_v3CurrentPosition.z + Random.Range(-5, 5));
    }

    private void GetRetreatPosition()
    {
        int iHidingSpotIndex = Random.Range(0, m_hidingSpots.Count);

        m_v3RetreatPosition = m_hidingSpots[iHidingSpotIndex].transform.position;

        m_navMeshAgent.speed = m_fRetreatSpeed;
        m_eBehaviour = Behaviour.RETREATING;
    }
}