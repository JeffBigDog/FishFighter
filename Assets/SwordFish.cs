using Unity.VisualScripting;
using UnityEngine;

public class SwordFish : BaseFish {
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isChasingFood)
        {
            DumbFish fish = collision.gameObject.GetComponent<DumbFish>();
            if (fish != null)
            {
                Destroy(fish.gameObject);  // Destroy the GameObject that the script is attached to
            }
        }       
    }
}