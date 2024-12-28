using UnityEngine;
public class DumbFish : MonoBehaviour
{
    public float speed = 3f;
    public Transform player;

    void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
