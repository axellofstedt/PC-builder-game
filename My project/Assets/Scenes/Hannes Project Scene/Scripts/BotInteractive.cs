using UnityEngine;

public class BotInteractive : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform promptAnchor;
    [SerializeField] private bool interactable = false;
    public Transform PromptAnchor => promptAnchor;
    public KeyCode InteractKey => KeyCode.E;
    public string PromptText => "E";
    public bool Interactable
    {
        get => interactable;
        set => interactable = value;
    }
    public void Interact()
    {
        Debug.Log("Interacted with Bot: " + gameObject.name);
    }

    void Awake()
    {
        Debug.Log($"{name} PromptAnchor = {promptAnchor}");
    }

}
