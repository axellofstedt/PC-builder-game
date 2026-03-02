using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkbenchUI : MonoBehaviour
{
    [Header("Order Image & Header")]
    public Image orderImage;
    public TMP_Text orderHeader;

    [Header("Buttons")]
    public Button DoneButton;

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

    public void ShowOrderImage()
    {
        List<PCPart> order = OrderManager.Instance.currentOrder;
        if (order != null && order.Count > 0)
        {
            // Uppdatera texten f�r varje del
            foreach (PCPart part in order)
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
    }
}
