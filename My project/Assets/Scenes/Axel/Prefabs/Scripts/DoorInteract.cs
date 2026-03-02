using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    public Transform promptAnchor;
    public Transform PromptAnchor => promptAnchor;
    public KeyCode InteractKey => KeyCode.E;
    public string PromptText => "E - Open/Close Door";
    public bool Interactable { get; set; } = true;

    [SerializeField] OpenCloseDoor doorScript;
    public void Interact()
    {
        if (doorScript != null)
        {
            Debug.Log("INTERACT");
            doorScript.interactDoor();
        }
    }
}
