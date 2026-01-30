using UnityEngine;

public class Workbench : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform promptAnchor;
    [SerializeField] private Camera workbenchCamera;
    [SerializeField] private bool interactable = true;

    public Transform PromptAnchor => promptAnchor;
    public KeyCode InteractKey => KeyCode.E;
    public string PromptText => "E";

    public bool Interactable
    {
        get => interactable;
        set => interactable = value;
    }

    [SerializeField] private FirstPersonController playerController;
    public bool IsInteracting { get; private set; } = false;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        if (workbenchCamera != null) workbenchCamera.enabled = false;
    }

    void Update()
    {
        if (workbenchCamera != null && workbenchCamera.enabled && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitWorkbench();
        }
    }

    public void Interact()
    {
        if (workbenchCamera == null || playerController == null) return;

        // switch cameras
        mainCamera.enabled = false;
        workbenchCamera.enabled = true;

        // mark interacting
        IsInteracting = true;

        // disable player movement
        playerController.enabled = false;

        Debug.Log("Workbench used - camera switched");
    }

    // Optional: exit back to main camera
    public void ExitWorkbench()
    {
        if (workbenchCamera == null || playerController == null) return;

        // switch back
        workbenchCamera.enabled = false;
        mainCamera.enabled = true;

        IsInteracting = false;

        // enable player movement
        playerController.enabled = true;
    }
}
