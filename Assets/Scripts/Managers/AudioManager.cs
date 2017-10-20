using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private float m_fMusicVolume = 1.0f;
    private float m_fMusicFadeInSpeed = 0.001f;
    private float m_fMusicFadeOutSpeed = 0.005f;

    private bool m_bFadeIn = true;

    private AudioClip m_mainMenuMusic;
    private AudioClip m_daytimeMusic;

    AudioSource m_audioSource;

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

        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.loop = true;
        m_audioSource.volume = 0.0f;
    }

    private void Start()
    {
        PlaySceneMusic(LevelManager.m_levelManager.CurrentSceneName);
    }

    private void Update()
    {
        if (m_bFadeIn)
        {
            AudioFadeIn(m_audioSource, m_fMusicFadeInSpeed, m_fMusicVolume);
        }
        else
        {
            AudioFadeOut(m_audioSource, m_fMusicFadeOutSpeed);
        }
    }

    private void AudioFadeOut(AudioSource a_audioSource, float a_fFadeSpeed)
    {
        if (a_audioSource.volume <= 0.0f)
        {
            return;
        }

        a_audioSource.volume -= a_fFadeSpeed;
    }

    private void AudioFadeIn(AudioSource a_audioSource, float a_fFadeSpeed, float a_fAudioVolume)
    {
        if (a_audioSource.volume >= a_fAudioVolume)
        {
            return;
        }

        a_audioSource.volume += a_fFadeSpeed;
    }

    public void PlaySceneMusic(string a_strCurrentScene)
    {
        switch (a_strCurrentScene)
        {
            case LevelManager.m_strMainMenuSceneName:
                {
                    if (m_audioSource.clip != m_mainMenuMusic)
                    {
                        m_audioSource.clip = m_mainMenuMusic;
                        m_audioSource.Play();
                    }
                    break;
                }

            case LevelManager.m_strTutorialSceneName:
                {
                    if (m_audioSource.clip != m_daytimeMusic)
                    {
                        m_audioSource.clip = m_daytimeMusic;
                        m_audioSource.Play();
                    }
                    break;
                }

            case LevelManager.m_strLevelOneSceneName:
                {
                    if (m_audioSource.clip != m_daytimeMusic)
                    {
                        m_audioSource.clip = m_daytimeMusic;
                        m_audioSource.Play();
                    }
                    break;
                }

            case LevelManager.m_strLevelTwoSceneName:
                {
                    if (m_audioSource.clip != m_daytimeMusic)
                    {
                        m_audioSource.clip = m_daytimeMusic;
                        m_audioSource.Play();
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
}
