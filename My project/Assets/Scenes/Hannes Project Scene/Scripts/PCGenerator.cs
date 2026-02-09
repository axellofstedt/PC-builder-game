using System.Collections.Generic;
using UnityEngine;

public class PCGenerator : MonoBehaviour
{
    List<PCPart> gpus = new List<PCPart>();
    List<PCPart> cpus = new List<PCPart>();
    List<PCPart> rams = new List<PCPart>();
    List<PCPart> motherboards = new List<PCPart>();
    List<PCPart> psus = new List<PCPart>();
    List<PCPart> cpuCoolers = new List<PCPart>();
    List<PCPart> chassies = new List<PCPart>();
    List<PCPart> drives = new List<PCPart>();
    List<PCPart> fans = new List<PCPart>();

    void Start()
    {
        FillPools();
    }

    public List<PCPart> GetNewPC()
    {
        return GenerateRandomPC();
    }

    void FillPools()
    {
        // GPU
        gpus.Add(new PCPart(PartType.GPU, "RTX 4060"));
        gpus.Add(new PCPart(PartType.GPU, "RX 6700 XT"));

        // CPU
        cpus.Add(new PCPart(PartType.CPU, "Ryzen 5 5600X"));
        cpus.Add(new PCPart(PartType.CPU, "Intel i5 12400F"));

        // RAM
        rams.Add(new PCPart(PartType.RAM, "16GB DDR4"));
        rams.Add(new PCPart(PartType.RAM, "32GB DDR4"));

        // Motherboard
        motherboards.Add(new PCPart(PartType.Motherboard, "B550"));
        motherboards.Add(new PCPart(PartType.Motherboard, "B660"));

        // PSU
        psus.Add(new PCPart(PartType.PSU, "650W"));
        psus.Add(new PCPart(PartType.PSU, "750W"));

        // CPU cooling
        cpuCoolers.Add(new PCPart(PartType.CPUCooling, "Air Cooler"));
        cpuCoolers.Add(new PCPart(PartType.CPUCooling, "240mm AIO"));

        // Chassi
        chassies.Add(new PCPart(PartType.Chassi, "ATX Mid Tower"));
        chassies.Add(new PCPart(PartType.Chassi, "Micro ATX"));

        // Drives
        drives.Add(new PCPart(PartType.Drive, "1TB HDD"));
        drives.Add(new PCPart(PartType.Drive, "500GB SSD"));

        // Fans
        fans.Add(new PCPart(PartType.Fan, "2 Fans"));
        fans.Add(new PCPart(PartType.Fan, "4 RGB Fans"));
    }

    List<PCPart> GenerateRandomPC()
    {
        List<PCPart> pc = new List<PCPart>();

        pc.Add(GetRandom(gpus));
        pc.Add(GetRandom(cpus));
        pc.Add(GetRandom(rams));
        pc.Add(GetRandom(motherboards));
        pc.Add(GetRandom(psus));
        pc.Add(GetRandom(cpuCoolers));
        pc.Add(GetRandom(chassies));
        pc.Add(GetRandom(drives));
        pc.Add(GetRandom(fans));

        return pc;
    }

    PCPart GetRandom(List<PCPart> list)
    {
        int index = Random.Range(0, list.Count);
        return list[index];
    }
}
