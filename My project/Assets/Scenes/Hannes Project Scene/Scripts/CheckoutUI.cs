using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CheckoutUI : MonoBehaviour
{
    [Header("Buttons")]
    public Button takeOrderButton;
    public Button closeOrderButton;
    public Button giveOrderButton;
    public Button exitModeButton;
    public bool firstOrderComplete = false;

    [Header("Order Image")]
    public Image orderImage;
    private OrderImage orderImageScript;

    [Header("Reward Screen")]
    public GameObject rewardPanel;
    public TMP_Text timeText;
    public TMP_Text componentsText;

    [Header("Stars")]
    [SerializeField] private Image[] starImages;
    [SerializeField] private Sprite filledStar;
    [SerializeField] private Sprite emptyStar;

    [Header("OrderScrrenToggleButtons")]
    public Button ActivateOrderOnPlayerScreenButton;
    public Button DeactivateOrderOnPlayerScreenButton;

    private void Awake()
    {
        orderImageScript = orderImage.GetComponent<OrderImage>();
    }


    public void NewCustomer()
    {
        exitModeButton.gameObject.SetActive(false);
        takeOrderButton.gameObject.SetActive(true);
        closeOrderButton.gameObject.SetActive(false);
        giveOrderButton.gameObject.SetActive(false);
        orderImage.gameObject.SetActive(false);
    }

    public void TakeOrder(List<PCPart> pcOrder)
    {
        orderImageScript.SetOrderText();

        takeOrderButton.gameObject.SetActive(false);
        closeOrderButton.gameObject.SetActive(true);
        orderImage.gameObject.SetActive(true);
        ActivateOrderOnPlayerScreenButton.gameObject.SetActive(true);
    }

    public void PCReady()
    {
        closeOrderButton.gameObject.SetActive(false);
        giveOrderButton.gameObject.SetActive(true);
    }

    public void CompleteOrder()
    {
        takeOrderButton.gameObject.SetActive(false);
        closeOrderButton.gameObject.SetActive(false);
        giveOrderButton.gameObject.SetActive(false);
        orderImage.gameObject.SetActive(false);
        if(firstOrderComplete != true)
        {
            firstOrderComplete = true;
        }
        ActivateOrderOnPlayerScreenButton.gameObject.SetActive(false);
        DeactivateOrderOnPlayerScreenButton.gameObject.SetActive(false);

        // Reset reward text and stars
        timeText.text = "";
        componentsText.text = "";
        foreach (var star in starImages)
            star.sprite = emptyStar;
    }

    // Reward Screen UI
    
    public void SetRewardPanel(bool set)
    {
        rewardPanel.SetActive(set);
    }

    public IEnumerator ShowStats(RewardResult orderReward)
    {
        yield return new WaitForSeconds(1.7f);
        timeText.text = $"Time: {orderReward.time:F2} seconds";

        yield return new WaitForSeconds(1f);
        componentsText.text = $"Correct components: {orderReward.correctComponents} / {orderReward.totalComponents}";

        yield return new WaitForSeconds(.8f);
        yield return ShowStars(orderReward.stars);

        yield return new WaitForSeconds(4f);
        SetRewardPanel(false);
        DayNightManager.Instance.StartNewDay();
    }

    private IEnumerator ShowStars(int starCount)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            yield return new WaitForSeconds(0.8f);

            if (i < starCount)
            {
                starImages[i].sprite = filledStar;
                AudioManager.Instance.PlaySFX(AudioManager.Instance.starPling);
            }
            else
                starImages[i].sprite = emptyStar;
        }
    }

    public void WaitingForCustomer()
    {
        exitModeButton.gameObject.SetActive(true);
        takeOrderButton.gameObject.SetActive(false);
        closeOrderButton.gameObject.SetActive(false);
        giveOrderButton.gameObject.SetActive(false);
        orderImage.gameObject.SetActive(false);
        ActivateOrderOnPlayerScreenButton.gameObject.SetActive(false);
        DeactivateOrderOnPlayerScreenButton.gameObject.SetActive(false);
    }

    // Order screen toggle buttons
    public void SetScreenOrderClicked()
    {
        ActivateOrderOnPlayerScreenButton.gameObject.SetActive(false);
        DeactivateOrderOnPlayerScreenButton.gameObject.SetActive(true);
    }

    public void RemoveScreenOrderClicked()
    {
        ActivateOrderOnPlayerScreenButton.gameObject.SetActive(true);
        DeactivateOrderOnPlayerScreenButton.gameObject.SetActive(false);
    }
}
