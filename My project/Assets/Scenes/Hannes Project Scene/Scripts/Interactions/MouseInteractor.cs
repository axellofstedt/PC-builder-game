using UnityEngine;

public interface IHoverable
{
    void OnHoverEnter();
    void OnHoverExit();
    void OnClick();

    string HoverText { get; }
}

public class MouseInteractor : MonoBehaviour
{
    private IHoverable currentHover;
    public float maxHoverDistance = 3f;

    void Update()
    {
        Camera cam = CameraManager.Instance?.activeCamera;
        if (cam == null)
            return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxHoverDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            IHoverable hoverable = hit.collider.GetComponentInParent<IHoverable>();
            PCPartHover partHover = hit.collider.GetComponentInParent<PCPartHover>();

            if (partHover != null && !partHover.hoverable)
            {
                if (currentHover != null)
                    currentHover.OnHoverExit();
                currentHover = null;
                return;
            }

            if (partHover != null && partHover.partData.partType == PartType.Chassi && ModeManager.Instance.currentMode == GameMode.Workbench) return;

            if (hoverable != currentHover)
            {
                currentHover?.OnHoverExit();
                currentHover = hoverable;
                currentHover?.OnHoverEnter();
            }

            if (currentHover != null && Input.GetMouseButtonDown(0))
            {
                currentHover.OnClick();
            }
        }
        else
        {
            if (currentHover != null)
                currentHover.OnHoverExit();
            currentHover = null;
        }
    }
}
