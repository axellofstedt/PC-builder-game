using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
   public static SelectionManager Instance;
   private Selectable selectedObject;
   public TextMeshProUGUI heldItemText;

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
                PlaceSelectedObject(hit.point);
            }
            else if (hit.collider.CompareTag("CPU_Cooling") && selectedObject.GetPartType().ToString() == "CPU_Cooling")
            {
                Debug.Log("CPU cooler placed on CPU" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.point);
            }
            else if (hit.collider.CompareTag("Drive") && selectedObject.GetPartType().ToString() == "Drive")
            {
                Debug.Log("Drive placed on motherboard" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.point);
            }
            else if (hit.collider.CompareTag("Fan") && selectedObject.GetPartType().ToString() == "Fan")
            {
                Debug.Log("Fan placed in chassi" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.point);
            }
            else if (hit.collider.CompareTag("GPU") && selectedObject.GetPartType().ToString() == "GPU")
            {
                Debug.Log("Graphics card placed on motherboard" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.point);
            }
            else if (hit.collider.CompareTag("Motherboard") && selectedObject.GetPartType().ToString() == "Motherboard")
            {
                Debug.Log("Motherbard placed in chassi" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.point);
            }
            else if (hit.collider.CompareTag("PSU") && selectedObject.GetPartType().ToString() == "PSU")
            {
                Debug.Log("Power supply placed in chassi" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.point);
            }
            else if (hit.collider.CompareTag("RAM") && selectedObject.GetPartType().ToString() == "RAM")
            {
                Debug.Log("RAM placed on motherboard" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.point);
            }
        }
    }

    public void SelectObject(Selectable obj)
    {
        selectedObject = obj;
        Debug.Log("Selected"+obj.GetPartName());
        UpdateHeldItemUI();
    }

    void PlaceSelectedObject(Vector3 position)
    {
        selectedObject.transform.position = position;
        selectedObject = null;
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
            $"Place correctly in chassi";
    }
}
