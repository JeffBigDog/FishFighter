using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public int damage;
    private GameObject attackOwner;  // Reference to the owner

    void Start()
    {
        AudioManager.instance.Play("Gunshot");
    }

    public void SetAttackOwner(GameObject owner)
    {
        attackOwner = owner;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Prevent self-hit by checking if the collider is the attack owner
        if (other.gameObject == attackOwner) return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }        
        }
        else if (other.CompareTag("Enemy") && other.gameObject != attackOwner)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        Destroy(gameObject);  // Destroy the attack after hitting anything
        
    }
}


