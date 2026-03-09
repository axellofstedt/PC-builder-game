using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PersistentVolume : MonoBehaviour
{
    public static PersistentVolume Instance;
    public Volume volume;

    void Awake()
    {
        // Singleton för att undvika dubbla Volumes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initiera med PlayerPrefs
            ApplyColorblindModeFromPrefs();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplyColorblindModeFromPrefs()
    {
        if (!volume.profile.TryGet(out ColorAdjustments adjustments))
        {
            Debug.LogError("Could not find Color Adjustments in Volume!");
            return;
        }

        int index = PlayerPrefs.GetInt("ColorblindMode", 0);
        switch (index)
        {
            case 0: adjustments.colorFilter.value = Color.white; break;
            case 1: adjustments.colorFilter.value = new Color32(145, 176, 208, 255); break;
            case 2: adjustments.colorFilter.value = new Color32(192, 160, 128, 255); break;
            case 3: adjustments.colorFilter.value = new Color32(160, 192, 192, 255); break;
        }
    }

    // Kan kallas från dropdown i MainMenu
    public void SetColorblindMode(int index)
    {
        PlayerPrefs.SetInt("ColorblindMode", index);
        PlayerPrefs.Save();
        ApplyColorblindModeFromPrefs();
    }
}