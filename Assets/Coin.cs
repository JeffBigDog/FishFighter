using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    public float fallSpeed = 1f;  // Coin fall speed
    public float lifetime = 5f;   // Time before automatic destruction
    public float collectSpeed = 200f;  // Speed to move towards the target
    private bool isCollecting = false;
    private Transform target;  // Target to move towards (like a UI object)

    void Start()
    {
        // Automatically destroy after 'lifetime' seconds if not collected
        Destroy(gameObject, lifetime);
        // Find the target (money counter or UI element)
        target = MoneyManager.Instance.moneyText.gameObject.transform;
    }

    void Update()
    {
        if (!isCollecting)
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
                MoneyManager.Instance.AddMoney(1);  // Add money when collected
                AudioManager.instance.Play("collect coin");
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        isCollecting = true;  // Trigger the collecting behavior
    }
}

