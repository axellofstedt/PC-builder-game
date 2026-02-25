using UnityEngine;

public class WorkbenchMode : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform promptAnchor;
    public Transform PromptAnchor => promptAnchor;

    public KeyCode InteractKey => KeyCode.E;
    public string PromptText => "E - Workbench Mode";
    public bool Interactable { get; set; } = true;

    public WorkbenchUI workbenchUI;
    public Camera workBenchCamera;

    public void Interact()
    {
        ModeManager.Instance.SetMode(GameMode.Workbench);

        // Lås upp alla workbench-objekt
        SelectionManager.Instance.UnlockWorkbenchObjects();

        // Visa Order Image
        workbenchUI.ShowOrderImage();
    }

    enum BuildState // Kanske?
    {
        Building,
        Completed
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { HandleClick(); }
        if (Input.GetKeyDown(KeyCode.Escape)) { Escape(); }
    }

    void HandleClick()
    {
        if (SelectionManager.Instance.selectedObject == null) return;

        if (!TryRaycast(out RaycastHit hit)) return;

        if (SelectionManager.Instance.TryPlaceOnTarget(hit))
            return;
    }

    bool TryRaycast(out RaycastHit hit)
    {
        Ray ray = workBenchCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }

    public void Escape()
    {
        ModeManager.Instance.SetMode(GameMode.Player);
    }
}