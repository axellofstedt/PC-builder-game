using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
   public static SelectionManager Instance;
   private Selectable selectedObject;
   public TextMeshProUGUI heldItemText;
   public Transform workbenchTransform;
   public PlacementZone workbenchZone;
   
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

        Selectable selectable = hit.collider.GetComponent<Selectable>();
        if (selectable != null)
        {
            SelectObject(selectable);
            return;
        }
        if (selectedObject != null)
        {
            if (hit.collider.CompareTag("CPU") && selectedObject.GetPartType().ToString() == "CPU")
            {
                Debug.Log("CPU placed on motherboard" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.collider.transform.position);
            }
            else if (hit.collider.CompareTag("CPU_Cooling") && selectedObject.GetPartType().ToString() == "CPU_Cooling")
            {
                Debug.Log("CPU cooler placed on CPU" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.collider.transform.position);
            }
            else if (hit.collider.CompareTag("Drive") && selectedObject.GetPartType().ToString() == "Drive")
            {
                Debug.Log("Drive placed on motherboard" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.collider.transform.position);
            }
            else if (hit.collider.CompareTag("Fan") && selectedObject.GetPartType().ToString() == "Fan")
            {
                Debug.Log("Fan placed in chassi" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.collider.transform.position);
            }
            else if (hit.collider.CompareTag("GPU") && selectedObject.GetPartType().ToString() == "GPU")
            {
                Debug.Log("Graphics card placed on motherboard" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.collider.transform.position);
            }
            else if (hit.collider.CompareTag("Motherboard") && selectedObject.GetPartType().ToString() == "Motherboard")
            {
                Debug.Log("Motherbard placed in chassi" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.collider.transform.position);
            }
            else if (hit.collider.CompareTag("PSU") && selectedObject.GetPartType().ToString() == "PSU")
            {
                Debug.Log("Power supply placed in chassi" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.collider.transform.position);
            }
            else if (hit.collider.CompareTag("RAM") && selectedObject.GetPartType().ToString() == "RAM")
            {
                Debug.Log("RAM placed on motherboard" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.collider.transform.position);
            }
        }
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
            obj.transform.position = slot.position;
            obj.transform.rotation = slot.rotation;
            obj.currentZone = workbenchZone;
            obj.currentSnapPoint = slot;
        }
        else
        {
            Debug.LogWarning("Ingen ledig plats på workbench för " + obj.GetPartType());
        }
    }

    void PlaceSelectedObject(Vector3 position)
    {
        selectedObject.transform.position = position;
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
        //string instruction = GetInstructionForPart(selectedObject.GetPartType());

        heldItemText.text =
            $"Du håller i: {partName}\n" +
            $"Place correctly in chassi\n" +
            $"Press [p] to deselect";
    }
}
