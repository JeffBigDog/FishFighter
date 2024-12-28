using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject attackPrefab;
    public float attackSpeed = 8f;
    public float attackInterval = 2f;
    public int attackDamage = 10;
    public float attackDuration = 1f;

    private Transform player;
    private float nextAttackTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null && Time.time >= nextAttackTime)
        {
            AttackPlayer();
            nextAttackTime = Time.time + attackInterval;
        }
    }

    void AttackPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        GameObject attack = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        // Set attack owner to ignore self-collision
        AttackCollider attackCollider = attack.GetComponent<AttackCollider>();
        attackCollider.damage = attackDamage;
        attackCollider.SetAttackOwner(gameObject);

        // Set rotation and velocity
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        attack.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        Rigidbody2D rb = attack.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * attackSpeed;

        Destroy(attack, attackDuration);
    }
}


