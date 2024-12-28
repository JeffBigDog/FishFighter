using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float yRange = 5f;
    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + Random.Range(10f, 20f);
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(0, 2) == 0 ? -10f : 10f;
        float randomY = Random.Range(-yRange, yRange);
        Vector2 spawnPosition = new Vector2(randomX, randomY);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
