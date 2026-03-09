using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;

    [Header("Workbench")]
    [SerializeField] public PlacementZone workbenchZone;
    public PlacementZone shelfZone1;
    public PlacementZone shelfZone2;
    public WorkbenchMode workbenchMode;
    // [SerializeField] private PlayerSoundEffects playerSoundEffects;

    public Selectable selectedObject;
    public bool cpuPlaced = false;

    public OrderImage orderImageScript;

    // [HideInInspector] public List<PCPart> currentBuild = new List<PCPart>();
    [HideInInspector] public List<Selectable> currentSelectableBuild = new List<Selectable>();
    [HideInInspector] public Selectable currentChassi;
    
    private OpenCloseDoor chassiDoor;
    private OpenCloseDoor motherboardDoor;

    private int numeberOfPlacedParts;
    void Awake()
    {
        Instance = this;
    }

    public int GetNumberPlaced()
    {
        return numeberOfPlacedParts;
    }

    public void SelectObject(Selectable obj)
    {
        if (!obj.CanInteract)
            return;

        if (obj.currentZone.zoneType == ZoneType.Shelf)
        {
            MoveToWorkbench(obj);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pickupClip);
            // playerSoundEffects.PlayPickUpSound();
            return;
        }

        if (obj.currentZone.zoneType == ZoneType.Workbench &&
            ModeManager.Instance.currentMode == GameMode.Workbench)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pickupClip);
            // playerSoundEffects.PlayPickUpSound();
            selectedObject = obj;
        }
        
        if (obj.currentZone.zoneType == ZoneType.Workbench &&
            ModeManager.Instance.currentMode == GameMode.Player)
        {
            if (currentSelectableBuild.Count <= 1 || obj.PartType != PartType.Chassi)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.pickupClip);
                ReturnPartToShelf(obj);
            } 
            else
            {
                Debug.Log("Can't return chassi to shelf, u have to finish the build");
                foreach (Selectable s in currentSelectableBuild)
                    Debug.Log(s.PartName);
            }
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

        if(obj.PartType == PartType.Motherboard)
        {
            motherboardDoor = obj.GetComponentInChildren<OpenCloseDoor>();
            Debug.Log(motherboardDoor);
            motherboardDoor.openDoor();
        }
        else if (obj.PartType == PartType.Chassi)
        {
            // Open the chassi door when placing it on the workbench
            chassiDoor = obj.GetComponentInChildren<OpenCloseDoor>();
            chassiDoor.openDoor();
            // Add the chassi to the current build list
            currentSelectableBuild.Add(obj);
            currentChassi = obj;
            // Enable the done button when the chassi is placed
            workbenchMode.SetDoneButton(true);
        }

        // Strike component on order
        orderImageScript.StrikeComponent(obj);
    }

    public void PlaceOnSurface(Selectable obj, Vector3 surfacePosition)
    {
        Renderer r = obj.GetComponentInChildren<Renderer>();
        if (r == null) return;

        float bottomOffset = r.bounds.min.y - obj.transform.position.y;

        Vector3 newPos = surfacePosition;
        newPos.y -= bottomOffset;

        obj.transform.position = newPos;
    }


    // In Worbech Mode

    public bool TryPlaceOnTarget(RaycastHit hit)
    {
        switch (selectedObject.PartType)
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
        motherboardDoor?.GetComponent<OpenCloseDoor>().closeDoor();
        return true;
    }

    bool TryPlace(RaycastHit hit, string tag)
    {
        if (!hit.collider.CompareTag(tag)) return false;

        PlaceAtSnap(hit);

        if (BeginnerModeManager.instance != null)
        {
            BoxCollider slot = hit.collider as BoxCollider;
            BeginnerModeManager.instance.MarkSlotAsUsed(slot);
        }

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

        // Make a list of items in current pc build
        // currentBuild.Add(selectedObject.GetComponent<PCPart>());
        Debug.Log($"Adding {selectedObject.PartName} to currentSelectableBuild");
        currentSelectableBuild.Add(selectedObject);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.placeClip);
        // playerSoundEffects.PlayPlaceSound();
        selectedObject = null;
        numeberOfPlacedParts++;
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

    public void ResetBuild()
    {
        // currentBuild.Clear();
        currentSelectableBuild.Clear();
        currentChassi = null;
        cpuPlaced = false;
        orderImageScript.numberOfStrikedRams = 0;
    }

    public void DonePressed()
    {
        Debug.Log("Done Pressed");
        chassiDoor?.closeDoor();
        motherboardDoor?.closeDoor();
    }

    public void ReturnPartToShelf(Selectable part)
    {
        // Reset from workbench slot occupied
        workbenchZone.FreeSlot(part.currentSnapPoint);

        // if chassi close door and remove from current build list
        if (part.PartType == PartType.Chassi)
        {
            chassiDoor?.closeDoor();
            currentChassi = null;
        }
        else if (part.PartType == PartType.Motherboard)
        {
            motherboardDoor?.closeDoor();

        }

        // Flytta tillbaka till startpositionen
        part.transform.SetParent(null); // lossna från PC/Checkout
        part.transform.position = part.originalPos;
        part.transform.rotation = part.originalRot;

        part.currentZone = part.originalZone;

        part.currentSnapPoint = null; // eller behåll om du vill

        part.UnlockInteraction();
        part.GetComponent<PCPartHover>().hoverable = true;
        part.RemoveHighlight();
    }

    public void ResetRamSlots()
    {
        shelfZone1.ResetRamSlots();
        shelfZone2.ResetRamSlots();
    }
}