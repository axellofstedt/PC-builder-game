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
        return new List<PCPart>
        {
            GetRandom(gpus),
            GetRandom(cpus),
            GetRandom(rams),
            GetRandom(motherboards),
            GetRandom(psus),
            GetRandom(cpuCoolers),
            GetRandom(chassies),
            GetRandom(drives),
            GetRandom(fans),
        };
    }

    PCPart GetRandom(List<PCPart> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
