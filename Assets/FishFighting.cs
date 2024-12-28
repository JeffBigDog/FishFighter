using UnityEngine;

public class FishFighter : MonoBehaviour
{
    public GameObject attackPrefab;  
    public GameObject counterPrefab;
    public float attackSpeed = 10f;  
    public float attackDuration = 3f;  
    public int attackDamage = 20;  // Damage per hit

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 attackDirection = GetAttackDirection();
            if (attackDirection != Vector2.zero)
            {
                Attack(attackDirection);
            }
        }
    }

    Vector2 GetAttackDirection()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.W)) vertical += 1f;  
        if (Input.GetKey(KeyCode.S)) vertical -= 1f;  
        if (Input.GetKey(KeyCode.A)) horizontal -= 1f;  
        if (Input.GetKey(KeyCode.D)) horizontal += 1f;  

        Vector2 direction = new Vector2(horizontal, vertical).normalized;  
        return direction;
    }

    void Attack(Vector2 direction)
    {
        GameObject attack = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        attack.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        Rigidbody2D arb = attack.GetComponent<Rigidbody2D>();
        arb.linearVelocity = direction * attackSpeed;

        AttackCollider attackCollider = attack.GetComponent<AttackCollider>();
        attackCollider.damage = attackDamage;
        attackCollider.SetAttackOwner(gameObject);

        Destroy(attack, attackDuration);
    }
  
  
}


