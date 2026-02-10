using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    public Transform destination;

    void OnMouseDown()
    {
        transform.position = destination.position;
        Debug.Log("Moved");
    }
}