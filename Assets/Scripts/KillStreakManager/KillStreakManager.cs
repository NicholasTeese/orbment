using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillStreakManager : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float m_fKillStreakImageShakeAmount = 5.0f;

    private Vector3 m_v3CurrentKillStreakImageOriginalPosition = Vector3.zero;

    private Image m_currentKillStreakImage;

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
        m_currentKillStreakImage = PlayerHUDManager.m_playerHUDManager.m_currentKillStreakImage;

        m_v3CurrentKillStreakImageOriginalPosition = IsoCam.m_playerCamera.m_uiCamera.WorldToScreenPoint(m_currentKillStreakImage.transform.position) +
                                                     new Vector3(-m_currentKillStreakImage.rectTransform.rect.width * 2, -m_currentKillStreakImage.rectTransform.rect.height * 2, 0.0f);
    }

    private void Update()
    {
        DisplayKillStreak(m_killStreak);

        if(!CheckKill())
        {
            ResetKillStreak();
        }

        if (m_currentKillStreakImage.gameObject.activeInHierarchy)
        {
            m_currentKillStreakImage.transform.localPosition = NewStreakShakePosition(m_v3CurrentKillStreakImageOriginalPosition, m_fKillStreakImageShakeAmount);
        }
    }

    private void DisplayKillStreak(int a_iKillStreak)
    {
        switch (a_iKillStreak)
        {
            case 0:
                {
                    m_currentKillStreakImage.gameObject.SetActive(false);
                    break;
                }

            case 2:
                {
                    m_currentKillStreakImage.gameObject.SetActive(true);
                    m_currentKillStreakImage.sprite = m_killStreakSprite[0];
                    break;
                }

            case 3:
                {
                    m_currentKillStreakImage.gameObject.SetActive(true);
                    m_currentKillStreakImage.sprite = m_killStreakSprite[1];
                    break;
                }

            case 4:
                {
                    m_currentKillStreakImage.gameObject.SetActive(true);
                    m_currentKillStreakImage.sprite = m_killStreakSprite[2];
                    break;
                }

            case 5:
                {
                    m_currentKillStreakImage.gameObject.SetActive(true);
                    m_currentKillStreakImage.sprite = m_killStreakSprite[3];
                    break;
                }

            case 6:
                {
                    m_currentKillStreakImage.gameObject.SetActive(true);
                    m_currentKillStreakImage.sprite = m_killStreakSprite[4];
                    break;
                }

            case 10:
                {
                    m_currentKillStreakImage.gameObject.SetActive(true);
                    m_currentKillStreakImage.sprite = m_killStreakSprite[5];
                    
                    if (Player.m_player.GodModeAvailable)
                    {
                        
                    }
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
                StartCoroutine(ActivateGodMode());
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

    public Vector3 NewStreakShakePosition(Vector3 a_v3CurrentImagePosition, float a_fMaxScreenShakeAmount)
    {
        return new Vector3((a_v3CurrentImagePosition.x + Random.Range(0.0f, a_fMaxScreenShakeAmount)), (a_v3CurrentImagePosition.y + Random.Range(0.0f, a_fMaxScreenShakeAmount)), 0.0f);
        //return new Vector3(Screen.width - (m_currentKillStreakImage.GetComponent<RectTransform>().rect.width * 2) + Random.Range(0.0f, a_fMaxScreenShakeAmount), Screen.height - (m_currentKillStreakImage.GetComponent<RectTransform>().rect.height + Random.Range(0.0f, a_fMaxScreenShakeAmount) * 2), 0.0f);
    }

    private IEnumerator ActivateGodMode()
    {
        Player.m_player.GodModeEnabled = true;
        yield return new WaitForSeconds(5.0f);
        Player.m_player.GodModeEnabled = false;
    }
}
