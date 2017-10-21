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

    private AudioClip m_mainMenuMusic;
    private AudioClip m_daytimeMusic;

    AudioSource m_musicAudioSource;
    AudioSource m_bulletAudioSource;
    AudioSource m_effectsAudioSource;
    AudioSource m_menuAudioSource;

    public static AudioManager m_audioManager;

    public bool FadeIn { get { return m_bFadeIn; } set { m_bFadeIn = value; } }

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

        m_mainMenuMusic = Resources.Load("Audio/Beta/Music/Main_Menu_Music") as AudioClip;
        m_daytimeMusic = Resources.Load("Audio/Beta/Music/Daytime_Music") as AudioClip;

        m_musicAudioSource = GetComponent<AudioSource>();
        m_musicAudioSource.loop = true;
        m_musicAudioSource.volume = 0.0f;
    }

    private void Start()
    {
        m_bulletAudioSource = Player.m_Player.m_currWeapon.BulletAudioSource;
        m_effectsAudioSource = IsoCam.m_playerCamera.GetComponent<AudioSource>();
        m_menuAudioSource = PauseMenuManager.m_pauseMenuManager.transform.parent.GetComponent<AudioSource>();

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
                    if (m_musicAudioSource.clip != m_daytimeMusic)
                    {
                        m_musicAudioSource.clip = m_daytimeMusic;
                        m_musicAudioSource.Play();
                    }
                    break;
                }

            case LevelManager.m_strLevelOneSceneName:
                {
                    if (m_musicAudioSource.clip != m_daytimeMusic)
                    {
                        m_musicAudioSource.clip = m_daytimeMusic;
                        m_musicAudioSource.Play();
                    }
                    break;
                }

            case LevelManager.m_strLevelTwoSceneName:
                {
                    if (m_musicAudioSource.clip != m_daytimeMusic)
                    {
                        m_musicAudioSource.clip = m_daytimeMusic;
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

    public void AdjustMasterVolume(float a_fMasterVolume)
    {
        m_fMasterVolume = a_fMasterVolume;
        AudioListener.volume = m_fMasterVolume;
    }

    public void AdjustMusicVolume(float a_fMusicVolume)
    {
        m_fMusicVolume = a_fMusicVolume;
        m_musicAudioSource.volume = m_fMusicVolume;
    }

    public void AdjustBulletVolume(float a_fBulletVolume)
    {
        m_fBulletVolume = a_fBulletVolume;
        m_bulletAudioSource.volume = m_fBulletVolume;
    }

    public void AdjustEffectsVolume(float a_fEffectsVolume)
    {
        m_fEffectsVolume = a_fEffectsVolume;
        m_effectsAudioSource.volume = m_fEffectsVolume;
    }

    public void AdjustMenuVolume(float a_fMenuVolume)
    {
        m_fMenuVolume = a_fMenuVolume;
        m_menuAudioSource.volume = m_fMenuVolume;
    }
}
