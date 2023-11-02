using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    //public GameObject GameController;

    public bool GameStart;
    public bool Respawn;
    //public GameObject player;
    private void Awake()
    {
        GameStart = GameObject.Find("GameController").GetComponent<StartGame>().GameStart;

        // Find the GameController object in the scene
        GameObject gameControllerObject = GameObject.Find("GameController");

        // Get the EnemySpawner script component on the GameController object
        enemySpawner = gameControllerObject.GetComponent<EnemySpawner>();
    }

    void Update()
    {
            print(GameStart);

        //gameTime = GameController.GetComponent<EnemySpawner>().gameTime;
    }
    // Start is called before the first frame update
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Destroy(this.gameObject);
            GameStart = false;
        }
        else

        if (other.gameObject.tag == "Oxygen")
        {
            enemySpawner.airCount = 8;
            print(enemySpawner.airCount);
        }
    }
    //IEnumerator DanDone()
    //{

    //}
}
