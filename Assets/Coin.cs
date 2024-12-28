using UnityEngine;

public class CoinBehavior : FallingObject
{
    public override Transform GetTarget()
    {
        return MoneyManager.Instance.moneyText.gameObject.transform;
    }
    public override void TriggerCollectAction() 
    {
        MoneyManager.Instance.AddMoney(1);  // Add money when collected
        AudioManager.instance.Play("collect coin");
    }
}

