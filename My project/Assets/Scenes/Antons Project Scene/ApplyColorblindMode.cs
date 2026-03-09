using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ApplyColorblindMode : MonoBehaviour
{
    public Volume volume;
    private ColorAdjustments adjustments;

    void Start()
    {
        Debug.Log("Volume: " + volume);
        Debug.Log("Volume profile: " + volume.profile);

        if (!volume.profile.TryGet(out adjustments))
        {
            Debug.LogError("Could not find Color Adjustments in Volume!");
            foreach (var comp in volume.profile.components)
                Debug.Log("Component in profile: " + comp);
            return;
        }

        int index = PlayerPrefs.GetInt("ColorblindMode", 0);
        ApplyMode(index);
    }

    public void ApplyMode(int index)
    {
        switch (index)
        {
            case 0:
                adjustments.colorFilter.value = Color.white;
                break;
            case 1:
                adjustments.colorFilter.value = new Color32(145, 176, 208, 255); // Protanopia
                break;
            case 2:
                adjustments.colorFilter.value = new Color32(192, 160, 128, 255); // Deuteranopia
                break;
            case 3:
                adjustments.colorFilter.value = new Color32(160, 192, 192, 255); // Tritanopia
                break;
        }
    }
}