using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PanelResizer : MonoBehaviour
{
    public TMP_Text text;
    public float paddingX = 20f; 
    public float paddingY = 10f; 

    private RectTransform panelRect;

    private void Awake()
    {
        panelRect = GetComponent<RectTransform>();
    }

    public void UpdateText(string newText)
    {
        if (text == null) return;

        text.text = newText;

        // Force TMP to recalc size
        text.ForceMeshUpdate();

        // Hämta textens size
        float textWidth = text.preferredWidth;
        float textHeight = text.preferredHeight;

        // Applicera till panelen
        panelRect.sizeDelta = new Vector2(textWidth + paddingX, textHeight + paddingY);
    }
}