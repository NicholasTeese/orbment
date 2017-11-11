using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private float m_fMasterVolume = 1.0f;
    private float m_fMusicVolume = 1.0f;
    private float m_fBulletVolume = 1.0f;
    private float m_fEffectsVolume = 1.0f;
    private float m_fMenuVolume = 1.0f;
    private float m_fMusicFadeInSpeed = 0.001f;
    private float m_fMusicFadeOutSpeed = 0.005f;

    private bool m_bFadeIn = true;
    private bool m_bFadeComplete = false;

    private AudioClip[] m_enemyDeathClips;

    [Header("Audio Clips")]
    public AudioClip m_inGameMusic;
    public AudioClip m_mainMenuMusic;
    public AudioClip m_menuClick;
    public AudioClip m_perkApplied;
    public AudioClip m_perkUnavailable;
    public AudioClip m_perkSelected;

    public AudioClip[] m_playerNormalBullets;
    public AudioClip[] m_playerFireBullets;
    public AudioClip[] m_playerDash;
    public AudioClip[] m_orbPickup;
    public AudioClip[] m_fireExplosions;
    public AudioClip[] m_wallBreak;
    public AudioClip[] m_enemyDeath;

    [Header("Audio Sources")]
    public AudioSource m_musicAudioSource;
    public AudioSource m_playerbulletAudioSource;
    public AudioSource m_effectsAudioSource;
    public AudioSource m_playerDashAudioSource;
    public AudioSource m_playerOrbPickupAudioSource;
    public AudioSource m_explosionAudioSource;
    public AudioSource m_wallBreakAudioSource;
    public AudioSource m_enemyDeathAudioSource;
    public AudioSource m_perkTreeAudioSource;
    public AudioSource m_menuAudioSource;

    public static AudioManager m_audioManager;

    public float MasterVolume { get { return m_fMasterVolume; } set { m_fMasterVolume = value; } }
    public float MusicVolume { get { return m_fMusicVolume; } set { m_fMusicVolume = value; } }
    public float BulletVolume { get { return m_fBulletVolume; } set { m_fBulletVolume = value; } }
    public float EffectsVolume { get { return m_fEffectsVolume; } set { m_fEffectsVolume = value; } }
    public float MenuVolume { get { return m_fMusicVolume; } set { m_fMenuVolume = value; } }

    public bool FadeIn { get { return m_bFadeIn; } set { m_bFadeIn = value; } }

    public AudioClip[] EnemyDeathClips { get { return m_enemyDeathClips; } }

    public AudioSource EffectsAudioSource { get { return m_effectsAudioSource; } }
    public AudioSource ExplosionAudioSource { get { return m_explosionAudioSource; } }
    public AudioSource PerkTreeAudioSource { get { return m_perkTreeAudioSource; } }

    private void Awake()
    {
        if (m_audioManager == null)
        {
            m_audioManager = this;
        }
        else if (m_audioManager != this)
        {
            Destroy(gameObject);
        }

        m_musicAudioSource.loop = true;
    }

    private void Start()
    {
        PlaySceneMusic(LevelManager.m_levelManager.CurrentSceneName);
    }

    private void Update()
    {
        if (m_bFadeIn && !m_bFadeComplete)
        {
            AudioFadeIn(m_musicAudioSource, m_fMusicFadeInSpeed, m_fMusicVolume);
        }
        else if (!m_bFadeIn && !m_bFadeComplete)
        {
            AudioFadeOut(m_musicAudioSource, m_fMusicFadeOutSpeed);
        }
    }

    private void AudioFadeIn(AudioSource a_audioSource, float a_fFadeSpeed, float a_fAudioVolume)
    {
        if (a_audioSource.volume >= a_fAudioVolume)
        {
            m_bFadeComplete = true;
            return;
        }

        a_audioSource.volume += a_fFadeSpeed;
    }

    private void AudioFadeOut(AudioSource a_audioSource, float a_fFadeSpeed)
    {
        if (a_audioSource.volume <= 0.0f)
        {
            return;
        }

        a_audioSource.volume -= a_fFadeSpeed;
    }

    public void PlaySceneMusic(string a_strCurrentScene)
    {
        switch (a_strCurrentScene)
        {
            case LevelManager.m_strMainMenuSceneName:
                {
                    if (m_musicAudioSource.clip != m_mainMenuMusic)
                    {
                        m_musicAudioSource.clip = m_mainMenuMusic;
                        m_musicAudioSource.Play();
                    }
                    break;
                }

            case LevelManager.m_strTutorialSceneName:
                {
                    if (m_musicAudioSource.clip != m_inGameMusic)
                    {
                        m_musicAudioSource.clip = m_inGameMusic;
                        m_musicAudioSource.Play();
                    }
                    break;
                }

            case LevelManager.m_strLevelOneSceneName:
                {
                    if (m_musicAudioSource.clip != m_inGameMusic)
                    {
                        m_musicAudioSource.clip = m_inGameMusic;
                        m_musicAudioSource.Play();
                    }
                    break;
                }

            case LevelManager.m_strLevelTwoSceneName:
                {
                    if (m_musicAudioSource.clip != m_inGameMusic)
                    {
                        m_musicAudioSource.clip = m_inGameMusic;
                        m_musicAudioSource.Play();
                    }
                    break;
                }

            default:
                {
                    Debug.Log(a_strCurrentScene + " scene name not recognised.");
                    break;
                }
        }
    }

    public void UpdateVolumes()
    {
        AudioListener.volume = m_fMasterVolume;

        if (m_musicAudioSource != null)
        {
            m_musicAudioSource.volume = m_fMusicVolume;
        }

        if (m_playerbulletAudioSource != null)
        {
            m_playerbulletAudioSource.volume = m_fBulletVolume;
        }

        if (m_effectsAudioSource != null)
        {
            m_effectsAudioSource.volume = m_fEffectsVolume;
        }

        if (m_explosionAudioSource != null)
        {
            m_explosionAudioSource.volume = m_fEffectsVolume;
        }

        if (m_menuAudioSource != null)
        {
            m_menuAudioSource.volume = m_fEffectsVolume;
        }
    }

    public void AdjustMasterVolume(float a_fMasterVolume)
    {
        m_fMasterVolume = a_fMasterVolume;
        AudioListener.volume = m_fMasterVolume;
    }

    public void AdjustMusicVolume(float a_fMusicVolume)
    {
        m_fMusicVolume = a_fMusicVolume;

        if (m_musicAudioSource != null)
        {
            m_musicAudioSource.volume = m_fMusicVolume;
        }
    }

    public void AdjustBulletVolume(float a_fBulletVolume)
    {
        m_fBulletVolume = a_fBulletVolume;

        if (m_playerbulletAudioSource != null)
        {
            m_playerbulletAudioSource.volume = m_fBulletVolume;
        }
    }

    public void AdjustEffectsVolume(float a_fEffectsVolume)
    {
        m_fEffectsVolume = a_fEffectsVolume;

        if (m_effectsAudioSource != null)
        {
            m_effectsAudioSource.volume = m_fEffectsVolume;
        }

        if (m_explosionAudioSource != null)
        {
            m_explosionAudioSource.volume = m_fEffectsVolume;
        }
    }

    public void AdjustMenuVolume(float a_fMenuVolume)
    {
        m_fMenuVolume = a_fMenuVolume;

        if (m_menuAudioSource != null)
        {
            m_menuAudioSource.volume = m_fMenuVolume;
        }
    }

    public void PlayOneShotPlayerNormalBullet()
    {
        m_playerbulletAudioSource.PlayOneShot(m_playerNormalBullets[Random.Range(0, m_playerNormalBullets.Length)]);
    }

    public void PlayOneShotPlayerFireBullet()
    {
        m_playerbulletAudioSource.PlayOneShot(m_playerFireBullets[Random.Range(0, m_playerNormalBullets.Length)]);
    }

    public void PlayOneShotFireExplosion()
    {
        m_explosionAudioSource.PlayOneShot(m_fireExplosions[Random.Range(0, m_fireExplosions.Length)]);
    }

    public void PlayOneShotWallBreak()
    {
        m_wallBreakAudioSource.PlayOneShot(m_wallBreak[Random.Range(0, m_wallBreak.Length)]);
    }

    public void PlayOneShotMenuClick()
    {
        m_menuAudioSource.PlayOneShot(m_menuClick);
    }

    public void PlayOneShotPerkApplied()
    {
        m_menuAudioSource.PlayOneShot(m_perkApplied);
    }

    public void PlayOneShotPerkUnavailable()
    {
        m_menuAudioSource.PlayOneShot(m_perkUnavailable);
    }

    public void PlayOneShotPerkSelected()
    {
        m_menuAudioSource.PlayOneShot(m_perkSelected);
    }

    public void PlayOneShotPlayerDash()
    {
        m_playerDashAudioSource.PlayOneShot(m_playerDash[Random.Range(0, m_playerDash.Length)]);
    }

    public void PlayOneShotOrbPickup()
    {
        m_playerOrbPickupAudioSource.PlayOneShot(m_orbPickup[Random.Range(0, m_orbPickup.Length)]);
    }

    public void PlayOneShotEnemyDeath()
    {
        m_enemyDeathAudioSource.PlayOneShot(m_enemyDeath[Random.Range(0, m_enemyDeath.Length)]);
    }
}
