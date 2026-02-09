using UnityEngine;

public enum PartType
{
    GPU, CPU, RAM, Motherboard, PSU,
    CPUCooling, Chassi, Drive, Fan
}

[CreateAssetMenu(menuName = "PC Parts/PC Part")]
public class PCPart : ScriptableObject
{
    public PartType partType;
    public string partName;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(partName))
            partName = name;
    }

}