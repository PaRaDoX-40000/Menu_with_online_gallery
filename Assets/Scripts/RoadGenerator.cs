using UnityEngine;
using System.Collections.Generic;

public class RoadGenerator : MonoBehaviour
{
    [Header("Settings")]
    public GameObject roadPrefab;      
    public Transform playerTransform;  
    public int poolSize = 10;          
    public float roadStepY = 3f;     

    private Queue<GameObject> roadPool = new Queue<GameObject>();
    private float lastSpawnY = 0f;
    private float firstRoadY = 0f; 
    private int spawnedCount = 0; 
    void Start()
    {

        for (int i = 0; i < poolSize; i++)
        {
            GameObject road = Instantiate(roadPrefab);
            road.SetActive(false);
            roadPool.Enqueue(road);
            road.transform.parent = transform;
        }

        for (int i = 0; i < poolSize / 2; i++)
        {
            ActivateNextRoad();
        }
    }


    void Update()
    {
        if (playerTransform.position.y > lastSpawnY - (roadStepY * 3))
        {
            ActivateNextRoad();
        }
    
        if (playerTransform.position.y - firstRoadY > roadStepY * 3)
        {
            RecycleOldestRoad();
        }
    }

    void ActivateNextRoad()
    {
        GameObject road = roadPool.Dequeue();
        road.transform.position = new Vector3(0, lastSpawnY, 0);
        road.SetActive(true);

        bool canSpawnEnemy = (spawnedCount > 0);
        SetupEnemy(road, canSpawnEnemy);

        roadPool.Enqueue(road);
        lastSpawnY += roadStepY;
        spawnedCount++;
    }

    void RecycleOldestRoad()
    {
        firstRoadY += roadStepY;
    }

    void SetupEnemy(GameObject road, bool canSpawn)
    {
        Transform enemy = road.transform.Find("Enemy");
        if (enemy != null)
        {
            if (!canSpawn)
            {
                enemy.gameObject.SetActive(false);
                return;
            }      
            enemy.localPosition = Vector3.zero;
            enemy.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
            enemy.gameObject.SetActive(Random.value > 0.3f);
        }
    }
}