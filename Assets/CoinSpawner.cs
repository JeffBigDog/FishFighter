using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float xRange = 10f;
    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnCoin();
            // Randomize the next spawn interval (1 to 7 seconds)
            nextSpawnTime = Time.time + Random.Range(1f, 7f);
        }
    }

    void SpawnCoin()
    {
        float randomX = Random.Range(-xRange, xRange);
        Vector2 spawnPosition = new Vector2(randomX, transform.position.y);
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }
}
