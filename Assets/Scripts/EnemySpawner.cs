
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject[] GoodPrefabs;
    private List<Vector3> spawnPositions = new List<Vector3>()
    {
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 2),
        new Vector3(0, 0, 4),
        new Vector3(2, 0, 0),
        new Vector3(2, 0, 2),
        new Vector3(2, 0, 4),
        new Vector3(4, 0, 0),
        new Vector3(4, 0, 2),
        new Vector3(4, 0, 4),
        new Vector3(6, 0, 0),
        new Vector3(6, 0, 2),
        new Vector3(6, 0, 4)
    };
    public float airCount = 8;
    public float gameTime = 2.5f;
    public TextMeshProUGUI DepthText;
    public TextMeshProUGUI MaxDepthText;
    public TextMeshProUGUI o2Text;
    public GameObject GameController;
    public bool GameStart;
    public float DiveDepth;
    public float MaxDepth;

    private void Awake()
    {
        // Load the max depth from PlayerPrefs
        MaxDepth = PlayerPrefs.GetFloat("MaxDepth");
    }

    private void Start()
    {
        StartCoroutine(WaitForGameStart());
    }

    void Update()
    {
        GameStart = GameController.GetComponent<StartGame>().GameStart;

        if (airCount <= 0)
        {
            print("bad");
        }

        o2Text.text = "o2: " + Mathf.Round(airCount);
        DepthText.text = "Depth: " + Mathf.Round(DiveDepth) + "'";
        MaxDepthText.text = "Record: " + Mathf.Round(MaxDepth) + "'";

        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            spawnedObject.transform.Translate(Vector3.up * (5.5f / gameTime) * Time.deltaTime);
        }
        GameObject[] o2Spawn = GameObject.FindGameObjectsWithTag("Oxygen");
        foreach (GameObject spawnedObject1 in o2Spawn)
        {
            spawnedObject1.transform.Translate(Vector3.up * (5.5f / gameTime) * Time.deltaTime);
        }
    }

    private IEnumerator WaitForGameStart()
    {
        while (!GameStart)
        {
            yield return null;
        }

        // Once GameStart is true, start the coroutines
        StartCoroutine(spawnLoop());
        StartCoroutine(DepthReached());
    }

    private IEnumerator spawnLoop()
    {
        while (true)
        {
            destroySpawnedObjects();

            spawnRandomPrefab();
            airCount--;
            if (gameTime > 1.1)
            {
                gameTime -= 0.1f;
            }
            yield return new WaitForSeconds(gameTime);
        }
    }

    private IEnumerator DepthReached()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            DiveDepth += (1 / gameTime);

            if (DiveDepth > MaxDepth)
            {
                // Update the max depth
                MaxDepth = DiveDepth;
                PlayerPrefs.SetFloat("MaxDepth", MaxDepth);
            }
        }
    }

    private void spawnRandomPrefab()
    {
        Vector3 spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

        // Select 9 unique spawn positions from the list
        List<Vector3> randomPositions = new List<Vector3>();
        List<Vector3> availablePositions = new List<Vector3>(spawnPositions);
        for (int i = 0; i < Random.Range(9, 11); i++)
        {
            int randomIndex = Random.Range(0, availablePositions.Count);
            Vector3 position = availablePositions[randomIndex];
            randomPositions.Add(position);
            availablePositions.RemoveAt(randomIndex);
        }

        // Instantiate the prefab at the selected spawn positions
        foreach (Vector3 position in randomPositions)
        {
            Instantiate(prefab, position, Quaternion.identity);
        }
        Vector3 o2Position = availablePositions[Random.Range(0, availablePositions.Count)];
        GameObject o2Prefab = GoodPrefabs[Random.Range(0, GoodPrefabs.Length)];
        Instantiate(o2Prefab, o2Position, Quaternion.identity);
    }

    private void destroySpawnedObjects()
    {
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            Destroy(spawnedObject);
        }

        GameObject[] oxygenObjects = GameObject.FindGameObjectsWithTag("Oxygen");
        foreach (GameObject oxygenObject in oxygenObjects)
        {
            Destroy(oxygenObject);
        }
    }
}