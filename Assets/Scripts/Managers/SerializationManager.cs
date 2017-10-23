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
    /// Options data being saved.
    /// </summary>
    /// <param name="a_serializationData"></param>
    private void SaveOptionsData(SerializationData a_serializationData)
    {
        // Audio volume multipliers.
        a_serializationData.m_optionsData.m_fMasterVolume = AudioManager.m_audioManager.MasterVolume;
        a_serializationData.m_optionsData.m_fMusicVolume = AudioManager.m_audioManager.MusicVolume;
        a_serializationData.m_optionsData.m_fBulletVolume = AudioManager.m_audioManager.BulletVolume;
        a_serializationData.m_optionsData.m_fEffectsVolume = AudioManager.m_audioManager.EffectsVolume;
        a_serializationData.m_optionsData.m_fMenuButtonVolume = AudioManager.m_audioManager.MenuVolume;

        // Other misc options.
        a_serializationData.m_optionsData.m_bForceHideCursor = GameManager.m_gameManager.ForceHideCursor;
    }

    /// <summary>
    /// Options data being loaded.
    /// </summary>
    /// <param name="a_serializationData"></param>
    private void LoadOptionsData(SerializationData a_serializationData)
    {
        // Audio volumes.
        AudioManager.m_audioManager.MasterVolume = a_serializationData.m_optionsData.m_fMasterVolume;
        AudioManager.m_audioManager.MusicVolume = a_serializationData.m_optionsData.m_fMusicVolume;
        AudioManager.m_audioManager.BulletVolume = a_serializationData.m_optionsData.m_fBulletVolume;
        AudioManager.m_audioManager.EffectsVolume = a_serializationData.m_optionsData.m_fEffectsVolume;
        AudioManager.m_audioManager.MenuVolume = a_serializationData.m_optionsData.m_fMenuButtonVolume;
        AudioManager.m_audioManager.UpdateVolumes();

        // Other misc options.
        GameManager.m_gameManager.ForceHideCursor = a_serializationData.m_optionsData.m_bForceHideCursor;
    }

    /// <summary>
    /// Save game data.
    /// </summary>
    public void Save()
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
    public void Load()
    {
        // If the save file does not exist, return.
        if (!File.Exists(m_strFullSaveFileDirectory))
        {
            return;
        }

        // Initialise BinaryFormatter & FileStream at save directory.
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(m_strFullSaveFileDirectory, FileMode.Open);     // A save has not been made yet, return.
        if (fileStream.Length == 0)
        {
            return;
        }

        // Deserialize SerializationData, load it & close FileStream.
        m_serializationData = (SerializationData)binaryFormatter.Deserialize(fileStream);
        LoadOptionsData(m_serializationData);
        fileStream.Close();
    }
}
