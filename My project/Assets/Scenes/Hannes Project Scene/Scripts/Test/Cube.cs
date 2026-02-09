using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform promptAnchor;
    [SerializeField] private bool interactable = true;

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
        Debug.Log("Door opened");
    }
}
