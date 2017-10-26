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
    protected float m_fGrowMultiplier = 1.5f;
    protected float m_fShrinkMultiplier = 1.0f;
    protected float m_fShakeRange = 0.2f;

    private bool m_bLifesteal = false;
    public bool Lifesteal { get; set; }

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

        //m_killStreakSprite = InitialiseKillStreakSprites();
    }

    private void Start()
    {
        m_currentKillStreakSprite = PlayerHUDManager.m_playerHUDManager.transform.Find("Kill_Streak_Image").GetComponent<Image>();
    }

    private void Update()
    {
        DisplayKillStreak(m_killStreak);

        if(!CheckKill())
        {
            ResetKillStreak();
        }
    }

    //x private List<Sprite> InitialiseKillStreakSprites()
    //x {
    //x     List<Sprite> killStreakImages = new List<Sprite>();
    //x     killStreakImages.Add(Resources.Load("Textures/UI/KillStreak/2") as Sprite);
    //x     killStreakImages.Add(Resources.Load("Textures/UI/KillStreak/3") as Sprite);
    //x     killStreakImages.Add(Resources.Load("Textures/UI/KillStreak/4") as Sprite);
    //x     killStreakImages.Add(Resources.Load("Textures/UI/KillStreak/5") as Sprite);
    //x     killStreakImages.Add(Resources.Load("Textures/UI/KillStreak/6") as Sprite);
    //x     killStreakImages.Add(Resources.Load("Textures/UI/KillStreak/10") as Sprite);
    //x     return killStreakImages;
    //x }

    private void DisplayKillStreak(int a_iKillStreak)
    {
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

    public void StreakSizing()
    {
        // Grow
        if (transform.localScale.x < m_fGrowMultiplier)
        {
            transform.localScale += new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
        // Shrink
        if (transform.localScale.x > m_fShrinkMultiplier)
        {
            transform.localScale -= new Vector3(m_fGrowShrinkSpeed, m_fGrowShrinkSpeed, 0.0f);
        }
    }

    public void StreakShake()
    {
        // Move up
        if (transform.position.y < m_fShakeRange)
        {
            transform.position += new Vector3(0.0f, m_fGrowShrinkSpeed, 0.0f);
        }
        // Move down
        if (transform.position.y > m_fShakeRange)
        {
            transform.position -= new Vector3(0.0f, m_fGrowShrinkSpeed, 0.0f);
        }
    }
}