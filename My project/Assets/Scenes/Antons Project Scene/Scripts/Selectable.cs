using UnityEngine;

public class Selectable : MonoBehaviour
{
    public PCPartHover hover;
    public PlacementZone currentZone;
    public Transform currentSnapPoint;

    private void Awake()
    {
        if (hover == null)
        {
            hover = GetComponent<PCPartHover>();
        }
    }

    public string GetPartName()
    {
        if (hover != null && hover.partData != null)
            return hover.partData.partName;
      
        return gameObject.name;
    }

    public PartType GetPartType()
    {
        if (hover != null && hover.partData != null)
            return hover.partData.partType;

        return default(PartType);
    }

    public void OnSelected()
    {
        if (currentZone != null && currentSnapPoint != null)
        {
            currentZone.FreeSlot(currentSnapPoint);
            currentSnapPoint = null;
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        //PlacementZone zone = other.GetComponent<PlacementZone>();
        if (zone != null && currentSnapPoint != null)
        {
            currentZone.FreeSlot(currentSnapPoint);
            currentSnapPoint = null;
        }
    }*/
}
