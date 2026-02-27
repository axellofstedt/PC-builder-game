using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;

    [Header("Workbench")]
    [SerializeField] private PlacementZone workbenchZone;

    public Selectable selectedObject;
    public bool cpuPlaced = false;


    void Awake()
    {
        Instance = this;
    }

    public void SelectObject(Selectable obj)
    {
        if (!obj.CanInteract)
            return;

        if (obj.currentZone.zoneType == ZoneType.Shelf)
        {
            MoveToWorkbench(obj);
            return;
        }

        if (obj.currentZone.zoneType == ZoneType.Workbench &&
            ModeManager.Instance.currentMode == GameMode.Workbench)
        {
            selectedObject = obj;
        }
    }

    void MoveToWorkbench(Selectable obj)
    {

        Transform slot = workbenchZone.GetSlotForPart(obj.PartType);
        if (slot == null)
        {
            Debug.LogWarning($"Ingen plats för {obj.PartType}");
            return;
        }

        PlaceOnSurface(obj, slot.position);
        obj.transform.rotation = slot.rotation;

        obj.currentZone = workbenchZone;
        obj.currentSnapPoint = slot;

        // Lås tills workbench mode
        obj.LockInteraction();
    }

    void PlaceOnSurface(Selectable obj, Vector3 surfacePosition)
    {
        Renderer r = obj.GetComponentInChildren<Renderer>();
        if (r == null) return;

        float bottomOffset = r.bounds.min.y - obj.transform.position.y;

        Vector3 newPos = surfacePosition;
        newPos.y -= bottomOffset;

        obj.transform.position = newPos;
    }

    public void UnlockWorkbenchObjects()
    {
        foreach (Selectable s in FindObjectsByType<Selectable>(FindObjectsSortMode.None))
        {
            if (s.currentZone.zoneType == ZoneType.Workbench)
            {
                s.UnlockInteraction();
            }
        }
    }


    // In Worbech Mode



    public bool TryPlaceOnTarget(RaycastHit hit)
    {
        switch (SelectionManager.Instance.selectedObject.PartType)
        {
            case PartType.CPU:
                return TryPlace(hit, "CPU", ref cpuPlaced);

            case PartType.CPUCooling:
                if (!cpuPlaced) return false;
                return TryPlace(hit, "CPU");

            case PartType.RAM:
                return TryPlaceRam(hit);

            case PartType.GPU:
                return TryPlace(hit, "GPU");

            case PartType.Drive:
                return TryPlace(hit, "Drive");

            case PartType.Fan:
                return TryPlace(hit, "Fan");

            case PartType.Motherboard:
                return TryPlace(hit, "Motherboard");

            case PartType.PSU:
                return TryPlace(hit, "PSU");
        }

        return false;
    }

    bool TryPlace(RaycastHit hit, string tag, ref bool flag)
    {
        if (!hit.collider.CompareTag(tag)) return false;

        flag = true;
        PlaceAtSnap(hit);
        return true;
    }

    bool TryPlace(RaycastHit hit, string tag)
    {
        if (!hit.collider.CompareTag(tag)) return false;

        PlaceAtSnap(hit);
        return true;
    }

    void PlaceAtSnap(RaycastHit hit)
    {
        Transform snap = hit.collider.transform.Find("Snap_Point");
        if (snap == null) return;

        Debug.Log($"{selectedObject.PartType} placed: {selectedObject.PartName} on {hit.collider.name} at: {snap}");
        PlaceSelectedObject(snap);
    }

    void PlaceSelectedObject(Transform snapPoint)
    {
        selectedObject.transform.position = snapPoint.position;
        selectedObject.transform.rotation = snapPoint.rotation;
        
        selectedObject.RemoveHighlight();
        selectedObject.GetComponent<PCPartHover>().hoverable = false;

        selectedObject = null;
    }

    bool TryPlaceRam(RaycastHit hit)
    {
        RamSlot slot = hit.collider.GetComponent<RamSlot>();
        if (slot == null) return false;

        if (slot.occupied)
        {
            Debug.Log("Ram slot taken");
            return true;
        }
        Debug.Log("Placing RAM in slot");
        Debug.Log(slot);
        Debug.Log($"Snap point: {slot.snapPoint.position}");
        PlaceSelectedObject(slot.snapPoint);
        slot.occupied = true;

        Debug.Log("RAM placed");
        return true;
    }

    public void Deselect()
    {
        if (selectedObject != null) selectedObject.RemoveHighlight();
        selectedObject = null;
    }
}