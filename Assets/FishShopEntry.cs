using UnityEngine;
using UnityEngine.EventSystems;

public class FishShopEntry : MonoBehaviour, IPointerClickHandler
{
    public GameObject fishPrefab;  // Fish to spawn
    public int price = 10;         // Price of the fish
    public Transform spawnPoint;   // Where the fish spawns

    // Detect clicks on the UI element
    public void OnPointerClick(PointerEventData eventData)
    {
        if (MoneyManager.Instance.money >= price)
        {
            AudioManager.instance.Play("Buyfish");
            MoneyManager.Instance.AddMoney(-price);  // Deduct price
            Instantiate(fishPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log(fishPrefab.name + " spawned for $" + price);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
}
