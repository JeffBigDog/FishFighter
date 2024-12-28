using Unity.VisualScripting;
using UnityEngine;

public class BaseFish : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveInterval = 2f;
    public float detectionRadius = 25f;
    public int xp = 0;
    public int xpToLevelUp = 5;
    public int level = 1;
    public int maxLevel = 2;
    public bool isChasingFood = false;
    
    public float minX = -10f;  // Boundary - Left
    public float maxX = 10f;   // Boundary - Right
    public float minY = -5f;  // Boundary - Bottom
    public float maxY = 5f;   // Boundary - Top

    private Vector2 moveDirection;
    private float nextMoveTime;
    private bool facingRight = true;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleAbility();

        GameObject closestFood = FindClosestFood();
        if (closestFood != null)
        {
            MoveTowardsFood(closestFood);
        }
        else
        {
            HandleRandomMovement();
        }
    }

    // Handle random left-right movement with occasional vertical shifts
    void HandleRandomMovement()
    {
        moveSpeed = 2f;
        isChasingFood = false;

        if (Time.time >= nextMoveTime)
        {
            // Randomize movement in any direction (left, right, up, down)
            int direction = Random.Range(0, 4);  // 0 = left, 1 = right, 2 = up, 3 = down

            switch (direction)
            {
                case 0:
                    moveDirection = Vector2.left;
                    break;
                case 1:
                    moveDirection = Vector2.right;
                    break;
                case 2:
                    moveDirection = Vector2.up;
                    break;
                case 3:
                    moveDirection = Vector2.down;
                    break;
            }

            nextMoveTime = Time.time + moveInterval;

            // Flip sprite horizontally if moving left or right
            if ((moveDirection == Vector2.left && facingRight) || (moveDirection == Vector2.right && !facingRight))
            {
                Flip();
            }
        }

        // Move the fish in the chosen direction
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Bounce off horizontal walls
        if (transform.position.x <= minX || transform.position.x >= maxX)
        {
            moveDirection.x *= -1;  // Reverse horizontal direction
            Flip();  // Flip horizontally on bounce
        }

        // Bounce off vertical walls
        if (transform.position.y <= minY || transform.position.y >= maxY)
        {
            moveDirection.y *= -1;  // Reverse vertical direction
        }
    }

    // Find the closest food within detection radius
    GameObject FindClosestFood()
    {
        GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
        GameObject closestFood = null;
        float minDistance = detectionRadius;

        foreach (GameObject food in foodObjects)
        {
            float distance = Vector2.Distance(transform.position, food.transform.position);
            if (distance < minDistance &&
                food.transform.position.x > minX &&
                food.transform.position.x < maxX &&
                food.transform.position.y > minY &&
                food.transform.position.y < maxY)
            {
                minDistance = distance;
                closestFood = food;
            }
        }
        return closestFood;
    }

    // Move towards the closest food
    void MoveTowardsFood(GameObject food)
    {
        moveSpeed = 5f;
        isChasingFood = true;

        transform.position = Vector2.MoveTowards(transform.position, food.transform.position, moveSpeed * Time.deltaTime);

        if (food.transform.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (food.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
    }

    // Flip the sprite horizontally
    void Flip()
    {
        facingRight = !facingRight;
        
        // Invert the x-scale to flip the fish horizontally
        transform.localScale = new Vector3(
            -transform.localScale.x,
            transform.localScale.y,
            transform.localScale.z
        );
    }


    // Trigger when colliding with food
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            xp += 1;
            Destroy(collision.gameObject);
            AudioManager.instance.Play("Eat chicken");

            if (xp >= xpToLevelUp)
            {
                LevelUp();
            }
        }
    }

    // Level up logic
    void LevelUp()
    {
        if (level < maxLevel)
        {
            level++;
            xp = 0;
            Debug.Log("ShitFish leveled up to " + level);

            // Increase the scale by 0.5 for each level
            transform.localScale += new Vector3(0.3f, 0.3f, 0);
        }
    }

    public virtual void HandleAbility()
    {
        // Override with fish ability
    }
}
