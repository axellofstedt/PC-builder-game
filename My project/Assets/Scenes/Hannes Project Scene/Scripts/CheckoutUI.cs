using UnityEngine;
using UnityEngine.UI;

public class CheckoutUI : MonoBehaviour
{
    public Button takeOrderButton;
    public Button closeOrderButton;
    public Image orderImage;

    public void NewCustomer()
    {
        takeOrderButton.gameObject.SetActive(true);
        closeOrderButton.gameObject.SetActive(false);
        orderImage.gameObject.SetActive(false);
    }

    public void TakeOrder()
    {
        takeOrderButton.gameObject.SetActive(false);
        closeOrderButton.gameObject.SetActive(true);
        orderImage.gameObject.SetActive(true);
    }

    public void CompleteOrder()
    {
        takeOrderButton.gameObject.SetActive(false);
        closeOrderButton.gameObject.SetActive(false);
        orderImage.gameObject.SetActive(false);
        Debug.Log("Order Completed!");
    }
}
