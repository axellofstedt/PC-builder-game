using UnityEngine;

public class SensitivityChanger : MonoBehaviour
{
    public static SensitivityChanger Instance;
    [SerializeField] FirstPersonController FPC;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FPC = GetComponent<FirstPersonController>();
    }
    public void SetSensitivity(float sensitivity)
    {
        Debug.Log("Setting sensitivity to: " + sensitivity);
        if (FPC == null)
        {
            Debug.LogError("FirstPersonController component is missing!");
            return;
        }
        FPC.mouseSensitivity =  sensitivity;
    }

    public float GetSensitivity()
    {
        return FPC.mouseSensitivity;
    }
}
