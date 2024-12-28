using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float fallSpeed = 1f;  // Fall speed
    public float lifetime = 5f;   // Time before automatic destruction
    public float collectSpeed = 200f;  // Speed to move towards the target
    public bool isCollectable = true;
    private bool isCollecting = false;
    public Transform target;  // Target to move towards (like a UI object)

    void Start()
    {
        // Automatically destroy after 'lifetime' seconds if not collected
        Destroy(gameObject, lifetime);
        // Find the target that the object will move to when being collected
        target = GetTarget();
    }

    void Update()
    {
        if (!isCollecting || target == null)
        {
            // Normal falling behavior
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }
        else
        {
            // Move towards the target quickly
            transform.position = Vector3.MoveTowards(transform.position, target.position, collectSpeed * Time.deltaTime);
            
            // Destroy when reaching the target
            if (Vector3.Distance(transform.position, target.position) < 0.5f)
            {
                TriggerCollectAction();
                Destroy(gameObject);
            }
        }
    }

    // TODO make the objects land on the floor

    private void OnMouseDown()
    {
        // Trigger the collecting behavior
        if (isCollectable)
        {
            isCollecting = true;
        }
    }

    public virtual Transform GetTarget()
    {
        // Override with get target to go to when collected
        return null;
    }

    public virtual void TriggerCollectAction()
    {
        // Override with action when item is collected
    }
}

