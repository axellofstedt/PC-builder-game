using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SimpleColorblindMode : MonoBehaviour
{
    public Volume volume;
    private ColorAdjustments adjustments;

    void Start()
    {
        volume.profile.TryGet(out adjustments);
    }

    public void SetProtanopia()
    {
        adjustments.colorFilter.value = new Color32(145, 176, 208, 255);
    }

    public void SetDeuteranopia()
    {
        adjustments.colorFilter.value = new Color32(192, 160, 128, 255);
    }

    public void SetTritanopia()
    {
        adjustments.colorFilter.value = new Color32(160, 192, 192, 255);
    }

    public void SetNormal()
    {
        adjustments.colorFilter.value = Color.white;
    }
}