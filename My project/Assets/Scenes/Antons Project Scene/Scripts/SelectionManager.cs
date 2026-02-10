using UnityEngine;

public class SelectionManager : MonoBehaviour
{
   public static SelectionManager Instance;
   private Selectable selectedObject;

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
            else if (hit.collider.CompareTag("CPU_Cooling") && selectedObject.getPartType().ToString() == "CPU_Cooling")
            {
                Debug.Log("CPU cooler placed on CPU" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.point);
            }
            else if (hit.collider.CompareTag("Drive") && selectedObject.getPartType().ToString() == "Drive")
            {
                Debug.Log("Drive placed on motherboard" + selectedObject.GetPartName());
                PlaceSelectedObject(hit.point);
            }
        }
        if (selectedObject != null && (hit.collider.CompareTag("Motherboard") && selectedObject.GetPartType().ToString() =="PSU"))
        {
            Debug.Log("Placed on motherboard" + selectedObject.GetPartName());
            PlaceSelectedObject(hit.point);
        }
        else if (selectedObject != null && hit.collider.CompareTag("PlacementSurface"))
        {
            //PlaceSelectedObject(hit.point);
            Debug.Log("Cantplacehere");
        }
    }

    public void SelectObject(Selectable obj)
    {
        selectedObject = obj;
        Debug.Log("Selected"+obj.GetPartName());
    }

    void PlaceSelectedObject(Vector3 position)
    {
        selectedObject.transform.position = position;
        selectedObject = null;
    }
}
