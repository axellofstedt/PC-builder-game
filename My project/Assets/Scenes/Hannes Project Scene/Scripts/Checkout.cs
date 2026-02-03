using UnityEngine;

public class Checkout : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform promptAnchor;
    public Transform PromptAnchor => promptAnchor;
    public KeyCode InteractKey => KeyCode.E;
    public string PromptText => "E - Checkout Mode";
    public bool Interactable { get; set; } = true;

    public CheckoutUI CheckoutUI;

    enum CheckoutState
    {
        Waiting,
        ReadyForOrder,
        Order,
        Complete
    }

    private CheckoutState currentCheckoutState = CheckoutState.Waiting;

    public void Interact()
    {
        ModeManager.Instance.SetMode(GameMode.Checkout);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Escape();

        if (currentCheckoutState == CheckoutState.Waiting &&
            BotSpawner.Instance.HasCustomer() &&
            BotSpawner.Instance.IsFrontCustomerReady())
        {
            NewCustomer();
        }

        if (currentCheckoutState == CheckoutState.Order &&
            Input.GetKeyDown(KeyCode.R))
        {
            CompleteOrder();
        }
    }

    public void TakeOrder()
    {
        CheckoutUI.TakeOrder();
        currentCheckoutState = CheckoutState.Order;
    }

    public void CompleteOrder()
    {
        CheckoutUI.CompleteOrder();
        currentCheckoutState = CheckoutState.Complete;
        // Timer, button or effect to show completion before resetting to waiting
        BotSpawner.Instance.CompleteFrontCustomer();
        currentCheckoutState = CheckoutState.Waiting;
    }

    public void NewCustomer()
    {
        CheckoutUI.NewCustomer();
        currentCheckoutState = CheckoutState.ReadyForOrder;
    }

    public void Escape()
    {
        ModeManager.Instance.SetMode(GameMode.Player);
    }


}


