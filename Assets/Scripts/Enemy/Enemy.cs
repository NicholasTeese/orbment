using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public enum EnemyType
    {
        Hunter,
        Defender,
        Protector,
        MELEE,
        BOMBER
    }

    private bool m_bRunning = false;
    public bool Running { get { return m_bRunning; } }

    private bool m_bFrozen = false;
    public bool Frozen { get { return m_bFrozen; } set { m_bFrozen = value; } }

    protected Collider m_collider;

    protected Animator m_Animator;

    public EnemyType m_type;

    protected Vector3 m_v3TravelDir = Vector3.zero;

    private AudioClip[] m_enemyDeathClips;

    private AudioSource m_audioSource;

    protected void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider>();

        m_v3TravelDir = transform.position;

        m_enemyDeathClips = Resources.LoadAll<AudioClip>("Audio/Beta/Actors/Enemies/Death");

        m_audioSource = GetComponent<AudioSource>();
    }

    new void Update()
    {
        // Debug shortcut for instakill
        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Equals) && Input.GetKey(KeyCode.Alpha0))
        {
            this.m_currHealth = 0;
        }
        // Debug shortcut for Freeze debuff
        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Equals) && Input.GetKey(KeyCode.F))
        {
            Frozen = true;
        }

        if (m_currHealth <= 0)
        {
            m_audioSource.PlayOneShot(m_enemyDeathClips[Random.Range(0, m_enemyDeathClips.Length)]);

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

        if (Vector3.Distance(transform.position, m_agent.destination) <= 1.2f)
        {
            m_bRunning = false;
        }

        base.Update();

        if(m_type == EnemyType.Protector && Frozen == true)
        {
            StartCoroutine(FreezeTime());
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Bullet m_bulletScript = collision.collider.GetComponent<Bullet>();

            if (m_bulletScript != null && m_bulletScript.m_id == "Player")
            {
                if (Player.m_player.FreezeUnlocked != true)
                {
                    if (m_type != EnemyType.BOMBER)
                    {
                        m_bRunning = true;
                        m_v3TravelDir = m_bulletScript.m_direction;
                        m_agent.SetDestination(collision.collider.transform.position - m_bulletScript.m_direction); // Travel to bullet origin
                    }
                    //m_agent.transform.LookAt(collision.collider.transform.position - m_bulletScript.m_direction); // Look at bullet origin
                }
                else
                {
                    if (m_type != EnemyType.BOMBER)
                    {
                        m_bRunning = true;
                        m_v3TravelDir = m_bulletScript.m_direction;
                        m_agent.SetDestination(collision.collider.transform.position - m_bulletScript.m_direction); // Travel to bullet origin
                    }

                    if (Random.Range(0, 99) <= 10)
                    {
                        Frozen = true;
                    }
                }
            }
        }
    }

    private void OnGUI()
    {
        if (ExpManager.m_experiencePointsManager.PerkTreeOpen || GameManager.m_gameManager.GameIsPaused)
        {
            return;
        }
        else if (m_currHealth != m_maxHealth)
        {
            if (IsoCam.m_playerCamera != null)
            {
                Vector2 screenPoint = IsoCam.m_playerCamera.IsometricCamera.WorldToScreenPoint(this.transform.position);
                GUI.DrawTexture(new Rect(screenPoint.x - 0.5f * m_healthBarWidth, Screen.height - screenPoint.y - 40, m_healthBarWidth, 10), m_emptyBarTexture);
                GUI.DrawTexture(new Rect(screenPoint.x - 0.5f * m_healthBarWidth, Screen.height - screenPoint.y - 40, m_healthBarWidth * ((float)m_currHealth / (float)m_maxHealth), 10), m_healthBarTexture);
            }
        }
        else
        {
            if (IsoCam.m_playerCamera != null)
            {
                Vector2 screenPoint = IsoCam.m_playerCamera.IsometricCamera.WorldToScreenPoint(this.transform.position);
                GUI.Label(new Rect(screenPoint.x - 0.5f * m_healthBarWidth, Screen.height - screenPoint.y - 60, m_healthBarWidth, 50), "Lvl " + m_currLevel);
            }
        }
    }

    protected bool CalculateFrustrum(Plane[] a_frustrumPlanes, Collider a_collider)
    {
        if (GeometryUtility.TestPlanesAABB(a_frustrumPlanes, a_collider.bounds))
        {
            return true;
        }
        return false;
    }

    protected IEnumerator FreezeTime()
    {
        Debug.Log("Enum-F");
        yield return new WaitForSeconds(2.0f);
        Frozen = false;
    }
}
