using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveDirection;

    void Update()
    {
        ProcessInputs();
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
