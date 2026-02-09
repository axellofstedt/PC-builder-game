using UnityEngine;

public interface IInteractable
{
    void Interact();
    Transform PromptAnchor { get; }
    KeyCode InteractKey { get; }
    string PromptText { get; }
    bool Interactable { get; set; }
}

public class Interactor : MonoBehaviour
{
    [Header("Interactor Settings")]
    public Transform interactorSource;
    public float interactorRange = 3f;
    public GameObject promptPrefab;
    // public FPCharAnims fPCharAnims;

    private IInteractable currentInteractable;
    private GameObject currentPromptInstance;
    private PanelResizer currentPanelResizer;
    private Camera playerCamera;
    private IInteractable lastInteractable;

    private void Awake()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        HandleRaycast();
        HandleInteraction();
    }

    private void LateUpdate()
    {
        // Always face camera
        if (currentPromptInstance != null && playerCamera != null)
            currentPromptInstance.transform.forward = playerCamera.transform.forward;
    }

    private void HandleRaycast()
    {
        IInteractable hitInteractable = null;

        Ray ray = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactorRange))
        {
            hitInteractable = hit.collider.GetComponentInParent<IInteractable>();
        }

        if (hitInteractable != currentInteractable)
        {
            ClearPrompt();
            currentInteractable = hitInteractable;

            if (currentInteractable != null)
                ShowPrompt(currentInteractable);
        }
    }

    private void HandleInteraction()
    {
        if (currentInteractable != null &&
            Input.GetKeyDown(currentInteractable.InteractKey))
        {
            currentInteractable.Interact();

            // Interactor animation
            // fPCharAnims.InteractAnimation();

            // Optional: Update prompt text if needed after interaction
            if (currentPanelResizer != null)
                currentPanelResizer.UpdateText(currentInteractable.PromptText);
        }
    }

    private void ShowPrompt(IInteractable interactable)
    {
        ClearPrompt();

        currentPromptInstance = Instantiate(
            promptPrefab,
            interactable.PromptAnchor.position,
            Quaternion.identity
        );

        lastInteractable = currentInteractable;

        // PanelResizer takes care of TextMeshPro text and panel width
        currentPanelResizer = currentPromptInstance.GetComponentInChildren<PanelResizer>();
        if (currentPanelResizer != null)
            currentPanelResizer.UpdateText(interactable.PromptText);
        else
            Debug.LogWarning("Prompt prefab is missing a PanelResizer component!");
        
    }

    public void ClearPrompt()
    {
        if (currentPromptInstance != null)
            Destroy(currentPromptInstance);

        currentPromptInstance = null;
        currentPanelResizer = null;
    }

    public void ShowLastPrompt()
    {
        if (lastInteractable != null) ShowPrompt(lastInteractable);
    }
}
