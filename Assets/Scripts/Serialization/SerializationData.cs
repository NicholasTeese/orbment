using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationData
{
    [Serializable]
    public struct OptionsData
    {
        public float m_fMasterVolumeMultiplier;
        public float m_fMusicVolumeMultiplier;
        public float m_fBulletVolumeMultiplier;
        public float m_fEffectsVolumeMultiplier;
        public float m_fMenuButtonVolumeMultiplier;

        public bool m_bForceHideCursor;
    }

    public OptionsData m_optionsData;

    private void Awake()
    {
        InitialiseOptionsData(m_optionsData);
    }

    private void InitialiseOptionsData(OptionsData a_optionsData)
    {
        a_optionsData.m_fMasterVolumeMultiplier = 1.0f;
        a_optionsData.m_fMusicVolumeMultiplier = 1.0f;
        a_optionsData.m_fBulletVolumeMultiplier = 1.0f;
        a_optionsData.m_fEffectsVolumeMultiplier = 1.0f;
        a_optionsData.m_fMenuButtonVolumeMultiplier = 1.0f;

        a_optionsData.m_bForceHideCursor = false;
    }
}
