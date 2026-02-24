using System.Collections.Generic;
using UnityEngine;

public class CheckoutMode : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform promptAnchor;
    public Transform PromptAnchor => promptAnchor;
    public KeyCode InteractKey => KeyCode.E;
    public string PromptText => "E - Checkout Mode";
    public bool Interactable { get; set; } = true;

    public CheckoutUI CheckoutUI;
    public RewardSystem rewardSystem;
    private PCGenerator pcGenerator;

    private float orderTimer = 0f;
    private int correctComponents = 0;
    private List<PCPart> currentOrder;

    void Start()
    {
        pcGenerator = GetComponent<PCGenerator>();
    }

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

        if (currentCheckoutState == CheckoutState.Order)
        {
            orderTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.R)) CompleteOrder();
        }
    }

    public void TakeOrder()
    {
        // Generate random order and display it on the UI
        currentOrder = pcGenerator.GetNewPC();
        CheckoutUI.TakeOrder(currentOrder);
        currentCheckoutState = CheckoutState.Order;
        BotSpawner.Instance.GetFrontCustomerMovement().SetOrderingState();
    }

    public void CompleteOrder()
    {
        CheckoutUI.CompleteOrder();
        currentCheckoutState = CheckoutState.Complete;

        // RewardSystem evaluates the order based on time taken and accuracy of the order
        correctComponents = 9; // TODO: Implement accuracy calculation based on the currentOrder and the player's assembled PC
        RewardResult orderReward = rewardSystem.Evaluate(orderTimer, correctComponents);
        Debug.Log($"Time Score: {orderReward.timeScore}, Accuracy Score: {orderReward.accuracyScore}, Final Score: {orderReward.finalScore}, Stars: {orderReward.stars}");

        // Timer, button or effect to show completion before resetting to waiting
        BotSpawner.Instance.RemoveFrontBot();
        currentCheckoutState = CheckoutState.Waiting;

        // Reset order timer
        orderTimer = 0f;
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


