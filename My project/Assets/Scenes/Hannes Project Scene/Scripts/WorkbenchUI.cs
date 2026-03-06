using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkbenchUI : MonoBehaviour
{
    [Header("Order Image & Header")]
    public Image orderImage;
    private OrderImage orderImageScript;

    [Header("Buttons")]
    public Button DoneButton;

    private void Awake()
    {
        orderImageScript = orderImage.GetComponent<OrderImage>();
    }   

    public void ShowOrderImage()
    {
        List<PCPart> order = OrderManager.Instance.currentOrder;
        if (order != null && order.Count > 0)
        {
            orderImageScript.SetOrderText();
            orderImage.gameObject.SetActive(true);
        }
        else
        {
            orderImage.gameObject.SetActive(false);
        }
    }

    public void SetButtoninteraction(bool set)
    {
        DoneButton.interactable = set;
        DoneButton.gameObject.SetActive(set);
    }
}
