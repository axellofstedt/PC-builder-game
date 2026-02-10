using UnityEngine;

public class Selectable : MonoBehaviour
{
    public PCPartHover hover;

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

        return gameObject.name; // fallback
    }

    public void Select()
    {
        SelectionManager.Instance.SelectObject(this);
    }
}
