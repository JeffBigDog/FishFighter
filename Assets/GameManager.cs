using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject foodPrefab;
    private bool canSpawn = true;
    public float spawnCooldown = 5f;
    public float foodLifetime = 3f;  // Food despawns after 3 seconds

    public GameObject player;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canSpawn && !IsPointerOverCoin())
        {
            SpawnFoodAtMousePosition();
        }
    }

    // Spawns food at the position where the player clicks
    void SpawnFoodAtMousePosition()
    {
        if (foodPrefab != null)
        {
            // Get mouse position and convert to world position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Instantiate food at the mouse position
            GameObject food = Instantiate(foodPrefab, mousePosition, Quaternion.identity);
            Debug.Log("Food spawned at: " + mousePosition);

            // Destroy the food after 'foodLifetime' seconds
            Destroy(food, foodLifetime);

            canSpawn = false;
            Invoke(nameof(ResetCooldown), spawnCooldown);
        }
    }

    void ResetCooldown()
    {
        canSpawn = true;
    }

    // Prevent spawning if the player clicks on UI elements
    bool IsPointerOverCoin()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        // Check if the ray hits a coin
        if (hit.collider != null && hit.collider.CompareTag("Coin"))
        {
            return true;
        }
        return false;
    }
}
