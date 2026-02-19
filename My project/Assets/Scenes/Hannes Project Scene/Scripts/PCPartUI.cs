using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PCPartUI : MonoBehaviour
{
    public TMP_Text promptText;

    public void SetPrompt(string message)
    {
        promptText.text = message;
    }

    public void ClearPrompt()
    {
        promptText.text = "";
    }
}
