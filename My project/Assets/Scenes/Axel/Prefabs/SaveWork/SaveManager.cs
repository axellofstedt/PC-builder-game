using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    string path;
    void Awake()
    {
        path = Application.persistentDataPath + "/save.json";

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();

        // gameplay
        data.dayNumber = DayNightManager.Instance.getCurrentDay();
        //data.reputation = storeManager.reputation;

        // settings
        data.sfxVolume = 0;
        AudioManager.Instance.mixer.GetFloat("SFXvolume", out data.sfxVolume);
        data.musicVolume = 0;
        data.musicVolume = AudioManager.Instance.musicSource.volume;
        data.difficulty = GameSettings.buildTime;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public void LoadGame()
    {
        if (!File.Exists(path))
            return;

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        // gameplay
        DayNightManager.Instance.SetCurrentDay(data.dayNumber);
        //storeManager.reputation = data.reputation;

        // settings
        AudioManager.Instance.SetSFXVolume(data.sfxVolume);
        AudioManager.Instance.SetMusicVolume(data.musicVolume);
        GameSettings.buildTime = data.difficulty;
    }
}

