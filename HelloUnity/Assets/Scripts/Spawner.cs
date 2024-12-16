using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float range = 10;
    public float maxItems = 10; // Total amount of items to spawn (not a limit of the amount at once)
    public float spawnRate = 0.1f;
    public float nextSpawn;
    public int count = 0;
    // Start is called before the first frame update
    public GameObject deathPrefab;
    void Start()
    {
        nextSpawn = Time.time + spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn && count < maxItems)
        {
            SpawnItem();
            nextSpawn = Time.time + spawnRate;
        }
        
    }

    void SpawnItem()
    {
        if (itemPrefab != null)
        {
            count++;
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
            GameObject item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);

            BehaviorMinion behaviorMinion = item.GetComponent<BehaviorMinion>();
            if (behaviorMinion != null)
            {
                behaviorMinion.deathPrefab = deathPrefab;
            }
        }
    }
}   
