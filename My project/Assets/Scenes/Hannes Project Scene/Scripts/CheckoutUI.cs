using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckoutUI : MonoBehaviour
{
    [Header("Buttons")]
    public Button takeOrderButton;
    public Button closeOrderButton;
    public Button giveOrderButton;

    [Header("Order Image & Header")]
    public Image orderImage;
    public TMP_Text orderHeader;

    [Header("Part Text Fields")]
    public TMP_Text gpuText;
    public TMP_Text cpuText;
    public TMP_Text ramText;
    public TMP_Text motherboardText;
    public TMP_Text psuText;
    public TMP_Text cpuCoolingText;
    public TMP_Text chassiText;
    public TMP_Text driveText;
    public TMP_Text fanText;

    [Header("Reward Screen")]
    public GameObject rewardPanel;
    public TMP_Text timeText;
    public TMP_Text componentsText;

    [Header("Stars")]
    [SerializeField] private Image[] starImages;
    [SerializeField] private Sprite filledStar;
    [SerializeField] private Sprite emptyStar;

    public void NewCustomer()
    {
        takeOrderButton.gameObject.SetActive(true);
        closeOrderButton.gameObject.SetActive(false);
        orderImage.gameObject.SetActive(false);
    }

    public void TakeOrder(List<PCPart> pcOrder)
    {
        // Uppdatera texten för varje del
        foreach (PCPart part in pcOrder)
        {
            switch (part.partType)
            {
                case PartType.GPU:
                    gpuText.text = part.partName;
                    break;

                case PartType.CPU:
                    cpuText.text = part.partName;
                    break;

                case PartType.RAM:
                    ramText.text = part.partName;
                    break;

                case PartType.Motherboard:
                    motherboardText.text = part.partName;
                    break;

                case PartType.PSU:
                    psuText.text = part.partName;
                    break;

                case PartType.CPUCooling:
                    cpuCoolingText.text = part.partName;
                    break;

                case PartType.Chassi:
                    chassiText.text = part.partName;
                    break;

                case PartType.Drive:
                    driveText.text = part.partName;
                    break;

                case PartType.Fan:
                    fanText.text = part.partName;
                    break;
            }
        }

        takeOrderButton.gameObject.SetActive(false);
        closeOrderButton.gameObject.SetActive(true);
        orderImage.gameObject.SetActive(true);
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
    }

    private IEnumerator ShowStars(int starCount)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            yield return new WaitForSeconds(0.8f);

            if (i < starCount)
                starImages[i].sprite = filledStar;
            else
                starImages[i].sprite = emptyStar;
        }
    }
}
