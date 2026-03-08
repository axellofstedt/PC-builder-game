using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PCPartUI : MonoBehaviour
{
    public TMP_Text promptText;
    public float defaultOffsetY = -493f;
    public float workbenchOffsetY = 50f;

    private RectTransform rect;

    void Start()
    {
        rect = promptText.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (ModeManager.Instance.currentMode == GameMode.Workbench)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, defaultOffsetY + workbenchOffsetY);
        }
        else
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, defaultOffsetY);
        }
    }

    public void SetPrompt(string message)
    {
        promptText.text = message;
    }

    public void ClearPrompt()
    {
        promptText.text = "";
    }
}
