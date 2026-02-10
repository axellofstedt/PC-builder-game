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

        if (selectedObject != null && hit.collider.CompareTag("PlacementSurface"))
        {
            PlaceSelectedObject(hit.point);
        }
    }

    public void SelectObject(Selectable obj)
    {
        selectedObject = obj;
        Debug.Log("Selected"+obj.name);
    }

    void PlaceSelectedObject(Vector3 position)
    {
        selectedObject.transform.position = position;
        selectedObject = null;
    }
}
