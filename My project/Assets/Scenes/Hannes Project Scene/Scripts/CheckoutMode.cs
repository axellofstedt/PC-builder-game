using System.Collections.Generic;
using System.Linq;
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
    public Transform ChassiTrans;

    private float orderTimer = 0f;
    private List<PCPart> currentOrder;


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
        currentOrder = OrderManager.Instance.GetNewOrder();
        CheckoutUI.TakeOrder(currentOrder);
        currentCheckoutState = CheckoutState.Order;
        BotSpawner.Instance.GetFrontCustomerMovement().SetOrderingState();
    }

    public void CompleteOrder()
    {
        CheckoutUI.CompleteOrder();
        currentCheckoutState = CheckoutState.Complete;

        // RewardSystem evaluates the order based on time taken and accuracy of the order
        List<Selectable> builtPC = SelectionManager.Instance.currentSelectableBuild;
        List<PCPart> orderedPC = currentOrder;
        int correctComponents = builtPC.Count(built => orderedPC.Any(order => order.partName == built.PartName));
        RewardResult orderReward = rewardSystem.Evaluate(orderTimer, correctComponents);
        Debug.Log($"Time Score: {orderReward.timeScore}, Accuracy Score: {orderReward.accuracyScore}, Final Score: {orderReward.finalScore}, Stars: {orderReward.stars}");

        // Timer, button or effect to show completion before resetting to waiting
        BotSpawner.Instance.RemoveFrontBot();
        currentCheckoutState = CheckoutState.Waiting;

        // Reset current order
        OrderManager.Instance.ClearOrder();

        // Reset order timer
        orderTimer = 0f;

        // Destroy PC
        GameObject pc = GameObject.Find("PC");
        Destroy(pc);

        // Reset build
        SelectionManager.Instance.ResetBuild();

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

    public void PlacePCOnCheckout()
    {
        GameObject pc = new GameObject("PC");

        SelectionManager selectionManager = SelectionManager.Instance;
        Selectable chassi = selectionManager.currentChassi;

        // Place PC at chassi position and rotation
        pc.transform.SetPositionAndRotation(chassi.transform.position, chassi.transform.rotation);

        // Parent all selected objects to the PC
        foreach (Selectable obj in selectionManager.currentSelectableBuild)
        {
            Debug.Log($"Placing {obj.PartName} in PC");
            obj.transform.SetParent(pc.transform, true);
            
            // Remove hover interaction from chassi
            if (obj.PartType == PartType.Chassi) obj.GetComponent<PCPartHover>().hoverable = false;
        }

        // Move pivot of the PC to the bottom of the chassi
        Renderer r = chassi.GetComponentInChildren<Renderer>();
        float bottomOffset = r.bounds.extents.y;

        pc.transform.position += Vector3.up * bottomOffset;

        // Move to checkout
        pc.transform.SetPositionAndRotation(ChassiTrans.position + Vector3.up * bottomOffset, ChassiTrans.rotation);

        // Update ChecoutUI
        CheckoutUI.PCReady();
    }

}


