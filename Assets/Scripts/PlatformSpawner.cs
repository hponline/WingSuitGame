using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platform")]
    [SerializeField] Transform PlatformSpawn; // hiyerarþide spawn tutucu
    public GameObject nextPlatformPrefab;
    public float roadLength; // 745.89,
    public float nextSpawnMount; // -391.9435f
    public float fixedSpawnDistance = -850f;
    [SerializeField] Transform player;
    public float destroyBound;

    public int speed;



    [Header("Spawn")]
    public GameObject[] spawnObstacles;
    public Transform[] spawnObstaclePoints;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PlatformSpawn = GameObject.FindGameObjectWithTag("PlatformSpawner").transform;

        roadLength = GetComponentInChildren<Renderer>().bounds.size.z;

        SpawnObstacles();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.z < player.position.z + destroyBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float angle = Mathf.Deg2Rad * 30;
            float moveY = Mathf.Sin(angle) * fixedSpawnDistance;
            float moveZ = -Mathf.Cos(angle) * fixedSpawnDistance;

            Vector3 spawnPosition = transform.position + new Vector3(0, moveY, moveZ);
            Quaternion spawnRotation = Quaternion.Euler(30, 0, 0);

            GameObject newPlatform = Instantiate(nextPlatformPrefab, spawnPosition, spawnRotation, PlatformSpawn.transform);

            PlatformSpawner newSpawner = newPlatform.GetComponent<PlatformSpawner>();
            newSpawner.nextSpawnMount = nextSpawnMount - roadLength;

        }
    }

    void SpawnObstacles()
    {
        int numberOfObstaclesToSpawn = Random.Range(1, 3);
        List<int> emtySpawnPoint = new();

        for (int i = 0; i < spawnObstaclePoints.Length; i++)
        {
            emtySpawnPoint.Add(i);
        }

        for (int i = 0; i < numberOfObstaclesToSpawn; i++)
        {
            if (emtySpawnPoint.Count == 0) break; // buraya bi daha bak           

            int randomIndex = Random.Range(0, emtySpawnPoint.Count);
            int selectedPointIndex = emtySpawnPoint[randomIndex];
            emtySpawnPoint.RemoveAt(randomIndex);

            Transform spawnPoint = spawnObstaclePoints[selectedPointIndex];
            if (spawnPoint.childCount == 0)
            {
                int indexObstacle = Random.Range(0, spawnObstacles.Length);
                Instantiate(spawnObstacles[indexObstacle], spawnPoint.position, spawnPoint.rotation, spawnPoint);
            }
        }




        /* 
        List<int> emtySpawnPoint = new();

        for (int i = 0; i < spawnObstaclePoints.Length; i++)
        {
            if (!hasSpawned[i])
                emtySpawnPoint.Add(i);
        }

        if (emtySpawnPoint.Count > 0)
        {
            int indexSpawnPoint = emtySpawnPoint[Random.Range(0, emtySpawnPoint.Count)];

            //int indexSpawnPoint = Random.Range(0, spawnObstaclePoints.Length);
            int indexObstacle = Random.Range(0, spawnObstacles.Length);

            Transform point = spawnObstaclePoints[indexSpawnPoint];
            if (point.childCount > 0) return; // Zaten bir engel varsa yeni spawn etme

            Instantiate(spawnObstacles[indexObstacle], point.position, point.rotation, point);
            hasSpawned[indexSpawnPoint] = true;
        }
        */
    }
    // spawn pointlerin 3 tanesine de spawn ediyor
    // market için gameobject.find kullanýp atýlacak
}
