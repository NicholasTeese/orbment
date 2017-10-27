using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillStreakManager : MonoBehaviour
{
    private Image m_currentKillStreakSprite = null;

    public List<Sprite> m_killStreakSprite = new List<Sprite>();

    public string[] m_descriptions;
    public Color[] m_colours;

    public int m_killStreak = 0;
    public float m_timeAllowedBetweenKills = 1.0f;
    private float m_timeOfLastKill = 0.0f;

    protected float m_fGrowShrinkSpeed = 0.1f;
    protected float m_fGrowMultiplier = 1.3f;
    protected float m_fShrinkMultiplier = 1.0f;
    
    protected float m_fShakeRange = 0.2f;

    private bool m_bLifesteal = false;
    public bool Lifesteal { get; set; }

    Vector3 v3KillstreakImagePosition = Vector3.zero;

    public static KillStreakManager m_killStreakManager;

    private void Awake()
    {
        if (m_killStreakManager == null)
        {
            m_killStreakManager = this;
        }
        else if (m_killStreakManager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        m_currentKillStreakSprite = PlayerHUDManager.m_playerHUDManager.transform.Find("Kill_Streak_Image").GetComponent<Image>();
        v3KillstreakImagePosition = m_currentKillStreakSprite.transform.position;
    }

    private void Update()
    {
        DisplayKillStreak(m_killStreak);

        if(!CheckKill())
        {
            ResetKillStreak();
        }

        if(m_currentKillStreakSprite.gameObject.activeInHierarchy)
        {
            StreakShake(m_currentKillStreakSprite.gameObject);
        }
    }

    private void DisplayKillStreak(int a_iKillStreak)
    {
        //m_currentKillStreakSprite.gameObject.SetActive(true);
        //
        //if(Input.GetKeyDown(KeyCode.PageUp))
        //{
        //    StreakGrow(m_currentKillStreakSprite.gameObject);
        //}
        //if (Input.GetKeyDown(KeyCode.PageDown))
        //{
        //    StreakRecede(m_currentKillStreakSprite.gameObject);
        //}
        switch (a_iKillStreak)
        {
            case 0:
                {
                    m_currentKillStreakSprite.gameObject.SetActive(false);
                    break;
                }

            case 2:
                {
                    m_currentKillStreakSprite.gameObject.SetActive(true);
                    m_currentKillStreakSprite.sprite = m_killStreakSprite[0];
                    break;
                }

            case 3:
                {
                    m_currentKillStreakSprite.gameObject.SetActive(true);
                    m_currentKillStreakSprite.sprite = m_killStreakSprite[1];
                    break;
                }

            case 4:
                {
                    m_currentKillStreakSprite.gameObject.SetActive(true);
                    m_currentKillStreakSprite.sprite = m_killStreakSprite[2];
                    break;
                }

            case 5:
                {
                    m_currentKillStreakSprite.gameObject.SetActive(true);
                    m_currentKillStreakSprite.sprite = m_killStreakSprite[3];
                    break;
                }

            case 6:
                {
                    m_currentKillStreakSprite.gameObject.SetActive(true);
                    m_currentKillStreakSprite.sprite = m_killStreakSprite[4];
                    break;
                }

            case 10:
                {
                    m_currentKillStreakSprite.gameObject.SetActive(true);
                    m_currentKillStreakSprite.sprite = m_killStreakSprite[5];
                    break;
                }
        }
    }

    public void ResetKillStreak()
    {
        m_killStreak = 0;
        m_timeOfLastKill = 0.0f;
    }

    public void AddKill()
    {
        if (CheckKill())
        {
            m_killStreak++;
            m_timeOfLastKill = Time.time;

            if (m_killStreak >= 5)
            {
                Player.m_player.m_currHealth += (Player.m_player.m_maxHealth * 0.10f);
                //                Debug.Log(Player.m_Player.m_currHealth);
            }

            if (m_killStreak >= 10)
            {
                Player.m_player.m_bGodModeIsActive = true;
                Player.m_player.GodModeTimer = 5.0f;
            }
        }
    }

    public bool CheckKill()
    {
        return (m_timeOfLastKill == 0.0f || Time.time - m_timeOfLastKill <= m_timeAllowedBetweenKills);
    }

    public void StreakGrow(GameObject currentStreak)
    {
        if (currentStreak.transform.localScale.x < m_fGrowMultiplier)
        {
            currentStreak.transform.localScale += new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
    }

    public void StreakRecede(GameObject currentStreak)
    {
        if (currentStreak.transform.localScale.x > m_fShrinkMultiplier)
        {
            currentStreak.transform.localScale -= new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
    }

    public void StreakShake(GameObject currentStreak)
    {
        //// Move up
        //if (currentStreak.transform.position.y < m_fShakeRange)
        //{
        //    currentStreak.transform.position += new Vector3(0.0f, m_fGrowShrinkSpeed, 0.0f);
        //}
        //// Move down
        //if (currentStreak.transform.position.y > m_fShakeRange)
        //{
        //    currentStreak.transform.position -= new Vector3(0.0f, m_fGrowShrinkSpeed, 0.0f);
        //}
        //Debug.Log(currentStreak.transform.position);
        //Vector3 v3Pos = currentStreak.transform.position;
        //currentStreak.transform.position = new Vector3(Mathf.Clamp((v3Pos.x + Random.Range(0.0f, 0.05f)), v3KillstreakImagePosition.x - 0.5f, v3KillstreakImagePosition.x + 0.5f), Mathf.Clamp((v3Pos.y + Random.Range(0.0f, 0.05f)), v3KillstreakImagePosition.y - 0.5f, v3KillstreakImagePosition.y + 0.5f), v3Pos.z);
    }
}