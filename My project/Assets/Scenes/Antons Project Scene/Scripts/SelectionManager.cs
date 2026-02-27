using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
   public static SelectionManager Instance;
   private Selectable selectedObject;
   public TextMeshProUGUI heldItemText;
   public Transform workbenchTransform;
   public PlacementZone workbenchZone;
   public Transform snapPoint;
   public bool cpuPlaced = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Deselect();
        }
    }

    void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(!Physics.Raycast(ray, out hit))
        {
            return;
        }

        if (selectedObject != null)
        {
            if (hit.collider.CompareTag("CPU") && selectedObject.GetPartType().ToString() == "CPU")
            {
                cpuPlaced = true;
                Debug.Log("CPU placed on motherboard" + selectedObject.GetPartName());
                Transform snapPoint = hit.collider.GetComponentInChildren<Transform>();
                PlaceSelectedObject(snapPoint);
            }
            else if (hit.collider.CompareTag("CPU") && selectedObject.GetPartType().ToString() == "CPUCooling" && cpuPlaced)
            {
                Debug.Log("CPU cooler placed on CPU" + selectedObject.GetPartName());
                Transform snapPoint = hit.collider.transform.Find("Snap_Point");
                PlaceSelectedObject(snapPoint);
            }
            else if (hit.collider.CompareTag("Drive") && selectedObject.GetPartType().ToString() == "Drive")
            {
                Debug.Log("Drive placed on motherboard" + selectedObject.GetPartName());
                Transform snapPoint = hit.collider.transform.Find("Snap_Point");
                PlaceSelectedObject(snapPoint);
            }
            else if (hit.collider.CompareTag("Fan") && selectedObject.GetPartType().ToString() == "Fan")
            {
                Debug.Log("Fan placed in chassi" + selectedObject.GetPartName());
                Transform snapPoint = hit.collider.transform.Find("Snap_Point");
                PlaceSelectedObject(snapPoint);
            }
            else if (hit.collider.CompareTag("GPU") && selectedObject.GetPartType().ToString() == "GPU")
            {
                Debug.Log("Graphics card placed on motherboard" + selectedObject.GetPartName());
                Transform snapPoint = hit.collider.GetComponentInChildren<Transform>();
                PlaceSelectedObject(snapPoint);
            }
            else if (hit.collider.CompareTag("Motherboard") && selectedObject.GetPartType().ToString() == "Motherboard")
            {
                Debug.Log("Motherbard placed in chassi" + selectedObject.GetPartName());

                Transform snapPoint = hit.collider.transform.Find("Snap_Point");
                PlaceSelectedObject(snapPoint);
                
            }
            else if (hit.collider.CompareTag("PSU") && selectedObject.GetPartType().ToString() == "PSU")
            {
                Debug.Log("Power supply placed in chassi" + selectedObject.GetPartName());
                Transform snapPoint = hit.collider.transform.Find("Snap_Point");
                PlaceSelectedObject(snapPoint);
            }
            else if (hit.collider.CompareTag("RAM") && selectedObject.GetPartType().ToString() == "RAM")
            {
                RamSlot slot = hit.collider.GetComponent<RamSlot>();
                if(slot == null)
                {
                    return;
                }
                if (slot.occupied)
                {
                    Debug.Log("Ram slot taken");
                }
                Debug.Log("RAM placed on motherboard" + selectedObject.GetPartName());
                //Transform snapPoint = hit.collider.GetComponentInChildren<Transform>();
                PlaceSelectedObject(slot.snapPoint);
                slot.occupied = true;
                return;
            }
        }


        Selectable selectable = hit.collider.GetComponentInParent<Selectable>();

        if (selectable != null)
        {
            if (selectable.isPlaced)
            {
                return;
            }
            SelectObject(selectable);
            return;
        }
        Debug.Log(hit.collider.gameObject.name);
    }


    public void SelectObject(Selectable obj)
    {
        obj.OnSelected();

        if (obj.currentZone == null)
        {
            Debug.Log("Zone null");
            return;
        }

        switch (obj.currentZone.zoneType)
        {
            case ZoneType.Shelf:
                Debug.Log("Movede to bench" + obj.GetPartName());
                obj.ActivateWorkbenchMode();
                MoveToWorkBench(obj);
                break;
        
            case ZoneType.Workbench:
                selectedObject = obj;
                UpdateHeldItemUI();
                Debug.Log("Selected" + obj.GetPartName());
                break;
        }
    }

    public void MoveToWorkBench(Selectable obj)
    {
        if(workbenchZone == null)
        {
            return;
        }

        Transform slot = workbenchZone.GetSlotForPart(obj.GetPartType());

        if (slot != null)
        {
            PlaceOnSurface(obj, slot.position);
            obj.transform.rotation = slot.rotation;
            obj.currentZone = workbenchZone;
            obj.currentSnapPoint = slot;
            if(obj.GetPartType().ToString() == "Chassi")
            {
                obj.isPlaced = true;
            }
            //obj.ActivateWorkbenchMode();
        }
        else
        {
            Debug.LogWarning("Ingen ledig plats på workbench för " + obj.GetPartType());
        }
    }

    void PlaceOnSurface(Selectable obj, Vector3 surfacePosition)
    {
        Renderer r = obj.GetComponentInChildren<Renderer>();
        if (r == null)
        {
            Debug.LogError("No renderer found on object");
            return;
        }

        float bottomOffset = r.bounds.min.y - obj.transform.position.y;

        Vector3 newPos = surfacePosition;
        newPos.y -= bottomOffset;

        obj.transform.position = newPos;
    }

    void PlaceSelectedObject(Transform snapPoint)
    {
        selectedObject.transform.position = snapPoint.position;
        selectedObject.transform.rotation = snapPoint.rotation;
        selectedObject.ActivateWorkbenchMode();

        //selectedObject.TurnOffColliders();
        selectedObject.isPlaced = true;
        BoxCollider slotCollider = snapPoint.GetComponentInParent<BoxCollider>();
        if (slotCollider != null)
        {
            BeginnerModeManager.instance.MarkSlotAsUsed(slotCollider);
        }
        selectedObject = null;
        UpdateHeldItemUI();
    }

    void Deselect()
    {
        if (selectedObject != null)
        {
            Debug.Log(selectedObject.name + " deselected");
            selectedObject = null;
        }
        UpdateHeldItemUI();
    }

    void UpdateHeldItemUI()
    {
        if (selectedObject == null)
        {
            heldItemText.text = "";
            return;
        }

        string partName = selectedObject.GetPartName();
        string partType = selectedObject.GetPartType().ToString();
        heldItemText.text =
            $"Du håller i: {partType}\n" +
            $"Place correctly in chassi\n" +
            $"Press [p] to deselect";
    }
}
