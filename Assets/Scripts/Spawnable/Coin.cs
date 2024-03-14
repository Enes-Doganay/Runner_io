using UnityEngine;

public class Coin : Collectable
{
    const string playerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Collect();
        }
    }
    protected override void Collect()
    {
        base.Collect();
        CurrencyManager.Instance.AddCurrency(1);
        gameObject.SetActive(false);
    }
}