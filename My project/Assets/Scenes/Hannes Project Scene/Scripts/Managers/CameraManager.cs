using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }
    public Camera PlayerCamera;
    public FirstPersonController playerController;
    public Interactor playerInteractor;

    private Camera activeCamera;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // Starta med spelarkameran
        SwitchCamera(PlayerCamera);
    }

    public void SwitchCamera(Camera newCamera)
    {
        // Stäng av nuvarande kamera
        if (activeCamera != null)
            activeCamera.enabled = false;

        // Slå på ny kamera
        activeCamera = newCamera;
        activeCamera.enabled = true;
    }

    public void ReturnToPlayerCamera()
    {
        SwitchCamera(PlayerCamera);
    }
}
