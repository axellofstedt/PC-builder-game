using UnityEngine;
using UnityEngine.Rendering;

public class RewardSystem : MonoBehaviour
{
    [Header("Time settings")]
    public float maxAllowedTime = 300f; // seconds

    [Header("Order settings")]
    public int totalComponents = 9;

    // Main evaluation method
    public RewardResult Evaluate(float actualTime, int correctComponents)
    {
        float timeScore = CalculateTimeScore(actualTime);
        float accuracyScore = CalculateAccuracyScore(correctComponents);

        float finalScore = (timeScore + accuracyScore) / 2f;
        int stars = CalculateStars(finalScore);

        return new RewardResult(totalComponents, correctComponents, actualTime, timeScore, accuracyScore, finalScore, stars);
    }

    private float CalculateTimeScore(float actualTime)
    {
        if (maxAllowedTime <= 0f) return 0;
        float score = 1f - (actualTime / maxAllowedTime);
        return Mathf.Clamp01(score);
    }

    private float CalculateAccuracyScore(int correctComponents)
    {
        if (totalComponents == 0) return 0;
        float score = (float)correctComponents / totalComponents;
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
