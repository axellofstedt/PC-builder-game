using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public GameObject prefab;      
    public Transform waypoint;     
    public float spawnInterval = 2f;
    public float distanceBetweenBots = 1f;
    public int maxBots = 4;

    private float timer;
    private Queue<GameObject> spawnedBots = new Queue<GameObject>();

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
