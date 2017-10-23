using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SerializationData
{
    [Serializable]
    public struct OptionsData
    {
        public float m_fMasterVolume;
        public float m_fMusicVolume;
        public float m_fBulletVolume;
        public float m_fEffectsVolume;
        public float m_fMenuButtonVolume;

        public bool m_bForceHideCursor;
    }

    public OptionsData m_optionsData;

    private void Awake()
    {
        InitialiseOptionsData(m_optionsData);
    }

    private void InitialiseOptionsData(OptionsData a_optionsData)
    {
        a_optionsData.m_fMasterVolume = 1.0f;
        a_optionsData.m_fMusicVolume = 1.0f;
        a_optionsData.m_fBulletVolume = 1.0f;
        a_optionsData.m_fEffectsVolume = 1.0f;
        a_optionsData.m_fMenuButtonVolume = 1.0f;

        a_optionsData.m_bForceHideCursor = false;
    }
}
