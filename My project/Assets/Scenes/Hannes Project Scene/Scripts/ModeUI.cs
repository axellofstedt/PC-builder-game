using UnityEngine;

public class ModeUI : MonoBehaviour
{
    [SerializeField] private GameMode visibleInMode;

    public void UpdateVisibility(GameMode currentMode)
    {
        gameObject.SetActive(currentMode == visibleInMode);
    }
}
