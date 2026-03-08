using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class CheckoutMode : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform promptAnchor;
    public Transform PromptAnchor => promptAnchor;
    public KeyCode InteractKey => KeyCode.E;
    public string PromptText => "E - Checkout Mode";
    public bool Interactable { get; set; } = true;
    public bool hasTakenOrder { get; private set; } = false;

    public CheckoutUI checkoutUI;
    public RewardSystem rewardSystem;
    public Transform ChassiTrans;

    private float orderTimer = 0f;
    private List<PCPart> currentOrder;


    private GameObject CheckoutPC;


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
        checkoutUI.TakeOrder(currentOrder);

        currentCheckoutState = CheckoutState.Order;
        BotSpawner.Instance.GetFrontCustomerMovement().SetOrderingState();

        // Skip if pc is ready
        if (CheckoutPC != null) checkoutUI.PCReady();
        hasTakenOrder = true;
    }
    
    public void CompleteOrder()
    {
        Debug.Log("Give order pressed");
        StartCoroutine(CompleteOrderCoroutine());
    }

    private IEnumerator CompleteOrderCoroutine()
    {
        checkoutUI.CompleteOrder();
        currentCheckoutState = CheckoutState.Complete;

        // RewardSystem evaluates the order based on time taken and accuracy of the order
        List<Selectable> builtPC = SelectionManager.Instance.currentSelectableBuild;
        List<PCPart> orderedPC = currentOrder;
        int correctComponents = builtPC.Count(built => orderedPC.Any(order => order.partName == built.PartName));
        RewardResult orderReward = rewardSystem.Evaluate(orderTimer, correctComponents);
        Debug.Log($"Time Score: {orderReward.timeScore}, Accuracy Score: {orderReward.accuracyScore}, Final Score: {orderReward.finalScore}, Stars: {orderReward.stars}");
        
        ShowRewardScreen();

        yield return new WaitForSeconds(10f);

        // Timer, button or effect to show completion before resetting to waiting
        BotSpawner.Instance.RemoveFrontBot();
        currentCheckoutState = CheckoutState.Waiting;

        // Reset current order
        OrderManager.Instance.ClearOrder();

        // Reset order timer
        orderTimer = 0f;

        BeginnerModeManager.instance?.ResetBeginnerMode();
        ReturnAllBuildPartsToShelves();
        SelectionManager.Instance.workbenchZone.ResetSlots();
        // Destroy PC
        Destroy(CheckoutPC);

        // Reset build
       // SelectionManager.Instance.ResetBuild();
        CheckoutPC = null;

        //DayNightManager.Instance.StartNewDay();

    }

    private void ReturnAllBuildPartsToShelves()
    {
        Selectable[] allParts = FindObjectsOfType<Selectable>();

        foreach (Selectable part in allParts)
        {
            // Flytta tillbaka om de är på workbench eller i datorn
            if (part.currentZone != null &&
                (part.currentZone.zoneType == ZoneType.Workbench ||
                 part.transform.parent != null)) // parent != null -> kan vara i CheckoutPC
            {
                SelectionManager.Instance.ReturnPartToShelf(part);
            }
        }

        // Rensa SelectionManager-listor
        SelectionManager.Instance.ResetBuild();
    }

    public void NewCustomer()
    {
        checkoutUI.NewCustomer();
        currentCheckoutState = CheckoutState.ReadyForOrder;
    }

    public void Escape()
    {
        ModeManager.Instance.SetMode(GameMode.Player);
    }

    public void PlacePCOnCheckout()
    {

        CheckoutPC = new GameObject("PC");

        SelectionManager selectionManager = SelectionManager.Instance;
        Selectable chassi = selectionManager.currentChassi;
        chassi.GetComponent<OpenCloseDoor>().closeDoor();

        // Place PC at chassi position and rotation

        CheckoutPC.transform.SetPositionAndRotation(chassi.transform.position, chassi.transform.rotation);

        // Parent all selected objects to the PC
        foreach (Selectable obj in selectionManager.currentSelectableBuild)
        {
            Debug.Log($"Placing {obj.PartName} in PC");

            obj.transform.SetParent(CheckoutPC.transform, true);
            
            // Remove hover interaction from chassi
            if (obj.PartType == PartType.Chassi) obj.GetComponent<PCPartHover>().hoverable = false;
        }

        // Move pivot of the PC to the bottom of the chassi
        Renderer r = chassi.GetComponentInChildren<Renderer>();
        float bottomOffset = r.bounds.extents.y;

        CheckoutPC.transform.position += Vector3.up * bottomOffset;

        // Move to checkout
        CheckoutPC.transform.SetPositionAndRotation(ChassiTrans.position + Vector3.up * bottomOffset, ChassiTrans.rotation);

        // Update ChecoutUI
        if (currentCheckoutState == CheckoutState.Order) checkoutUI.PCReady();
    }

    // Show reward screen with stars and scores
    private void ShowRewardScreen()
    {
        checkoutUI.SetRewardPanel(true);

        // RewardSystem evaluates the order based on time taken and accuracy of the order
        List<Selectable> builtPC = SelectionManager.Instance.currentSelectableBuild;
        List<PCPart> orderedPC = currentOrder;
        int correctComponents = 0;
        foreach (Selectable built in builtPC)
        {
            foreach (PCPart order in orderedPC)
            {
                if (built.PartName == order.partName)
                {
                    correctComponents++;
                    break;
                }
            }
        }
        Debug.Log($"Correct Components: {correctComponents} out of {orderedPC.Count}");

        RewardResult orderReward = rewardSystem.Evaluate(orderTimer, correctComponents);
        Debug.Log($"Time Score: {orderReward.timeScore}, Accuracy Score: {orderReward.accuracyScore}, Final Score: {orderReward.finalScore}, Stars: {orderReward.stars}");

        // Display the reward result on the UI
        StartCoroutine(checkoutUI.ShowStats(orderReward));
    }
}


