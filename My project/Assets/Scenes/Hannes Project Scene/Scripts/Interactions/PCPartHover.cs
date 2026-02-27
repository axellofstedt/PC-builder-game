using UnityEngine;

public class PCPartHover : MonoBehaviour, IHoverable
{
    public PCPart partData;
    public bool hoverable = true;

    private Outline outline;
    private PCPartUI partUI;
    private Selectable selectable;

    public string HoverText =>
        partData != null
            ? $"{partData.partType}: {partData.partName}"
            : name;

    void Awake()
    {
        partUI = Object.FindFirstObjectByType<PCPartUI>();
        selectable = GetComponentInParent<Selectable>();

        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.red;
        outline.OutlineWidth = 5f;
        outline.enabled = false;
    }

    public void OnHoverEnter()
    {
        if (selectable != null && !selectable.CanInteract)
            return;

        outline.enabled = true;
        partUI?.SetPrompt(HoverText);
    }

    public void OnHoverExit()
    {
        if (SelectionManager.Instance.selectedObject != selectable)
        {
            outline.enabled = false;
            Debug.Log($"Selected: {SelectionManager.Instance.selectedObject}, this selectable: {selectable}");
        }

        partUI?.ClearPrompt();
        Debug.Log($"Hover exit: {HoverText}");
    }

    public void OnClick()
    {
        if (selectable == null || !selectable.CanInteract)
            return;

        if (SelectionManager.Instance.selectedObject != selectable)
        {
            SelectionManager.Instance.Deselect();
        }

        SelectionManager.Instance.SelectObject(selectable);
        if (ModeManager.Instance.currentMode == GameMode.Workbench)
        {
            selectable.Highlight();
        }
    }
}