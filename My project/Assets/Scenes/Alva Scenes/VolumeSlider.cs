using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public enum VolumeType { Music, SFX }
    public VolumeType volumeType;

    void Awake()
    {
        GetComponent<Slider>().onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float value)
    {
        Debug.Log("Slider changed: " + value);

        if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager.Instance är NULL");
            return;
        }

        Debug.Log("Calling AudioManager on: " + AudioManager.Instance.gameObject.name);

        if (volumeType == VolumeType.Music)
            AudioManager.Instance.SetMusicVolume(value);
        else
            AudioManager.Instance.SetSFXVolume(value);
    }
}