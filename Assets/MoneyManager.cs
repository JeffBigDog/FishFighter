using UnityEngine;
using TMPro;  // For TextMeshPro UI

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public int money = 0;
    public TextMeshProUGUI moneyText;  // Reference to the UI text


    void Awake()
    {
        // Singleton pattern to ensure only one MoneyManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "$ " + money;
        }
    }
}
