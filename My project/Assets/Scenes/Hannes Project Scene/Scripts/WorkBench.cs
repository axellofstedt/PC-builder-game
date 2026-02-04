using UnityEngine;

public class Workbench : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform promptAnchor;
    public Transform PromptAnchor => promptAnchor;
    public KeyCode InteractKey => KeyCode.E;
    public string PromptText => "E - Workbench Mode";
    public bool Interactable { get; set; } = true;

    private Camera workbenchCamera;

    private void Awake()
    {
        workbenchCamera = GetComponentInChildren<Camera>();
    }

    public void Interact()
    {
        ModeManager.Instance.SetMode(GameMode.Workbench);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ModeManager.Instance.SetMode(GameMode.Player);
        }
    }


}


