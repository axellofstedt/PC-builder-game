using UnityEngine;
using System.Collections.Generic;

public enum ZoneType
{
    Shelf,
    Workbench,
}

public class PlacementZone : MonoBehaviour
{
    public ZoneType zoneType;

    [Header("Workbench settings")]
    public List<ZoneSlot> slots;

    public Transform GetSlotForPart(PartType partType)
    {
        foreach (var slot in slots)
        {
            if (slot.partType == partType && !slot.occupied)
            {
                slot.occupied = true;
                return slot.snapPoint;
            }
        }
        return null;
    }

    public void FreeSlot(Transform snapPoint)
    {
        foreach (var slot in slots)
        {
            if (slot.snapPoint == snapPoint)
            {
                slot.occupied = false;
                return;
            }
        }
    }
}

[System.Serializable]
public class ZoneSlot
{
    public PartType partType;
    public Transform snapPoint;
    public bool occupied;
}