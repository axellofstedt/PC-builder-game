using UnityEngine;

public enum GameMode
{
    Player,
    Checkout,
    Workbench
}

public class ModeManager : MonoBehaviour
{
    public static ModeManager Instance { get; private set; }

    [Header("Player")]
    public Camera playerCamera;
    public FirstPersonController playerController;
    public Interactor playerInteractor;
    public GameObject crosshair;

    [Header("Cameras")]
    public Camera checkoutCamera;
    public Camera workbenchCamera;

    [Header("UI")]
    [SerializeField] private ModeUI[] modeUIs;
    [SerializeField] private GameMode currentMode = GameMode.Player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetMode(GameMode newMode)
    {
        if (currentMode == newMode)
            return;

        ExitCurrentMode();
        currentMode = newMode;
        EnterCurrentMode();
    }

    private void EnterCurrentMode()
    {
        switch (currentMode)
        {
            case GameMode.Player:
                EnterPlayerMode();
                break;

            case GameMode.Checkout:
                EnterInteractionMode(checkoutCamera);
                break;

            case GameMode.Workbench:
                EnterInteractionMode(workbenchCamera);
                break;
        }

        UpdateUI();
    }

    private void ExitCurrentMode()
    {
        if (currentMode == GameMode.Player)
            return;
    }

    private void EnterPlayerMode()
    {
        CameraManager.Instance.SwitchCamera(playerCamera);
        playerController.enabled = true;

        crosshair?.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerInteractor?.ShowLastPrompt();
    }

    private void EnterInteractionMode(Camera modeCamera)
    {
        CameraManager.Instance.SwitchCamera(modeCamera);
        playerController.enabled = false;

        crosshair?.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playerInteractor?.ClearPrompt();
    }

    private void UpdateUI()
    {
        foreach (var ui in modeUIs)
            ui.UpdateVisibility(currentMode);
    }
}
