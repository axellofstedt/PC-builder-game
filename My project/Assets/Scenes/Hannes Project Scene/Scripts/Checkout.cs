using UnityEngine;

public class Checkout : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform promptAnchor;
    public Transform PromptAnchor => promptAnchor;
    public KeyCode InteractKey => KeyCode.E;
    public string PromptText => "E - Checkout Mode";
    public bool Interactable { get; set; } = true;

    private Camera checkoutCamera;

    private void Awake()
    {
        checkoutCamera = GetComponentInChildren<Camera>();
    }

    public void Interact()
    {
        ModeManager.Instance.SetMode(GameMode.Checkout);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ModeManager.Instance.SetMode(GameMode.Player);
        }
    }

}


