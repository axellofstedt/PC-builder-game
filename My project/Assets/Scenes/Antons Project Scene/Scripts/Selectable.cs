using UnityEngine;
using UnityEngine.UI;

public class Selectable : MonoBehaviour
{
    public PCPart partData;

    public PlacementZone currentZone;
    public Transform currentSnapPoint;

    // Styr om objektet får interageras med
    public bool CanInteract { get; private set; } = true;

    public PartType PartType => partData.partType;
    public string PartName => partData.partName;

    private Outline outline;
    void Start()
    {
        outline = gameObject.GetComponent<Outline>();
    }

    public void Highlight()
    {
        outline.OutlineColor = Color.green;
    }

    public void RemoveHighlight()
    {
        outline.enabled = false;
        outline.OutlineColor = Color.red;
    }

    public void LockInteraction()
    {
        CanInteract = false;
    }

    public void UnlockInteraction()
    {
        CanInteract = true;
    }
}