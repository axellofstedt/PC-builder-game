using UnityEngine;

public class BeginnerModeManager : MonoBehaviour
{
    public static BeginnerModeManager instance;
    public bool isActive = false;
    //private BoxCollider[] allSlots;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        BoxCollider[] allSlots = FindObjectsOfType<BoxCollider>();
        foreach (var box in allSlots)
        {
            Debug.Log(box.name);
            //PlacementZone zone = box.GetComponent<PlacementZone>();
            if (!box.CompareTag("Untagged"))
            {
                // Hämta eller lägg till Outline
                Outline outline = box.GetComponent<Outline>();
                if (outline == null)
                    outline = box.gameObject.AddComponent<Outline>();

                outline.enabled = false; // Starta avstängd
            }
        }
    }

    public void ToggleBeginnerMode()
    {
        isActive = !isActive;
        Debug.Log("Mode is " + isActive);

        BoxCollider[] allSlots = FindObjectsOfType<BoxCollider>();

        foreach (var box in allSlots)
        {
            if (!box.CompareTag("Untagged"))
            {
                Outline outline = box.GetComponent<Outline>();

                if (outline == null)
                {
                    outline = box.gameObject.AddComponent<Outline>();
                }
                outline.enabled = isActive;
            }
        }
    }
}
