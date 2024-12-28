using UnityEngine;

public class FoodBehavior : MonoBehaviour
{
    public float fallSpeed = 2f;  // Speed at which food falls
    public Transform target;      // Target the food falls towards (optional)
    public float collectSpeed = 5f;  // Speed at which the food is collected
    private bool isCollecting = false;  // Control for collection state

    void Update()
    {
        if (isCollecting && target != null)
        {
            // Move towards the target (fish)
            transform.position = Vector3.MoveTowards(transform.position, target.position, collectSpeed * Time.deltaTime);

            // Destroy the food when it reaches the target
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // Make the food fall downwards
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }
    }

    // Trigger collection by the fish when they collide with the food
    public void Collect(Transform fishTarget)
    {
        target = fishTarget;
        isCollecting = true;
    }
}
