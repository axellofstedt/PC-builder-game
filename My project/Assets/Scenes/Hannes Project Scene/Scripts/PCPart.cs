using UnityEngine;

public class PCPart
{
    public PartType partType;   
    public string partName;   

    public PCPart(PartType type, string name)
    {
        partType = type;
        partName = name;
    }
}
