using UnityEngine;
using UnityEngine.UI;

public class PCPartHover : MonoBehaviour, IHoverable
{
    public PCPart partData;
    public Outline outline;
    
    private PCPartUI partUI;

    public string HoverText => partData.partType + ": " + partData.partName;

    private void Awake()
    {
        partUI = Object.FindFirstObjectByType<PCPartUI>();

        if (partUI == null)
            Debug.LogWarning("PCPartUI saknas i scenen!");

        if (outline != null)
            outline.enabled = false;
    }

    public void OnHoverEnter()
    {
        partUI?.SetPrompt(HoverText);

        if (outline != null)
            outline.enabled = true;
    }

    public void OnHoverExit()
    {
        partUI?.ClearPrompt();

        if (outline != null)
            outline.enabled = false;
    }

    public void OnClick()
    {
        Debug.Log("Clicked " + partData.partName + " of type " + partData.partType);
        // Place part on workbench
    }
}
