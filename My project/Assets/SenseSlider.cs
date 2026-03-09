using UnityEngine;
using UnityEngine.UI;

public class SenseSlider : MonoBehaviour
{

    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void Start()
    {
        //if (AudioManager.Instance != null) slider.value = AudioManager.Instance.currentVolume;
        if (SensitivityChanger.Instance == null) return;
        slider.value = SensitivityChanger.Instance.GetSensitivity();
    }

    public void OnValueChanged(float value)
    {
        Debug.Log("Slider changed: " + value);

        if (SensitivityChanger.Instance == null)
        {
            Debug.LogError("SensitivityChanger.Instance är NULL");
            return;
        }

        SensitivityChanger.Instance.SetSensitivity(value);
    }
}