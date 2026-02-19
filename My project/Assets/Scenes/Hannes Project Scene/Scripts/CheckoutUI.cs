using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckoutUI : MonoBehaviour
{
    public Button takeOrderButton;
    public Button closeOrderButton;
    public Image orderImage;

    // Text fields for each part type
    public TMP_Text gpuText;
    public TMP_Text cpuText;
    public TMP_Text ramText;
    public TMP_Text motherboardText;
    public TMP_Text psuText;
    public TMP_Text cpuCoolingText;
    public TMP_Text chassiText;
    public TMP_Text driveText;
    public TMP_Text fanText;

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

    public void CompleteOrder()
    {
        takeOrderButton.gameObject.SetActive(false);
        closeOrderButton.gameObject.SetActive(false);
        orderImage.gameObject.SetActive(false);
    }
}
