using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDiver : MonoBehaviour
{
    public GameObject diverPrefab; // prefab to be spawned
    private GameObject diverInstance; // instance of the prefab in the scene

    public List<Vector3> spawnPositions = new List<Vector3>()
    {
        new Vector3(0, 3, 0),
        new Vector3(0, 3, 2),
        new Vector3(0, 3, 4),
        new Vector3(2, 3, 0),
        new Vector3(2, 3, 2),
        new Vector3(2, 3, 4),
        new Vector3(4, 3, 0),
        new Vector3(4, 3, 2),
        new Vector3(4, 3, 4),
        new Vector3(6, 3, 0),
        new Vector3(6, 3, 2),
        new Vector3(6, 3, 4)
    };

    public int currentPositionIndex = 5; // index of the current position in spawnPositions
    public GameObject GameController;
    public bool GameStart;

    private void Start()
    {
        StartCoroutine(WaitForGameStart());
    }

    private IEnumerator WaitForGameStart()
    {
        while (!GameStart)
        {
            yield return null;
        }
        diverInstance = Instantiate(diverPrefab, spawnPositions[currentPositionIndex], Quaternion.identity);

    }

    private void Update()
    {
        GameStart = GameController.GetComponent<StartGame>().GameStart;
        if (GameStart)
        {
            // check for arrow key or swipe input
            if (Input.GetKeyDown(KeyCode.UpArrow) || (Input.touchCount > 0 && Input.GetTouch(0).deltaPosition.y > 0))
            {
                // move the prefab up if it's not already at the top of the grid
                if (currentPositionIndex - 3 >= 0)
                {
                    currentPositionIndex -= 3;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.touchCount > 0 && Input.GetTouch(0).deltaPosition.y < 0))
            {
                // move the prefab down if it's not already at the bottom of the grid
                if (currentPositionIndex + 3 < spawnPositions.Count)
                {
                    currentPositionIndex += 3;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.touchCount > 0 && Input.GetTouch(0).deltaPosition.x < 0))
            {
                // move the prefab left if it's not already at the left edge of the grid
                if ((currentPositionIndex % 3) != 0)
                {
                    currentPositionIndex--;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || (Input.touchCount > 0 && Input.GetTouch(0).deltaPosition.x > 0))
            {
                // move the prefab right if it's not already at the right edge of the grid
                if ((currentPositionIndex % 3) != 2 && currentPositionIndex + 1 < spawnPositions.Count)
                {
                    currentPositionIndex++;
                }
            }
            diverInstance.transform.position = spawnPositions[currentPositionIndex];
        }

        else
        
        if (!GameStart)
        {
            Destroy(diverInstance);
        }

    }

}