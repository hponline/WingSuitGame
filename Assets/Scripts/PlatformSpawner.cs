using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platform Pool")]
    public GameObject[] nextPlatformPrefab;
    public Transform spawnOrigin;  
    Transform lastEndPoint;    
    Queue<GameObject> platformPool = new();
    List<GameObject> activePlatformMap = new();

    [Header("Variables")]
    public int speed;
    public Transform player;
    public int mapPoolSize = 5;
    public float despawnPlatform = 300f;


    private void Awake()
    {
        for (int i = 0; i < mapPoolSize; i++)
        {
            GameObject obj = Instantiate(nextPlatformPrefab[Random.Range(0, nextPlatformPrefab.Length)],
            spawnOrigin.position,
            Quaternion.Euler(30, 0, 0),
            spawnOrigin);

            lastEndPoint = obj.transform.Find("EndPoint");
            obj.SetActive(false);
            platformPool.Enqueue(obj);
        }

        for (int i = 0; i < mapPoolSize; i++)
        {
            CreatePlatform();
        }
    }

    private void Update()
    {        
        foreach (var platform in activePlatformMap)
        {
            platform.transform.Translate(Vector3.back * Time.deltaTime * speed);
        }

        RecycleOldPlatform();
    }

    public void TriggerNextSpawn()
    {        
        CreatePlatform();
    }

    void CreatePlatform() // PlatformSpawn
    {
        GameObject platformPart = GetPooledObject();
        platformPart.transform.position = lastEndPoint.position;
        platformPart.transform.rotation = Quaternion.Euler(30, 0, 0);
        platformPart.transform.SetParent(spawnOrigin);

        platformPart.SetActive(true);
        activePlatformMap.Add(platformPart);

        Transform newEndPoint = platformPart.transform.Find("EndPoint");
        lastEndPoint = newEndPoint;
    }

    void RecycleOldPlatform() // Platform Geri dönüşüm
    {
        if (activePlatformMap.Count > 0)
        {
            GameObject firstPlatform = activePlatformMap[0];

            if (player.position.z - firstPlatform.transform.position.z > despawnPlatform)
            {
                activePlatformMap.RemoveAt(0);
                firstPlatform.SetActive(false);
                platformPool.Enqueue(firstPlatform);
            }
        }
    }

    GameObject GetPooledObject() // Havuzdan Platform çekme
    {
        if (platformPool.Count > 0)
        {
            GameObject obj = platformPool.Dequeue();
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(nextPlatformPrefab[Random.Range(0, nextPlatformPrefab.Length)]);
            return obj;
        }
    }

}
