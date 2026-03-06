using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public enum VolumeType { Music, SFX }
    public VolumeType volumeType;

    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void Start()
    {
        if (AudioManager.Instance != null) slider.value = AudioManager.Instance.currentVolume;
    }

    public void OnValueChanged(float value)
    {
        Debug.Log("Slider changed: " + value);

        if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager.Instance är NULL");
            return;
        }

        if (volumeType == VolumeType.Music)
            AudioManager.Instance.SetMusicVolume(value);
        else
            AudioManager.Instance.SetSFXVolume(value);
    }
}