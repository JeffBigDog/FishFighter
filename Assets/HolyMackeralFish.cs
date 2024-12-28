using UnityEngine;

public class HolyMackeralFish : BaseFish {
    public GameObject healthPrefab;
    // Spawn coins at level 2
    public override void HandleAbility() 
    {
        if (level >= 2 && !IsInvoking(nameof(SpawnHealth)))
        {
            float spawnTime = Random.Range(5f, 15f);
            Invoke(nameof(SpawnHealth), spawnTime);
        }
    }

    void SpawnHealth()
    {
        Instantiate(healthPrefab, transform.position, Quaternion.identity);
        Debug.Log("Health spawned by Holy Mackeral!");
    }
}