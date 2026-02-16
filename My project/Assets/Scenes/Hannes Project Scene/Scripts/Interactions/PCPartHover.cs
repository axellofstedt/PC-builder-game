using UnityEngine;
using UnityEngine.UI;

public class PCPartHover : MonoBehaviour, IHoverable
{
    public PCPart partData;

    private Outline outline;
    private PCPartUI partUI;

    public string HoverText => partData.partType + ": " + partData.partName;

    private void Awake()
    {
        partUI = Object.FindFirstObjectByType<PCPartUI>();

        if (partUI == null)
            Debug.LogWarning("PCPartUI saknas i scenen!");

        // Lägg till Outline-komponenten för att hantera hover-effekten
        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.red;
        outline.OutlineWidth = 5f;

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
