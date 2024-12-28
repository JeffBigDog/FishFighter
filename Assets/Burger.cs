using UnityEngine;

public class BurgerBehavior : FallingObject
{
    public int healAmount = 25;
    private GameObject player;

    public override Transform GetTarget()
    {
        player = GameManager.Instance.player;
        return player.transform;
    }

    public override void TriggerCollectAction() 
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount); // Heal player
            AudioManager.instance.Play("Eat burger");
        }  
    }
}