using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveDirection;
    private bool facingRight = true;

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

        // Check if we need to flip the sprite based on horizontal input
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }
    }

    void Move()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

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
}
