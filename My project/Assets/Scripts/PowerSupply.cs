using UnityEngine;

public class PowerSupply : PCPartComponent
{
    void Awake()
    {
        partType = PartType.PSU;
        partName = "Power Supply Unit";
    }

    void OnMouseDown()
    {
        
    }
}
