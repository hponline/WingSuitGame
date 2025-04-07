using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] nextPlatformPrefab;
    public Transform spawnOrigin;
    public int speed;
    public int initializePlatformCount = 5;

    List<GameObject> activePlatform = new();

    Transform lastEndPoint;

    private void Start()
    {
        GameObject firstPlatform = Instantiate(nextPlatformPrefab[Random.Range(0, nextPlatformPrefab.Length)], 
            spawnOrigin.position, 
            Quaternion.Euler(30, 0, 0), 
            spawnOrigin);

        activePlatform.Add(firstPlatform);
        lastEndPoint = firstPlatform.transform.Find("EndPoint");

        for (int i = 0; i < initializePlatformCount; i++)
        {
            SpawnNextPlatform();
        }
    }

    private void Update()
    {        
        foreach (var platform in activePlatform)
        {
            platform.transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
    }

    void SpawnNextPlatform()
    {
        GameObject prefab = nextPlatformPrefab[Random.Range(0, nextPlatformPrefab.Length)];
        GameObject newPlatform = Instantiate(prefab, lastEndPoint.position, prefab.transform.rotation, spawnOrigin);
        Transform newEndPoint = newPlatform.transform.Find("EndPoint");

        lastEndPoint = newEndPoint ;
        activePlatform.Add(newPlatform);
    }

    public void TriggerNextSpawn()
    {
        SpawnNextPlatform();
    }
}
