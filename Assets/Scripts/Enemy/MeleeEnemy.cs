using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
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
    public float timer;

    private Behaviour m_eBehaviour = Behaviour.WANDERING;

    //private Animator m_Animator;

    private Vector3 m_v3ChargeTarget = Vector3.zero;

    //private GameObject m_target;

    private NavMeshAgent m_navMeshAgent;

    private FindObjectsInRadius m_foir;

    private new void Awake()
    {
        base.Awake();
        m_foir = this.GetComponent<FindObjectsInRadius>();
        //m_Animator = GetComponent<Animator>();
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.destination = GetWanderPosition(transform.position);
        m_navMeshAgent.speed = m_fMoveSpeed;
    }

    new private void Start()
    {
        base.Start();
        //m_target = Player.m_Player.transform.gameObject;
    }

    new private void Update()
    {
        // kill code for debugging
        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Equals) && Input.GetKey(KeyCode.Alpha0))
        {
            this.m_currHealth = 0;
        }

        if (!CalculateFrustrum(IsoCam.m_playerCamera.FrustrumPlanes, m_collider))
        {
            return;
        }

        if (m_currHealth <= 0)
        {
            m_expManager.m_playerExperience += m_experienceValue;
            if (m_killStreakManager != null)
            {
                m_killStreakManager.AddKill();
            }
        }

        base.Update();

        Vector3 V_targetOffset = new Vector3(Player.m_Player.transform.position.x, transform.position.y, Player.m_Player.transform.position.z);

        switch (m_eBehaviour)
        {
            case Behaviour.WANDERING:
                #region
                {
                    m_Animator.SetBool("Walking2Recovery", false);
                    m_Animator.SetBool("Charge2Recovery", false);
                    m_Animator.SetBool("Charge2Walking", false);
                    m_Animator.SetBool("Recovery2Charge", false);
                    m_Animator.SetBool("Recovery2Walking", false);
                    // Choose point to wander to
                    if (Vector3.Distance(transform.position, m_navMeshAgent.destination) <= 1.0f)
                    {
                        m_navMeshAgent.destination = GetWanderPosition(transform.position);
                    }
                    // If enemy detects player
                    if (m_foir.m_target != null)
                    {
                        if (Vector3.Distance(transform.position, Player.m_Player.transform.position) <= 15.0f)
                        {
                            // set behaviour to prepare charge
                            m_Animator.SetBool("Walking2Recovery", true);
                            m_eBehaviour = Behaviour.PREPARING;
                            break;
                        }
                    }
                    break;
                }
            #endregion

            case Behaviour.PREPARING:
                #region
                {
                    m_Animator.SetBool("Walking2Recovery", false);
                    m_Animator.SetBool("Charge2Recovery", false);
                    m_Animator.SetBool("Charge2Walking", false);
                    m_Animator.SetBool("Recovery2Charge", false);
                    m_Animator.SetBool("Recovery2Walking", false);

                    // cease wander
                    //m_navMeshAgent.destination = transform.position;
                    // If player retreats far enough away before enemy charges, return to wander
                    if (Vector3.Distance(transform.position, Player.m_Player.transform.position) > 15.0f)
                    {
                        m_fPrepareChargeTime = 2.0f;
                        m_Animator.SetBool("Walking2Recovery", false);
                        m_Animator.SetBool("Recovery2Walking", true);
                        m_eBehaviour = Behaviour.WANDERING;
                    }

                    m_fPrepareChargeTime -= Time.deltaTime;

                    this.transform.LookAt(V_targetOffset);
                    if (m_fPrepareChargeTime <= 0.0f)
                    {
                        if (m_foir.m_target != null)
                        {
                          m_v3ChargeTarget = V_targetOffset;
                          m_navMeshAgent.enabled = false;
                          m_fPrepareChargeTime = 2.0f;
                          m_Animator.SetBool("Walking2Recovery", false);
                          m_Animator.SetBool("Recovery2Charge", true);
                          m_eBehaviour = Behaviour.CHARGING;
                          break;
                        }
                        else
                        {
                            m_fPrepareChargeTime = 2.0f;
                            m_Animator.SetBool("Walking2Recovery", false);
                            m_Animator.SetBool("Recovery2Walking", true);
                            m_eBehaviour = Behaviour.WANDERING;
                        }
                    }

                    break;
                }
            #endregion

            case Behaviour.CHARGING:
                #region
                {
                    m_Animator.SetBool("Walking2Recovery", false);
                    m_Animator.SetBool("Charge2Recovery", false);
                    m_Animator.SetBool("Charge2Walking", false);
                    m_Animator.SetBool("Recovery2Charge", false);
                    m_Animator.SetBool("Recovery2Walking", false);

                    if (timer <= 3)
                    {
                        timer += Time.time;// * 1.5f;
                        m_v3ChargeTarget = V_targetOffset;
                    }
                    
                    this.transform.LookAt(V_targetOffset);
                    transform.position = Vector3.MoveTowards(transform.position, m_v3ChargeTarget, (m_fChargeSpeed * Time.deltaTime));

                    if (DamageCheck())
                    {
                        Player.m_Player.m_bImpacted = true;
                    }

                    if (Vector3.Distance(transform.position, m_v3ChargeTarget) <= 1.5f)
                    {
                        m_Animator.SetBool("Recovery2Charge", false);
                        m_Animator.SetBool("Charge2Recovery", true);
                        m_eBehaviour = Behaviour.RECOVERING;
                        break;
                    }
                    break;
                }
            #endregion

            case Behaviour.RECOVERING:
                #region
                {
                    m_Animator.SetBool("Walking2Recovery", false);
                    m_Animator.SetBool("Charge2Recovery", false);
                    m_Animator.SetBool("Charge2Walking", false);
                    m_Animator.SetBool("Recovery2Charge", false);
                    m_Animator.SetBool("Recovery2Walking", false);

                    if (m_fRecoverTime <= 0.0f)
                    {
                        if (Vector3.Distance(transform.position, Player.m_Player.transform.position) >= 4.0f)
                        {
                            if (m_foir.m_target != null)
                            {
                                if (Vector3.Distance(transform.position, Player.m_Player.transform.position) <= 15.0f)
                                {
                                    m_navMeshAgent.enabled = true;
                                    m_navMeshAgent.speed = m_fMoveSpeed;
                                    m_fRecoverTime = 3.0f;
                                    m_eBehaviour = Behaviour.PREPARING;
                                    break;
                                }
                            }

                            m_navMeshAgent.enabled = true;
                            m_navMeshAgent.speed = m_fMoveSpeed;
                            m_fRecoverTime = 3.0f;
                            m_Animator.SetBool("Recovery2Walking", true);
                            m_eBehaviour = Behaviour.WANDERING;
                            break;
                        }

                        Vector3 v3RetreatDirection = transform.position - Player.m_Player.transform.position;
                        v3RetreatDirection = new Vector3(v3RetreatDirection.x, 0.0f, v3RetreatDirection.z).normalized;
                        transform.position += v3RetreatDirection * m_fMoveSpeed * Time.deltaTime;
                        break;
                    }

                    m_fRecoverTime -= Time.deltaTime;
                    break;
                }
            #endregion

            default:
                {
                    Debug.Log("MeleeEnemy behaviour not recognised. Name: " + gameObject.name);
                    break;
                }
        }
    }

    void FixedUpdate()
    {
        float DamagePercent = Player.m_Player.m_maxHealth * 0.25f;

        if (Player.m_Player.m_bImpacted)
        {
            if (Player.m_Player.m_iImpactTimer == 0)
            {
                Player.m_Player.m_currHealth -= (int)DamagePercent;
                //m_explosionManager.RequestExplosion(transform.position, m_v3ChargeTarget, Explosion.ExplosionType.SmallBlood, 0.0f);
            }
            Player.m_Player.m_iImpactTimer += (Time.deltaTime * 2);
            if (Player.m_Player.m_iImpactTimer > 2)
            {
                Player.m_Player.m_bImpacted = false;
                Player.m_Player.m_iImpactTimer = 0;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Bullet m_bulletScript = collision.collider.GetComponent<Bullet>();

            if (m_bulletScript != null && m_bulletScript.m_id == "Player")
            {
                //m_navMeshAgent.SetDestination(collision.collider.transform.position - m_bulletScript.m_direction);
                //m_navMeshAgent.transform.LookAt(collision.collider.transform.position - m_bulletScript.m_direction);
            }
        }
    }

    private Vector3 GetWanderPosition(Vector3 a_v3CurrentPosition)
    {
        Vector3 v3NewPosition = new Vector3(a_v3CurrentPosition.x + Random.Range(-2, 2), a_v3CurrentPosition.y, a_v3CurrentPosition.z + Random.Range(-2, 2));
        NavMeshHit navMeshHit;

        if (NavMesh.SamplePosition(v3NewPosition, out navMeshHit, 4.0f, NavMesh.AllAreas))
        {
            return v3NewPosition;
        }
        else
        {
            return GetWanderPosition(a_v3CurrentPosition);
        }
    }

    private bool DamageCheck()
    {
        if (Vector3.Distance(transform.position, Player.m_Player.transform.position) <= 1.7f)
        {
            return true;
        }

            return false;
    }
}
