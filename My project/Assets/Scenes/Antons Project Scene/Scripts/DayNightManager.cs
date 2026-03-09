using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DayNightManager : MonoBehaviour
{
    public static DayNightManager Instance;

    [Header("UI Panels")]
    public GameObject rewardPanel;

    [Header("Fade & Day Text")]
    public Image fadeImage;      // Svart overlay
    public TMP_Text dayText;     // “Dag X”-text

    [Header("Daginställningar")]
    public float dayDuration = 300f; // 5 minuter per dag
    public float fadeDuration = 1f;

    private float timer = 0f;
    private int currentDay = 1;
    private bool returnPCLämnad = false;
    private bool dayEnding = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        // Räkna dagstid
        timer += Time.deltaTime;


        // Fade sker endast om Return PC lämnats och dagens tid är slut
       /* if (returnPCLämnad && !dayEnding && timer >= dayDuration && !rewardPanelActive)
        {
            dayEnding = true;
            StartCoroutine(EndDaySequence());
        }*/
    }

    public void StartNewDay()
    {
        if (!dayEnding && timer >= dayDuration)
        {
            dayEnding = true;
            StartCoroutine(EndDaySequence());
        }
    }
    // Anropas när Return PC lämnas tillbaka
    public void OnReturnPCClicked()
    {
        returnPCLämnad = true;
        bool rewardPanelActive = rewardPanel != null && rewardPanel.activeInHierarchy;

    }

    private IEnumerator EndDaySequence()
    {
        // Fade ut
        yield return StartCoroutine(Fade(0f, 1f));

        // Visa dagtext
        dayText.text = "Day " + currentDay;
        dayText.gameObject.SetActive(true);

        // Vänta 2 sekunder för att spelaren ska se texten
        yield return new WaitForSeconds(2f);

        // Fade in
        yield return StartCoroutine(Fade(1f, 0f));

        // Dölj dagtext
        dayText.gameObject.SetActive(false);

        // Förbered nästa dag
        currentDay++;
        timer = 0f;
        dayEnding = false;
        returnPCLämnad = false; // reset
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }
}