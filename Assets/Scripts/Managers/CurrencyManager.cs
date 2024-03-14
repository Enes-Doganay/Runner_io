using UnityEngine;

public class CurrencyManager : AbstractSingleton<CurrencyManager>
{
    private int currentCoin = 0;
    public int CurrentCoin => currentCoin;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void AddCurrency(int amount)
    {
        currentCoin += amount;
        UIManager.Instance.UpdateCoinText(currentCoin);
    }
}