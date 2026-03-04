using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DayNightManager : MonoBehaviour
{
    public static DayNightManager Instance;
    private float dayExposure;
    private Color daySkyTint;

    private Color nightSkyTint = new Color(0.02f, 0.02f, 0.1f);
    private float nightExposure = 0.2f;
    [Header("Dag / timer")]
    public int day = 1;
    public float dayLength = 20f; // total dag i sekunder
    private bool dayEndingReady = false; // blir true när timer är klar

    [Header("Natt / övergång")]
    public float nightTransitionTime = 5f; // fade tid
    public Light sunLight;
    public Material skyboxMaterial;

    [Header("UI")]
    public Image fadeImage;
    public TMP_Text messageText;
    public Button beginButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        fadeImage.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false);
        beginButton.gameObject.SetActive(false);

        dayExposure = skyboxMaterial.GetFloat("_Exposure");
        daySkyTint = skyboxMaterial.GetColor("_SkyTint");

        StartCoroutine(DayTimer());
    }

    private IEnumerator DayTimer()
    {
        // Vänta dagLength sekunder minus tid för nattövergång
        yield return new WaitForSeconds(dayLength - nightTransitionTime);
        dayEndingReady = true;
        Debug.Log("Dag nära slut, natt kan triggas när spelaren lämnar datorn.");
    }

    // Ska kallas från datorn när spelaren lämnar den
    public void PlayerLeftComputer()
    {
        if (dayEndingReady)
        {
            TriggerEndOfDay();
        }
    }

    private void TriggerEndOfDay()
    {
        StartCoroutine(EndDaySequence());
    }

    private IEnumerator EndDaySequence()
    {
        float elapsed = 0f;

        // 1. Mörka himlen + ljus
        while (elapsed < nightTransitionTime)
        {
            float t = elapsed / nightTransitionTime;

            skyboxMaterial.SetFloat("_Exposure",
                Mathf.Lerp(dayExposure, nightExposure, t));

            skyboxMaterial.SetColor("_SkyTint",
                Color.Lerp(daySkyTint, nightSkyTint, t));

            sunLight.intensity = Mathf.Lerp(1f, 0f, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 2. Fade till svart
        fadeImage.gameObject.SetActive(true);
        elapsed = 0f;

        while (elapsed < nightTransitionTime)
        {
            fadeImage.color = new Color(0, 0, 0, elapsed / nightTransitionTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 3. Text
        messageText.gameObject.SetActive(true);
        messageText.text = "Day Over";
        yield return new WaitForSeconds(2f);

        // 4. Fade tillbaka
        elapsed = 0f;
        while (elapsed < nightTransitionTime)
        {
            fadeImage.color = new Color(0, 0, 0, 1f - elapsed / nightTransitionTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);

        // 5. Ny dag
        messageText.text = "Dag " + (++day);
        beginButton.gameObject.SetActive(true);

        beginButton.onClick.RemoveAllListeners();
        beginButton.onClick.AddListener(StartNewDay);
    }
    private void StartNewDay()
    {
        messageText.gameObject.SetActive(false);
        beginButton.gameObject.SetActive(false);

        skyboxMaterial.SetFloat("_Exposure", dayExposure);
        skyboxMaterial.SetColor("_SkyTint", daySkyTint);
        sunLight.intensity = 1f;

        dayEndingReady = false;
        StartCoroutine(DayTimer());
    }
}