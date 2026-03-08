using UnityEngine;
using UnityEngine.Rendering;

public class RewardSystem : MonoBehaviour
{
    [Header("Time settings")]
    private float maxAllowedTime = GameSettings.buildTime; // This will be set dynamically based on the difficulty slider

    [Header("Order settings")]
    public int TotalComponents { get; set; } = 9;

    private void Update()
    {
        maxAllowedTime = GameSettings.buildTime; // Update max allowed time based on the current difficulty setting
        // print($"Updated max allowed time: {maxAllowedTime:F2}s");
    }

    // Main evaluation method
    public RewardResult Evaluate(float actualTime, int correctComponents)
    {
        // Set total components based on the current order
        TotalComponents = OrderManager.Instance.currentOrder.Count;

        float timeScore = CalculateTimeScore(actualTime);
        float accuracyScore = CalculateAccuracyScore(correctComponents);

        float finalScore = (timeScore + accuracyScore) / 2f;
        int stars = CalculateStars(finalScore);

        Debug.Log($"Evaluation: Time={actualTime:F2}s, Correct={correctComponents}/{TotalComponents}, TimeScore={timeScore:F2}, AccuracyScore={accuracyScore:F2}, FinalScore={finalScore:F2}, Stars={stars}, maxTime: {maxAllowedTime}");

        return new RewardResult(TotalComponents, correctComponents, actualTime, timeScore, accuracyScore, finalScore, stars);
    }

    private float CalculateTimeScore(float actualTime)
    {
        if (maxAllowedTime <= 0f) return 0;
        float score = 1f - (actualTime / maxAllowedTime);
        return Mathf.Clamp01(score);
    }

    private float CalculateAccuracyScore(int correctComponents)
    {
        if (TotalComponents == 0) return 0;
        float score = (float)correctComponents / TotalComponents;
        return Mathf.Clamp01(score);
    }

    private int CalculateStars(float score)
    {
        if (score >= 0.9f) return 5;
        if (score >= 0.75f) return 4;
        if (score >= 0.6f) return 3;
        if (score >= 0.4f) return 2;
        if (score >= 0.2f) return 1;
        return 0;
    }
}

// Simple data container
public struct RewardResult
{
    public int totalComponents;
    public int correctComponents;
    public float time;
    public float timeScore;
    public float accuracyScore;
    public float finalScore;
    public int stars;

    public RewardResult(int totalComp, int correctComp, float time, float timeScore, float accuracy, float final, int stars)
    {
        this.totalComponents = totalComp;
        this.correctComponents = correctComp;
        this.time = time;
        this.timeScore = timeScore;
        this.accuracyScore = accuracy;
        this.finalScore = final;
        this.stars = stars;
    }
}
