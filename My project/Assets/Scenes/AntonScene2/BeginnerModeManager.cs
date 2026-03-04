using UnityEngine;
using System.Collections.Generic;

public class BeginnerModeManager : MonoBehaviour
{
    public static BeginnerModeManager instance;
    public bool isActive = false;
    private BoxCollider[] allSlots;
    private HashSet<BoxCollider> usedSlots = new HashSet<BoxCollider>();

    private void Update()
    {
        if(ModeManager.Instance.currentMode != GameMode.Workbench)
        {
            DisableBeginnerMode();
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        allSlots = FindObjectsOfType<BoxCollider>();
        foreach (var box in allSlots)
        {
            Debug.Log(box.name);
            if (!box.CompareTag("Untagged"))
            {
                Outline outline = box.GetComponentInChildren<Outline>();
                if (outline != null)
                {
                    outline.enabled = isActive;
                }
            }
        }
    }

    public void ToggleBeginnerMode()
    {
        isActive = !isActive;
        Debug.Log("Mode is " + isActive);

        //BoxCollider[] allSlots = FindObjectsOfType<BoxCollider>();

        foreach (var box in allSlots)
        {
            if (box.CompareTag("Untagged")) continue;
            //Debug.Log(box + "box");
            if (usedSlots.Contains(box)) continue;

            // Hämta Outline på barnet som har MeshRenderer
            Outline outline = box.GetComponentInChildren<Outline>();
            if (outline != null)
                outline.enabled = isActive;
        }
    }

    public void DisableBeginnerMode()
    {
        foreach (var box in allSlots)
        {
            if (box.CompareTag("Untagged")) continue;
            if (usedSlots.Contains(box)) continue;

            Outline outline = box.GetComponentInChildren<Outline>();
            if (outline != null)
                outline.enabled = false;
        }
    }

    public void MarkSlotAsUsed(BoxCollider box)
    {
        if (box == null) return;
        Outline outline = box.GetComponentInChildren<Outline>();
        if (outline != null)
            outline.enabled = false;

        usedSlots.Add(box);
    }
}
