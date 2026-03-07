using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    public PCGenerator pcGenerator;
    
    public List<PCPart> currentOrder;

    public int numberOfRams;

    private void Awake()
    {
        Instance = this;
    }

    public List<PCPart> GetNewOrder()
    {
        currentOrder = pcGenerator.GetNewPC();
        numberOfRams = currentOrder.Count(part => part.partType == PartType.RAM);
        return currentOrder;
    }

    public void ClearOrder()
    {
        currentOrder = null;
    }
}
