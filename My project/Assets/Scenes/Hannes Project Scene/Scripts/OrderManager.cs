using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    public PCGenerator pcGenerator;
    
    public List<PCPart> currentOrder;

    private void Awake()
    {
        Instance = this;
    }

    public List<PCPart> GetNewOrder()
    {
        currentOrder = pcGenerator.GetNewPC();
        return currentOrder;
    }

    public void ClearOrder()
    {
        currentOrder = null;
    }
}
