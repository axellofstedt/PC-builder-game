using TMPro;
using UnityEngine;

public class OrderImage : MonoBehaviour
{
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

    public int numberOfStrikedRams = 0;


    public void SetOrderText()
    {
        // Uppdatera texten för varje del
        foreach (PCPart part in OrderManager.Instance.currentOrder)
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
        ramText.text = OrderManager.Instance.numberOfRams + "x " + ramText.text;
    }

    public void StrikeComponent(Selectable obj)
    {
        foreach (PCPart part in OrderManager.Instance.currentOrder)
        {
            if (part.partType == obj.PartType && part.partName == obj.PartName)
            {
                // Om det är rätt del, stryk över den i orderlistan
                switch (obj.PartType)
                {
                    case PartType.GPU:
                        gpuText.text = "<s>" + gpuText.text + "</s>";
                        break;
                    case PartType.CPU:
                        cpuText.text = "<s>" + cpuText.text + "</s>";
                        break;
                    case PartType.RAM:
                        numberOfStrikedRams++;
                        if (numberOfStrikedRams >= OrderManager.Instance.numberOfRams * OrderManager.Instance.numberOfRams)
                            ramText.text = "<s>" + ramText.text + "</s>";
                        break;
                    case PartType.Motherboard:
                        motherboardText.text = "<s>" + motherboardText.text + "</s>";
                        break;
                    case PartType.PSU:
                        psuText.text = "<s>" + psuText.text + "</s>";
                        break;
                    case PartType.CPUCooling:
                        cpuCoolingText.text = "<s>" + cpuCoolingText.text + "</s>";
                        break;
                    case PartType.Chassi:
                        chassiText.text = "<s>" + chassiText.text + "</s>";
                        break;
                    case PartType.Drive:
                        driveText.text = "<s>" + driveText.text + "</s>";
                        break;
                    case PartType.Fan:
                        fanText.text = "<s>" + fanText.text + "</s>";
                        break;
                }
            }
        }
    }
}
