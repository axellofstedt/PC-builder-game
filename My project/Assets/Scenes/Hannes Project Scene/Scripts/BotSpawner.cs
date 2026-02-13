using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;      
    public Transform waypoint;     
    public float spawnInterval = 2f;
    public float distanceBetweenBots = 1f;
    public int maxBots = 4;

    public static BotSpawner Instance { get; private set; }

    public Queue<GameObject> spawnedBots = new Queue<GameObject>();

    private float timer;

    private void Awake()
    {
        Instance = this;
    }

    public bool HasCustomer(){ return spawnedBots.Count > 0; }

    public BotMovement GetFrontCustomer()
    {
        if (spawnedBots.Count == 0) return null;
        return spawnedBots.Peek().GetComponent<BotMovement>();
    }

    public bool IsFrontCustomerReady()
    {
        BotMovement bot = GetFrontCustomer();
        return bot != null && bot.IsAtTarget;
    }

    public void CompleteFrontCustomer()
    {
        DeSpawn();
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && spawnedBots.Count < maxBots)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];
        GameObject bot = Instantiate(prefab, transform.position, transform.rotation);
        spawnedBots.Enqueue(bot);

        BotMovement movement = bot.GetComponent<BotMovement>();
        if (movement != null)
        {
           movement.SetTarget(waypoint, spawnedBots.Count - 1, distanceBetweenBots);
        }
    }

    public void DeSpawn() 
    {   
        if (spawnedBots.Count == 0) return;
        GameObject bot = spawnedBots.Dequeue();
        Destroy(bot);
        updateQueue();
    }

    void updateQueue()
    {
        for (int i = 0; i < spawnedBots.Count; i++)
        {
            GameObject bot = spawnedBots.ToArray()[i];
            BotMovement movement = bot.GetComponent<BotMovement>();
            if (movement != null)
            {
                movement.SetTarget(waypoint, i, distanceBetweenBots);
            }
        }
    }
}
