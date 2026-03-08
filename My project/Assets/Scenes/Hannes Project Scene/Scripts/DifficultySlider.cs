using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour
{
    public Slider slider;

    public float minTime = 100f;
    public float maxTime = 1500f;

    private void Start()
    {
        GameSettings.buildTime = maxTime; // Set default build time to maxTime
    }

    public void OnSliderChanged()
    {
        GameSettings.buildTime = Mathf.Lerp(maxTime, minTime, slider.value);
        Debug.Log($"Difficulty set to {slider.value}, build time: {GameSettings.buildTime}");
    }
}
