using UnityEngine;

public class Selectable : MonoBehaviour
{
    public void Select()
    {
        SelectionManager.Instance.SelectObject(this);
    }
}
