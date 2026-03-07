using System.Collections.Generic;
using UnityEngine;

public class PCGenerator : MonoBehaviour
{
    [Header("Part Pools")]
    public List<PCPart> gpus;
    public List<PCPart> cpus;
    public List<PCPart> rams;
    public List<PCPart> motherboards;
    public List<PCPart> psus;
    public List<PCPart> cpuCoolers;
    public List<PCPart> chassies;
    public List<PCPart> drives;
    public List<PCPart> fans;

    public List<PCPart> GetNewPC()
    {
        List<PCPart> pc = new List<PCPart>
        {
            GetRandom(gpus),
            GetRandom(cpus),
            GetRandom(motherboards),
            GetRandom(psus),
            GetRandom(cpuCoolers),
            GetRandom(chassies),
            GetRandom(drives),
            GetRandom(fans),
        };

        PCPart ram = GetRandom(rams); // Same RAM model for all sticks
        int numberOfRams = Random.Range(1, 5); // 1 to 4 RAM sticks
        for (int i = 0; i < numberOfRams; i++)
        {
            pc.Add(ram);
        }

        return pc;
    }

    PCPart GetRandom(List<PCPart> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
