using TMPro;
using UnityEngine;

public class PlayerOrderUI : MonoBehaviour
{
    [Header("Image")]
    public GameObject OrderImage;
    private OrderImage orderImageScript;

    private bool orderActive = false;

    private void Awake()
    {
        orderImageScript = OrderImage.GetComponent<OrderImage>();
    }


    private void Update()
    {
        if (ModeManager.Instance.currentMode == GameMode.Checkout || ModeManager.Instance.currentMode == GameMode.Workbench)
        {
            OrderImage.gameObject.SetActive(false);
        }
        else if (ModeManager.Instance.currentMode == GameMode.Player && orderActive)
        {
            OrderImage.gameObject.SetActive(true);
        }

        if (OrderManager.Instance.currentOrder == null)
        {
            DeactivateOrderOnScreen();
        }
    }

    // Order in corner of screen
    public void ActivateOrderOnScreen()
    {
        orderImageScript.SetOrderText();
        OrderImage.gameObject.SetActive(true);
        orderActive = true;
    }

    public void DeactivateOrderOnScreen()
    {
        OrderImage.gameObject.SetActive(false);
        orderActive = false;
    }
}
