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

    [Header("Shelf settings")]
    public List<RamSlot> ramSlots;

    public Transform GetSlotForPart(PartType partType)
    {
        if (slots == null)
        {
            Debug.LogError("slots-listan är null i PlacementZone!");
            return null;
        }

        foreach (var slot in slots)
        {
            if (slot == null)
            {
                Debug.LogError("Ett slot i slots-listan är null!");
                continue;
            }

            if (slot.partType == partType && !slot.occupied)
            {
                slot.occupied = true;
                Debug.Log($"Hittade ledig slot för {partType} i {zoneType}!");
                return slot.snapPoint;
            }
        }
        Debug.LogWarning($"Ingen ledig slot för {partType} i {zoneType}!");
        return null;
    }

    public void FreeSlot(Transform snapPoint)
    {
        foreach (var slot in slots)
        {
            Debug.Log($"delen: {slot.partType} passar? {slot.snapPoint == snapPoint}");
            if (slot.snapPoint == snapPoint)
            {
                slot.occupied = false;
                return;
            }
        }
    }

    public int NumberOfFreeSlots()
    {
        int counter = 9;
        foreach (var slot in slots)
        {
            if(slot.occupied == true)
            {
                counter--;
            }
        }
        return counter;
    }

    public void ResetSlots()
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].occupied = false;
    }

    public void ResetRamSlots()
    {
        for (int i = 0; i < ramSlots.Count; i++)
            ramSlots[i].occupied = false;
    }
}

[System.Serializable]
public class ZoneSlot
{
    public PartType partType;
    public Transform snapPoint;
    public bool occupied;
}