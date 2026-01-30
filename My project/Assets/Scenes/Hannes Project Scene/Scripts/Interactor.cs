using TMPro;
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
    public Transform interactorSource;
    public float interactorRange = 3f;
    public GameObject promptPrefab;
    
    private IInteractable currentInteractable;
    private GameObject currentPromptInstance;
    private TextMeshProUGUI promptText;
    private Camera playerCamera;

    void Awake()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        // om vi interagerar med workbench → return
        if (currentInteractable is Workbench wb && wb.IsInteracting)
        {
            ClearPrompt();
            return;
        }
            

        IInteractable hitInteractable = null;

        Ray ray = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactorRange))
        {
            hitInteractable = hit.collider.GetComponentInParent<IInteractable>();
        }
        if (!hitInteractable.Interactable) return;

        if (hitInteractable != currentInteractable)
        {
            ClearPrompt();
            currentInteractable = hitInteractable;

            if (currentInteractable != null)
                ShowPrompt(currentInteractable);
        }

        if (currentInteractable != null &&
            Input.GetKeyDown(currentInteractable.InteractKey))
        {
            currentInteractable.Interact();
        }
    }


    void ShowPrompt(IInteractable interactable)
    {
        currentPromptInstance = Instantiate(
            promptPrefab,
            interactable.PromptAnchor.position,
            Quaternion.identity
        );

        promptText = currentPromptInstance
            .GetComponentInChildren<TextMeshProUGUI>();

        promptText.text = interactable.PromptText;
    }

    void ClearPrompt()
    {
        if (currentPromptInstance != null)
            Destroy(currentPromptInstance);
    }

    void LateUpdate()
    {
        if (currentPromptInstance != null && playerCamera != null)
        {
            currentPromptInstance.transform.forward = playerCamera.transform.forward;
        }
    }

}
