using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public string enemyPrefabPath;
    public float spawnInterval = 2;
    public int maxEnemies = 20;

    public Wave(string prefabPath, float interval, int max)
    {
        enemyPrefabPath = prefabPath;
        spawnInterval = interval;
        maxEnemies = max;
    }
}

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints;
    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private GameManagerBehavior gameManager;
    private AblyManagerBehavior ablyManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        ablyManager = GameObject.Find("AblyManager").GetComponent<AblyManagerBehavior>();
    }

    void Update()
    {
        if (!ablyManager.started)
        {
            lastSpawnTime = Time.time;
            return;
        }

        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length)
        {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;
            bool isFirstEnemy = enemiesSpawned == 0;
            bool canSpawn = (isFirstEnemy && timeInterval > timeBetweenWaves) || (timeInterval > spawnInterval);

            if (canSpawn && enemiesSpawned < waves[currentWave].maxEnemies)
            {
                lastSpawnTime = Time.time;
                string prefabPath = Random.value > 0.5f ? "Prefabs/Enemy" : "Prefabs/Enemy 2";
                GameObject newEnemy = EnemyFactory.CreateEnemy(
                    prefabPath,
                    transform.position,
                    Quaternion.identity,
                    waypoints
                );

                if (newEnemy != null)
                {
                    enemiesSpawned++;
                }
            }

            if (enemiesSpawned == waves[currentWave].maxEnemies &&
                GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                gameManager.Wave++;
                gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
        }
        else
        {
            gameManager.gameOver = true;
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }
    }
}

