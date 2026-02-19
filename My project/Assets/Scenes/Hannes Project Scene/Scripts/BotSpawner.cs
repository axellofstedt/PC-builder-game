using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public static BotSpawner Instance { get; private set; }

    [Header("Spawn")]
    public List<GameObject> prefabs;
    public float spawnInterval = 5f;

    [Header("Waypoints")]
    public List<Transform> waypoints;

    [Header("Queue Settings")]
    public float distanceBetweenBots = 2f;
    public int maxQueueSize = 5;

    public Queue<GameObject> spawnedBots = new Queue<GameObject>();

    float timer = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SpawnBot();
    }
    
    private void Update()
    {
        timer += Time.deltaTime;  

        if (spawnedBots.Count < maxQueueSize && timer >= spawnInterval)
        {
            SpawnBot();
            timer = 0f; 
        }
    }

    void SpawnBot()
    {
        if (prefabs.Count == 0 || waypoints.Count == 0)
        {
            Debug.LogWarning("No prefabs or waypoints assigned to BotSpawner.");
            return;
        }

        // Randomly select a bot prefab to spawn
        GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];

        // Spawn the bot facing the first waypoint
        Vector3 dir = waypoints[0].position - transform.position;
        GameObject bot = Instantiate(prefab, transform.position, Quaternion.LookRotation(dir));
        BotMovement movement = bot.GetComponent<BotMovement>();
        movement.Init(waypoints, spawnedBots.Count, distanceBetweenBots);

        spawnedBots.Enqueue(bot);
    }

    public void RemoveFrontBot()
    {
        if (spawnedBots.Count == 0)
            return;

        GameObject frontBot = spawnedBots.Dequeue();
        Destroy(frontBot);

        UpdateQueuePositions();
    }

    public void UpdateQueuePositions()
    {
        int index = 0;
        foreach (GameObject bot in spawnedBots)
        {
            BotMovement movement = bot.GetComponent<BotMovement>();
            movement.SetQueueIndex(index);
            index++;
        }
    }

    public bool HasCustomer() { return spawnedBots.Count > 0; }

    public bool IsFrontCustomerReady()
    {
        if (spawnedBots.Count == 0)
            return false;

        BotMovement bot = spawnedBots.Peek().GetComponent<BotMovement>();
        if (bot == null)
        {
            Debug.LogWarning("Front bot does not have a BotMovement component.");
            return false;
        }

        return bot.IsReadyToOrder;
    }

    public BotMovement GetFrontCustomerMovement()
    {
        if (spawnedBots.Count == 0)
            return null;

        return spawnedBots.Peek().GetComponent<BotMovement>();
    }
}
