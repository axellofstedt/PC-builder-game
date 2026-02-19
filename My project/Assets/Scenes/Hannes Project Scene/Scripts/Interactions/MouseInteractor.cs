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

        if (Physics.Raycast(ray, out RaycastHit hit, maxHoverDistance))
        {
            IHoverable hoverable = hit.collider.GetComponentInParent<IHoverable>();

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
            // Om inget träffas inom maxDistance
            currentHover?.OnHoverExit();
            currentHover = null;
        }
    }
}
