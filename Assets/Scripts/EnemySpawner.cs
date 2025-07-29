using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("敵人 Prefab")]
    public GameObject enemyPrefab;

    [Header("敵人出生點們")]
    public Transform[] enemySpawnPoints;

    [Header("每個點產生幾隻")]
    public int enemiesPerSpawnPoint = 1;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            for (int i = 0; i < enemiesPerSpawnPoint; i++)
            {
                Vector3 spawnPos = spawnPoint.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}
