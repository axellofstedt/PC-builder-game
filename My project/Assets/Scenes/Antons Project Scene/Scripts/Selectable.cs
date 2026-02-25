using UnityEngine;

public class Selectable : MonoBehaviour
{
    public BoxCollider shelfClickCollider;
    public BoxCollider preciseCollider;

    public PCPartHover hover;
    public PlacementZone currentZone;
    public Transform currentSnapPoint;

    private void Awake()
    {
        ActivateShelfMode();

        if (hover == null)
        {
            hover = GetComponent<PCPartHover>();
        }
    }

    public void ActivateShelfMode()
    {
        shelfClickCollider.enabled = true;
        preciseCollider.enabled = false;
    }

    public void ActivateWorkbenchMode()
    {
        Debug.Log("WORKSKSKSKDOWKDOKS");
        shelfClickCollider.enabled = false;
        preciseCollider.enabled = true;
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
