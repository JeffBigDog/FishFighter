using UnityEngine;

public class ShitFish : BaseFish {
    public GameObject coinPrefab;
    // Spawn coins at level 2
    public override void HandleAbility() 
    {
        if (level >= 2 && !IsInvoking(nameof(SpawnCoin)))
        {
            float spawnTime = Random.Range(5f, 15f);
            Invoke(nameof(SpawnCoin), spawnTime);
        }
    }

    void SpawnCoin()
    {
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
        Debug.Log("Coin spawned by ShitFish!");
    }
}

// using UnityEngine;

// public class ShitFish : MonoBehaviour
// {
//     public float moveSpeed = 2f;
//     public float moveInterval = 2f;
//     public float detectionRadius = 25f;
//     public int xp = 0;
//     public int xpToLevelUp = 5;
//     public int level = 1;
//     public GameObject coinPrefab;

//     public float minX = -8f;  // Boundary - Left
//     public float maxX = 8f;   // Boundary - Right
//     public float minY = -4f;  // Boundary - Bottom
//     public float maxY = 4f;   // Boundary - Top

//     private Vector2 moveDirection;
//     private float nextMoveTime;
//     private bool facingRight = true;
//     private bool moveVertically = false;

//     private SpriteRenderer spriteRenderer;

//     void Start()
//     {
//         spriteRenderer = GetComponent<SpriteRenderer>();
//     }

//     void Update()
//     {
//         if (level >= 2)
//         {
//             HandleCoinSpawning();
//         }

//         GameObject closestFood = FindClosestFood();
//         if (closestFood != null)
//         {
//             MoveTowardsFood(closestFood);
//         }
//         else
//         {
//             HandleRandomMovement();
//         }
//     }

//     // Handle random left-right movement with occasional vertical shifts
//     void HandleRandomMovement()
//     {
//         moveSpeed = 2f;

//         if (Time.time >= nextMoveTime)
//         {
//             // Always move left or right, with a chance to move up/down as well
//             int direction = Random.Range(0, 4);  // 0 = left, 1 = right, 2 = up, 3 = down

//             // Always choose left or right
//             if (direction == 0)
//             {
//                 moveDirection = Vector2.left;
//             }
//             else if (direction == 1)
//             {
//                 moveDirection = Vector2.right;
//             }

//             // Randomly choose to add vertical movement
//             moveVertically = Random.Range(0, 2) == 0;  // 50% chance to move up/down

//             nextMoveTime = Time.time + moveInterval;

//             // Flip sprite horizontally if necessary
//             if ((moveDirection == Vector2.left && facingRight) || (moveDirection == Vector2.right && !facingRight))
//             {
//                 Flip();
//             }
//         }

//         // Apply left/right movement
//         transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

//         // Apply up/down movement (if chosen)
//         if (moveVertically)
//         {
//             float verticalDirection = Random.Range(0, 2) == 0 ? 1f : -1f;  // Move up or down
//             transform.Translate(Vector2.up * verticalDirection * (moveSpeed / 2) * Time.deltaTime);
//         }

//         // Clamp position within defined boundaries
//         transform.position = new Vector2(
//             Mathf.Clamp(transform.position.x, minX, maxX),
//             Mathf.Clamp(transform.position.y, minY, maxY)
//         );
//     }

//     // Find the closest food within detection radius
//     GameObject FindClosestFood()
//     {
//         GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
//         GameObject closestFood = null;
//         float minDistance = detectionRadius;

//         foreach (GameObject food in foodObjects)
//         {
//             float distance = Vector2.Distance(transform.position, food.transform.position);
//             if (distance < minDistance)
//             {
//                 minDistance = distance;
//                 closestFood = food;
//             }
//         }
//         return closestFood;
//     }

//     // Move towards the closest food
//     void MoveTowardsFood(GameObject food)
//     {
//         moveSpeed = 5f;

//         transform.position = Vector2.MoveTowards(transform.position, food.transform.position, moveSpeed * Time.deltaTime);

//         if (food.transform.position.x < transform.position.x && facingRight)
//         {
//             Flip();
//         }
//         else if (food.transform.position.x > transform.position.x && !facingRight)
//         {
//             Flip();
//         }
//     }

//     // Flip the sprite horizontally
//     void Flip()
//     {
//         facingRight = !facingRight;
//         spriteRenderer.flipX = !spriteRenderer.flipX;  // Flip the sprite
//     }

//     // Trigger when colliding with food
//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         Debug.Log(collision.gameObject.name);
//         if (collision.CompareTag("Food"))
//         {
//             xp += 1;
//             Destroy(collision.gameObject);

//             if (xp >= xpToLevelUp)
//             {
//                 LevelUp();
//             }
//         }
//     }

//     // Level up logic
//     void LevelUp()
//     {
//         level++;
//         xp = 0;
//         Debug.Log("ShitFish leveled up to " + level);

//         // Increase the scale by 0.5 for each level
//         transform.localScale += new Vector3(0.3f, 0.3f, 0);
//     }


//     // Handle coin spawning at level 2
//     void HandleCoinSpawning()
//     {
//         if (!IsInvoking(nameof(SpawnCoin)))
//         {
//             float spawnTime = Random.Range(5f, 15f);
//             Invoke(nameof(SpawnCoin), spawnTime);
//         }
//     }

//     void SpawnCoin()
//     {
//         Instantiate(coinPrefab, transform.position, Quaternion.identity);
//         Debug.Log("Coin spawned by ShitFish!");
//     }
// }
