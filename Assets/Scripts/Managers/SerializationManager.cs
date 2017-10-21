using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManager : MonoBehaviour
{
    private string m_strSaveFolderName = "Orbment_Save_Data";
    private string m_strSaveName = "Options_Data";
    private string m_strFullSaveFileDirectory;

    private SerializationData m_serializationData;

    public static SerializationManager m_serializationManager;

    private void Awake()
    {
        if (m_serializationManager == null)
        {
            m_serializationManager = this;
        }
        else if (m_serializationManager != this)
        {
            Destroy(gameObject);
        }

        m_strFullSaveFileDirectory = Application.persistentDataPath + "/" + m_strSaveFolderName + "/" + m_strSaveName + ".dat";
    }

    /// <summary>
    /// Save game data.
    /// </summary>
    private void Save()
    {
        // Save does not have a name. Return.
        if (m_strSaveName == null)
        {
            return;
        }

        // Save folder does not have a name. Return.
        if (m_strSaveFolderName == null)
        {
            return;
        }

        // If the directory does not exist, create it.
        if (!Directory.Exists(Application.persistentDataPath + "/" + m_strSaveFolderName))
        {
            DirectoryInfo directoryInfo = Directory.CreateDirectory(Application.persistentDataPath + "/" + m_strSaveFolderName);

            // Save directory does not exists and could not be created. Return.
            if (!Directory.Exists(directoryInfo.FullName))
            {
                return;
            }
        }

        // If the save file already exists, delete it.
        if (File.Exists(m_strFullSaveFileDirectory))
        {
            File.Delete(m_strFullSaveFileDirectory);
        }

        // Initialise BinaryFormatter & FileStream at save directory.
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(m_strFullSaveFileDirectory);

        // Initialise SerializationData & gather save data.
        SerializationData serializationData = new SerializationData();
        SaveOptionsData(serializationData);

        // Serialize save data & close FileStream.
        binaryFormatter.Serialize(fileStream, serializationData);
        fileStream.Close();
    }

    /// <summary>
    /// Load game data.
    /// </summary>
    private void Load()
    {
        // If the save file does not exist, return.
        if (!File.Exists(m_strFullSaveFileDirectory))
        {
            return;
        }

        // Initialise BinaryFormatter & FileStream at save directory.
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(m_strFullSaveFileDirectory);

        // Deserialize SerializationData & close FileStream.
        m_serializationData = (SerializationData)binaryFormatter.Deserialize(fileStream);
        fileStream.Close();
    }

    /// <summary>
    /// Options data being saved.
    /// </summary>
    /// <param name="a_serializationData"></param>
    private void SaveOptionsData(SerializationData a_serializationData)
    {
        // Audio volume multipliers.
        a_serializationData.m_optionsData.m_fMasterVolumeMultiplier = 1.0f;
        a_serializationData.m_optionsData.m_fMusicVolumeMultiplier = 1.0f;
        a_serializationData.m_optionsData.m_fBulletVolumeMultiplier = 1.0f;
        a_serializationData.m_optionsData.m_fEffectsVolumeMultiplier = 1.0f;
        a_serializationData.m_optionsData.m_fMenuButtonVolumeMultiplier = 1.0f;

        // Other misc options.
        a_serializationData.m_optionsData.m_bForceHideCursor = false;
    }

    /// <summary>
    /// Options data being loaded.
    /// </summary>
    /// <param name="a_serializationData"></param>
    private void LoadOptionsData(SerializationData a_serializationData)
    {
        //// Audio volume multipliers.
        //a_serializationData.m_optionsData.m_fMasterVolumeMultiplier;
        //a_serializationData.m_optionsData.m_fMusicVolumeMultiplier;
        //a_serializationData.m_optionsData.m_fBulletVolumeMultiplier;
        //a_serializationData.m_optionsData.m_fEffectsVolumeMultiplier
        //a_serializationData.m_optionsData.m_fMenuButtonVolumeMultiplier;
        //
        //// Other misc options.
        //a_serializationData.m_optionsData.m_bForceHideCursor;
    }
}
