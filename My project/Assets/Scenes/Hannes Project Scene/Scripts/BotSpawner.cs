using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public GameObject prefab;      
    public Transform waypoint;     
    public float spawnInterval = 2f;
    public float distanceBetweenBots = 1f;

    private float timer;
    private float deathTimer;
    private Queue<GameObject> spawnedBots = new Queue<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }

        deathTimer += Time.deltaTime;
        if (deathTimer >= spawnInterval * 2f && spawnedBots.Count > 0)
        {
            GameObject botToDestroy = spawnedBots.Dequeue();
            Destroy(botToDestroy);
            deathTimer = 0f;
            updateQueue();
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
