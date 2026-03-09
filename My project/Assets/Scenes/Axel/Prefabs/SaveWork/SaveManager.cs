using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    string path;
    public bool loadBool = false;
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
        AudioManager.Instance.mixer.GetFloat("SFXVolume", out data.sfxVolume);
        AudioManager.Instance.mixer.GetFloat("MusicVolume", out data.musicVolume);
        float vol;
        bool ok = AudioManager.Instance.mixer.GetFloat("MusicVolume", out vol);
        Debug.Log($"Found: {ok}, Value: {vol}");
        data.difficulty = GameSettings.buildTime;
        data.sensitivity = SensitivityChanger.Instance.GetSensitivity();

        data.averageScore = RewardSystem.Instance.averageScore;
        data.totalOrdersCompleted = RewardSystem.Instance.totalOrdersCompleted;
        data.tutorialCompleted = TutorialScript.Instance.tutorialCompleted;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public void LoadGame()
    {
        Debug.Log("Loading game from");
        if (!File.Exists(path))
            return;

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        // gameplay
        DayNightManager.Instance.SetCurrentDay(data.dayNumber);
        RewardSystem.Instance.averageScore = (data.averageScore);
        RewardSystem.Instance.totalOrdersCompleted = (data.totalOrdersCompleted);
        TutorialScript.Instance.tutorialCompleted = data.tutorialCompleted;
        SensitivityChanger.Instance.SetSensitivity(data.sensitivity);

        // settings
        AudioManager.Instance.mixer.SetFloat("SFXVolume", data.sfxVolume);
        AudioManager.Instance.mixer.SetFloat("MusicVolume", data.musicVolume);
        GameSettings.buildTime = data.difficulty;
        loadBool = false;
        Debug.Log(data.averageScore + "THIS IS THE SCORE");
        RewardSystem.Instance.DisplayStars();
    }
}

